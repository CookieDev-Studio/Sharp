using System;
using System.IO;
using Newtonsoft.Json;
using Npgsql;

namespace SharpBot.Data
{
    internal class DataExtentions
    {
        public static NpgsqlConnection GetConnection()
        {
            var dbConnection = JsonConvert.DeserializeObject<DbConnection>(
                File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "SqlConnectionString.json")));
            
            return new NpgsqlConnection(
                $"Host={dbConnection.Host};" +
                $"Username={dbConnection.Username};" +
                $"Password={dbConnection.Password};" +
                $"Database={dbConnection.Database};" +
                $"sslmode=Require;Trust Server Certificate=true");
        }
    }
}
