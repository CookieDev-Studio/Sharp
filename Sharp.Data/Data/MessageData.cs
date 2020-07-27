using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Data
{
    public class MessageData : IMessageData
    { 
        public List<Message> GetMessages(ulong guildId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            var a = connection.Query<Message>($"SELECT * FROM message WHERE guild_id = '{guildId}'").ToList();
            return connection.Query<Message>($"SELECT * FROM message WHERE guild_id = '{guildId}'").ToList();
        }
        public Task<List<Message>> GetMessagesAsync(ulong guildId)
        {
            throw new NotImplementedException();
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
