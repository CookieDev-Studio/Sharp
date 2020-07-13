using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBot.Data
{
    public class MessageService
    {
        private readonly string connectionString;
        public MessageService() => connectionString = ServiceExtentions.GetConnectionString();

        public void AddMessage(ulong guildId, ulong modChannelId, ulong userId, string message, DateTime dateTime)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            connection.Execute($"select add_message('{guildId}', '{modChannelId}', '{userId}', '{message}', '{dateTime}')");
        }
    }
}
