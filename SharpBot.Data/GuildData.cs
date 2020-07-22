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
        public Config GetGuildConfig(ulong guildId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return connection.Query<Config>($"select * from get_config('{guildId}')").First();
        }

        public void SetModChannel(ulong guildId, ulong modChannelId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.Execute($"select set_mod_channel_id('{guildId}', '{modChannelId}')");
        }

        public void SetPrefix(ulong guildId, char prefix)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.Execute($"select set_prefix('{guildId}', '{prefix}')");
        }

        public void SetMessageLog(ulong guildId, bool value)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.Execute($"select set_message_log('{guildId}', {value})");
        }
    }
}
