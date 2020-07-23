using System;
using System.IO;
using Npgsql;

namespace SharpBot.Data
{
    internal class DataExtentions
    {
        public static NpgsqlConnection GetConnection()
        {
            string connectionString;

            try { connectionString = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "SqlConnectionString.txt")); }
            catch
            {
                Console.WriteLine("Enter sql Conection String:");
                connectionString = Console.ReadLine();
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "SqlConnectionString.txt"), connectionString);
            }
            return new NpgsqlConnection(connectionString);
        }
    }
}
