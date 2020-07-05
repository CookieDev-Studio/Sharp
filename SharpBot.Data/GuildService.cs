using Dapper;
using Npgsql;
using System;
using System.IO;
using System.Linq;

namespace SharpBot.Data
{
    public class GuildService
    {
        private readonly string connectionString;

        public GuildService() => connectionString = ServiceExtentions.GetConnectionString();

        public Config GetGuildConfig(ulong guildId)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection.Query<Config>($"select * from get_config('{guildId}')").First();
        }

        public void AddConfig(ulong guildId, ulong modChannelId)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            connection.Execute($"select add_config('{guildId}', '{modChannelId}')");
        }

        public void SetModChannel(ulong guildId, ulong modChannelId)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            connection.Execute($"select set_mod_channel_id('{guildId}', '{modChannelId}')");
        }

    }
}
