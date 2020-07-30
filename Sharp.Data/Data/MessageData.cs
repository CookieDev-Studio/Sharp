using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharp.Data
{
    public class MessageData : IMessageData
    { 
        public List<Message> GetMessages(ulong guildId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return connection.Query<Message>($"SELECT * FROM message WHERE guild_id = '{guildId}'").ToList();
        }
        public async Task<List<Message>> GetMessagesAsync(ulong guildId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return await Task.FromResult(connection.QueryAsync<Message>($"SELECT * FROM message WHERE guild_id = '{guildId}'").Result.ToList());
        }

        public void AddMessage(ulong guildId, ulong modChannelId, ulong userId, string message, DateTime dateTime)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.ExecuteAsync($"select add_message('{guildId}', '{modChannelId}', '{userId}', E'{message}', '{dateTime}')");
        }
        public async Task AddMessageAsync(ulong guildId, ulong modChannelId, ulong userId, string message, DateTime dateTime)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            await connection.ExecuteAsync($"select add_message('{guildId}', '{modChannelId}', '{userId}', E'{message}', '{dateTime}')");
        }
    }
}
