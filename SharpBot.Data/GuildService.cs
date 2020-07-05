using Dapper;
using Npgsql;
using System.Linq;

namespace SharpBot.Data
{
    public class GuildService
    {
        private static string npgsqlConnectionString = "";

        public static Config GetGuildConfig(ulong guildId)
        {
            using (var connection = new NpgsqlConnection(npgsqlConnectionString))
            {
                connection.Open();
                return connection.Query<Config>($"select * from getconfig('{guildId}')").First();
            }
        }

        public static void SetModChannel(ulong guildId, ulong modChannelId)
        {
            using (var connection = new NpgsqlConnection(npgsqlConnectionString))
            {
                connection.Open();
                connection.Execute($"select setmodchannelid('{guildId}', '{modChannelId}')");
            }
        }
    }
}
