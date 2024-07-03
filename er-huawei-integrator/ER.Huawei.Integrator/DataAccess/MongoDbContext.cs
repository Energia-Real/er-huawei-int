using ER.Huawei.Integrator.Domain.Events;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ER.Huawei.Integrator.Cons.DataAccess
{
    public class MongoDbContext
    {
        private readonly IConfiguration _configuration;

        private readonly MongoClient _MongoClient;
        private IMongoDatabase _database;

        public MongoDbContext()
        {
            var connectionString = "mongodb+srv://cloudservices:PtbsrhOZbWW6qlCT@er-closter.yhbgqe4.mongodb.net/";
            var dataBase = "er-gigawatt-develop";

            _MongoClient = new MongoClient(connectionString);
            _database = _MongoClient.GetDatabase(dataBase);
        }

        public async Task InsertDeviceDataAsync(PlantDeviceResultEvent device)
        {
            try
            {
                var collection = _database.GetCollection<PlantDeviceResultEvent>("RepliRealtimeData");
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
