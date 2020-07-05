using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Linq;

namespace Sharpbot.Data
{
    public class StrikeService
    {
        private static string npgsqlConnectionString = "";

        public static List<Strike> GetStrikes(ulong guildId, ulong userId)
        {
            using (var connection = new NpgsqlConnection(npgsqlConnectionString))
            {
                connection.Open();
                return connection.Query<Strike>($"select * from getstrikes('{guildId}', '{userId}')").ToList();
            }
        }

        public static void AddStrike(ulong guildId, ulong userId, ulong modId, string reason, string date)
        {
            using (var connection = new NpgsqlConnection(npgsqlConnectionString))
            {
                connection.Open();
                connection.Execute($"select addstrike('{guildId}', '{userId}', '{modId}', '{reason}', '{date}')");
            }
        }

        public static void RemoveStrike(int strikeId)
        {
            using (var connection = new NpgsqlConnection(npgsqlConnectionString))
            {
                connection.Open();
                connection.Execute($"select removestrike({strikeId})");
            }
        }
    }
}
