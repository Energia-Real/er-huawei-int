using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

public class Program
{
    

    public static async Task Main(string[] args)
    {
        // Configuración
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        //Subscriptions
        builder.Services.AddTransient<PlantDeviceResultEventHandler>();

        var app = builder.Build();
    }

    
}