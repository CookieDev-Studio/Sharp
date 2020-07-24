using System;
using System.IO;
using Npgsql;

namespace SharpBot.Data
{
    internal class DataExtentions
    {
        public static NpgsqlConnection GetConnection()
        {
            string connectionString = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "SqlConnectionString.txt"));
            //string connectionString = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "SqlConnectionString.txt"));
            return new NpgsqlConnection(connectionString);
        }
    }
}
