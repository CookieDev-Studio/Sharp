using System;
using System.IO;
using Newtonsoft.Json;
using Npgsql;

namespace Sharp.Data
{
    public class DataExtentions
    {
        public static NpgsqlConnection GetConnection()
        {
            var dbConnection = JsonConvert.DeserializeObject<DbConnection>(
                File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "SqlConnection.json")));
            
            return new NpgsqlConnection(
                $"Host={dbConnection.Host};" +
                $"Username={dbConnection.Username};" +
                $"Password={dbConnection.Password};" +
                $"Database={dbConnection.Database};" +
                $"sslmode=Require;" +
                $"Trust Server Certificate=true");
        }
    }
}
