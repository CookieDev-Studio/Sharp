using Dapper;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Linq;
using System.Net.Http;

namespace SharpBot.Data
{
    public class GuildData
    {
        private readonly string connectionString;

        public GuildData() => connectionString = DataExtentions.GetConnectionString();

        public Config GetGuildConfig(ulong guildId)
        {
            /*
            using var client = new HttpClient();
            string responseString = client.GetAsync($"https://localhost:5001/api/config/{guildId}").Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Config>(responseString);
            */
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
