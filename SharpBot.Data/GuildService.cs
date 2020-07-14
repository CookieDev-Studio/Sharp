using Dapper;
using Npgsql;
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

        public void SetModChannel(ulong guildId, ulong modChannelId)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            connection.Execute($"select set_mod_channel_id('{guildId}', '{modChannelId}')");
        }

        public void SetPrefix(ulong guildId, char prefix)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            connection.Execute($"select set_prefix('{guildId}', '{prefix}')");
        }

        public void SetMessageLog(ulong guildId, bool value)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            connection.Execute($"select set_message_log('{guildId}', {value})");
        }
    }
}
