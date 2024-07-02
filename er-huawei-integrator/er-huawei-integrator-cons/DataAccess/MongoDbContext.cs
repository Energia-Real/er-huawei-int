using er_huawei_integrator_cons.Application.Model.Dto;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace er_huawei_integrator_cons.DataAccess
{
    public class MongoDbContext
    {
        private readonly IConfiguration _configuration;

        private readonly MongoClient _MongoClient;
        private IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration["MongoDb"];
            var dataBase = _configuration["DataBases:MongoGWDataBase"];

            _MongoClient = new MongoClient(connectionString);
            _database = _MongoClient.GetDatabase(dataBase);
            _configuration = configuration;
        }

        public async Task InsertDeviceDataAsync(PlantDeviceResult device)
        {
            try
            {
                var collection = _database.GetCollection<PlantDeviceResult>("RepliRealtimeData");
                device.repliedDateTime = DateTime.Now;

                // Insertar el dispositivo en la colección
                await collection.InsertOneAsync(device);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar en la base de datos: {ex.Message}");
            }
        }
    }
}
