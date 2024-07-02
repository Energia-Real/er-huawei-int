namespace er_huawei_integrator.Data
{
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System.Text;

    public class RabbitMQClient
    {
        public void StartConsumer()
        {
            // Configuración de la conexión con credenciales
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqps://kefeokrc:zlN1XAhEY_KL2KmZtS6tYqJY6hMs_imx@turkey.rmq.cloudamqp.com/kefeokrc")
            };

            // Crear una conexión
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declarar y configurar la cola
                channel.QueueDeclare(queue: "desired_queue_name",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Configurar el consumidor
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Received message: {message}");

                    await Task.Run(() => channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false));
                };

                // Iniciar la consumición de mensajes de la cola
                channel.BasicConsume(queue: "desired_queue_name",
                                     autoAck: false,
                                     consumer: consumer);

                Console.WriteLine("Consumer started. Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
