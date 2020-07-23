using Dapper;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpBot.Data
{
    public class GuildData
    {
        public Task<Config> GetGuildConfigAsync(ulong guildId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return connection.QuerySingleAsync<Config>($"select * from get_config('{guildId}')");
        }

        public Task SetModChannelAsync(ulong guildId, ulong modChannelId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return connection.ExecuteAsync($"select set_mod_channel_id('{guildId}', '{modChannelId}')");
        }

        public Task SetPrefixAsync(ulong guildId, char prefix)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return connection.ExecuteAsync($"select set_prefix('{guildId}', '{prefix}')");
        }

        public Task SetMessageLogAsync(ulong guildId, bool value)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return connection.ExecuteAsync($"select set_message_log('{guildId}', {value})");
        }
    }
}
