using Dapper;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sharp.Data.Depricated
{
    public class GuildData : IGuildData
    {
        public Config GetGuildConfig(ulong guildId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return connection.QuerySingle<Config>($"select * from get_config('{guildId}')");
        }
        public async Task<Config> GetGuildConfigAsync(ulong guildId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return await connection.QuerySingleAsync<Config>($"select * from get_config('{guildId}')");
        }

        public void AddConfig(ulong guildId, ulong ModchannelId, char prefix, bool messagelog)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.Execute($"select * from add_config('{guildId}', '{ModchannelId}', '{prefix}', {messagelog})");
        }
        public async Task AddConfigAsync(ulong guildId, ulong ModchannelId, char prefix, bool messagelog)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            await connection.ExecuteAsync($"select * from add_config('{guildId}', '{ModchannelId}', '{prefix}', {messagelog})");
        }

        public string[] GetAllGuilds()
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return connection.Query<string>($"SELECT guild_id FROM config").ToArray();
        }
        public async Task<string[]> GetAllGuildsAsync()
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return await Task.FromResult(connection.QueryAsync<string>($"SELECT guild_id FROM config").Result.ToArray());
        }

        public void SetModChannel(ulong guildId, ulong modChannelId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.Execute($"select set_mod_channel_id('{guildId}', '{modChannelId}')");
        }
        public async Task SetModChannelAsync(ulong guildId, ulong modChannelId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            await connection.ExecuteAsync($"select set_mod_channel_id('{guildId}', '{modChannelId}')");
        }

        public void SetPrefix(ulong guildId, char prefix)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.Execute($"select set_prefix('{guildId}', '{prefix}')");
        }
        public async Task SetPrefixAsync(ulong guildId, char prefix)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            await connection.ExecuteAsync($"select set_prefix('{guildId}', '{prefix}')");
        }

        public void SetMessageLog(ulong guildId, bool value)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.ExecuteAsync($"select set_message_log('{guildId}', {value})");
        }
        public async Task SetMessageLogAsync(ulong guildId, bool value)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            await connection.ExecuteAsync($"select set_message_log('{guildId}', {value})");
        }
    }
}
