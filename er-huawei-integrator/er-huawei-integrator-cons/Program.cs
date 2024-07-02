using System.Text;
using er_huawei_integrator_cons.Application.Model.Dto;
using er_huawei_integrator_cons.DataAccess;
using er_huawei_integrator_cons.Model.Dto;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class Program
{
    private static IConfiguration Configuration;

    public static async Task Main(string[] args)
    {
        // Configuración
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        Configuration = builder.Build();

        var connectionStrings = Configuration.GetSection("ConnectionStrings");
        var factory = new ConnectionFactory()
        {
            Uri = new Uri(connectionStrings["RabbitMQ"])
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        var queueName = "DataCreatedEvent";
        channel.QueueDeclare(queue: queueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message: {message}");
                await ProcessQueueAsync(message);
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing message: {ex.Message}");
            }
        };

        Console.WriteLine($"Starting consumer for queue: {queueName}");
        channel.BasicConsume(queue: queueName,
                             autoAck: false,
                             consumer: consumer);
        Console.WriteLine("Consumer started. Press [enter] to exit.");
        Console.ReadLine();
    }

    private static async Task ProcessQueueAsync(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            return;
        }

        var deviceRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<PlantDeviceResult>(message);

        if (deviceRequest is null)
        {
            return;
        }

        // Mapear a RepliMessagesDto
        var mappTopg = new RepliMessagesDto
        {
            Id = ToGuid(deviceRequest._id),
            BrandName = deviceRequest.brandName,
            StationCode = deviceRequest.stationCode,
            HoraConsulta = deviceRequest.repliedDateTime,
            HoraLlegoConsolidador = deviceRequest.repliedDateTime,
        };

        // genera instancia de postgress
        var dbContext = new PostgresDbContext(Configuration);
        var mdbContext = new MongoDbContext(Configuration);
        await dbContext.InsertDeviceAsync(mappTopg);

        await mdbContext.InsertDeviceDataAsync(deviceRequest);

        // Simulación de procesamiento asíncrono
        Console.WriteLine($"Processed message: {message}");
        await Task.CompletedTask;
    }

    public static Guid ToGuid(ObjectId objectId)
    {
        var bytes = objectId.ToByteArray();
        Array.Resize(ref bytes, 16);
        return new Guid(bytes);
    }
}