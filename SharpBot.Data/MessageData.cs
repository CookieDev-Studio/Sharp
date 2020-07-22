using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBot.Data
{
    public class MessageData
    {
        public void AddMessage(ulong guildId, ulong modChannelId, ulong userId, string message, DateTime dateTime)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.Execute($"select add_message('{guildId}', '{modChannelId}', '{userId}', E'{message}', '{dateTime}')");
        }
    }
}
