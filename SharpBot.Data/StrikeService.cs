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
        public static List<Strike> GetStrikes()
        {
            using (var connection = new NpgsqlConnection("connection string"))
            {
                connection.Open();
                return connection.Query<Strike>("Select * from strikes").ToList();
            }
        }

        public static void AddStrike(ulong userId, ulong modId, string reason, string date)
        {
            using (var connection = new NpgsqlConnection("connection string"))
            {
                connection.Open();
                connection.Execute($"INSERT INTO strikes(userid, modid, reason, date) VALUES('{userId}', '{modId}', '{reason}', '{date}')");
            }
        }
    }
}
