using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharpBot.Data
{
    public class StrikeService
    {
        private string npgsqlConnectionString;

        public StrikeService()
        {
            try { npgsqlConnectionString = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "SqlConnectionString.txt")); }
            catch
            {
                Console.WriteLine("Enter sql Conection String:");
                string connectionString = Console.ReadLine();
                File.WriteAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "SqlConnectionString.txt"), connectionString);
                npgsqlConnectionString = connectionString;
            }
        }

        public List<Strike> GetStrikes(ulong guildId, ulong userId)
        {
            using (var connection = new NpgsqlConnection(npgsqlConnectionString))
            {
                connection.Open();
                return connection.Query<Strike>($"select * from get_strikes('{guildId}', '{userId}')").ToList();
            }
        }

        public void AddStrike(ulong guildId, ulong userId, ulong modId, string reason, string date)
        {
            using (var connection = new NpgsqlConnection(npgsqlConnectionString))
            {
                connection.Open();
                connection.Execute($"select add_strike('{guildId}', '{userId}', '{modId}', '{reason}', '{date}')");
            }
        }

        public void RemoveStrike(int strikeId)
        {
            using (var connection = new NpgsqlConnection(npgsqlConnectionString))
            {
                connection.Open();
                connection.Execute($"select remove_strike({strikeId})");
            }
        }
    }
}
