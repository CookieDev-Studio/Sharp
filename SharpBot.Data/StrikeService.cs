using Dapper;
using Discord.WebSocket;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharpbot.data 
{
    public class StrikeService
    {
        public static List<Strike> GetStrikes(ulong guildId, ulong userId)
        {
            using (var connection = new NpgsqlConnection(""))
            {
                connection.Open();
                return connection.Query<Strike>($"select * from getstrikes('{guildId}', '{userId}')").ToList();
                
            }
        }

        public static void AddStrike(ulong guildId, ulong userId, ulong modId, string reason, string date)
        {
            using (var connection = new NpgsqlConnection(""))
            {
                connection.Open();
                connection.Execute($"select addstrike('{guildId}', '{userId}', '{modId}', '{reason}', '{date}')");
            }
        }
    }
}
