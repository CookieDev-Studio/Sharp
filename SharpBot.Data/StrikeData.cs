using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Linq;

namespace SharpBot.Data
{
    public class StrikeData
    {
        public List<Strike> GetStrikes(ulong guildId, ulong userId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return connection.Query<Strike>($"select * from get_strikes('{guildId}', '{userId}')").ToList();
        }

        public void AddStrike(ulong guildId, ulong userId, ulong modId, string reason, string date)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.Execute($"select add_strike('{guildId}', '{userId}', '{modId}', '{reason}', '{date}')");
        }

        public void RemoveStrike(int strikeId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.Execute($"select remove_strike({strikeId})");
        }

        public void RemoveAllStrikesFromUser(ulong guildId, ulong userId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.Execute($"select remove_all_strikes('{guildId}', '{userId}')");
        }
    }
}
