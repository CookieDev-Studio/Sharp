using Dapper;
using Npgsql;
using System;
using System.IO;
using System.Linq;

namespace SharpBot.Data
{
    public class GuildService
    {
        private string npgsqlConnectionString;

        public GuildService()
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

        public Config GetGuildConfig(ulong guildId)
        {
            using (var connection = new NpgsqlConnection(npgsqlConnectionString))
            {
                connection.Open();
                return connection.Query<Config>($"select * from get_config('{guildId}')").First();
            }
        }

        public void AddConfig(ulong guildId, ulong modChannelId)
        {
            using (var connection = new NpgsqlConnection(npgsqlConnectionString))
            {
                connection.Open();
                connection.Execute($"select add_config('{guildId}', '{modChannelId}')");
            }
        }

        public void SetModChannel(ulong guildId, ulong modChannelId)
        {
            using (var connection = new NpgsqlConnection(npgsqlConnectionString))
            {
                connection.Open();
                connection.Execute($"select set_mod_channel_id('{guildId}', '{modChannelId}')");
            }
        }

    }
}
