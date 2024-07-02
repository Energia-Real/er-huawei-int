namespace er_huawei_integrator_cons.DataAccess
{
    using System;
    using Npgsql;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using er_huawei_integrator_cons.Model.Dto;

    public class PostgresDbContext
    {
        private readonly string? _connectionString;

        public PostgresDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgressConnection");
        }

        public async Task InsertDeviceAsync(RepliMessagesDto message)
        {
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var query = @"INSERT INTO devices (Id, brandName, stationCode, TypeMensaje, horaConsulta, horaLlegoConsolidador, fechaConsolido, Estatus) 
                          VALUES (@Id, @brandName, @stationCode, @typeMensaje, @horaConsulta, @horaLlegoConsolidador, @fechaConsolido, @estatus)";

                await using var cmd = new NpgsqlCommand(query, connection);
                cmd.Parameters.AddWithValue("Id", message.Id);
                cmd.Parameters.AddWithValue("brandName", message.BrandName);
                cmd.Parameters.AddWithValue("stationCode", message.StationCode);
                cmd.Parameters.AddWithValue("typeMensaje", message.TypeMensaje);
                cmd.Parameters.AddWithValue("horaConsulta", message.HoraConsulta);
                cmd.Parameters.AddWithValue("horaLlegoConsolidador", message.HoraLlegoConsolidador);
                cmd.Parameters.AddWithValue("fechaConsolido", message.FechaConsolido);
                cmd.Parameters.AddWithValue("estatus", message.Estatus);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar el dispositivo: {ex.Message}");
            }
        }
    }
}
