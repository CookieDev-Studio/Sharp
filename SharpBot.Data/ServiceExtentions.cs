using System;
using System.IO;

namespace SharpBot.Data
{
    internal class ServiceExtentions
    {
        public static string GetConnectionString()
        {
            string connectionString;

            try { connectionString = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "SqlConnectionString.txt")); }
            catch
            {
                Console.WriteLine("Enter sql Conection String:");
                connectionString = Console.ReadLine();
                File.WriteAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "SqlConnectionString.txt"), connectionString);
            }
            return connectionString;
        }
    }
}
