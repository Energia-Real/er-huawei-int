using ER.Huawei.Integrator.Cons.DataAccess;
using ER.Huawei.Integrator.Cons.Domain.Core.Bus;
using ER.Huawei.Integrator.Cons.Model.Dto;
using ER.Huawei.Integrator.Domain.Events;
using MongoDB.Bson;

namespace ER.Huawei.Integrator.Domain.EventHandlers;

public class PlantDeviceResultEventHandler : IEventHandler<PlantDeviceResultEvent>
{
    public PlantDeviceResultEventHandler()
    {
    }

    public async Task Handle(PlantDeviceResultEvent @event)
    {
        await ProcessQueueAsync(@event);

    }

    private static async Task ProcessQueueAsync(PlantDeviceResultEvent deviceRequest)
    {

        if (deviceRequest is null)
            return;


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
        var dbContext = new PostgresDbContext();
        var mdbContext = new MongoDbContext();
        await dbContext.InsertDeviceAsync(mappTopg);

        await mdbContext.InsertDeviceDataAsync(deviceRequest);

        // Simulación de procesamiento asíncrono
        Console.WriteLine($"Processed message: {deviceRequest.ToJson()}");
        await Task.CompletedTask;
    }

    public static Guid ToGuid(ObjectId objectId)
    {
        var bytes = objectId.ToByteArray();
        Array.Resize(ref bytes, 16);
        return new Guid(bytes);
    }
}
