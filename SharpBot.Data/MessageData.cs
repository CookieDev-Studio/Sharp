using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBot.Data
{
    public class MessageData
    {
        public void AddMessage(ulong guildId, ulong modChannelId, ulong userId, string message, DateTime dateTime)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.ExecuteAsync($"select add_message('{guildId}', '{modChannelId}', '{userId}', E'{message}', '{dateTime}')");
        }
        public Task AddMessageAsync(ulong guildId, ulong modChannelId, ulong userId, string message, DateTime dateTime)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return connection.ExecuteAsync($"select add_message('{guildId}', '{modChannelId}', '{userId}', E'{message}', '{dateTime}')");
        }
    }
}
