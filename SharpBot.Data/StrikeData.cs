using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBot.Data
{
    public class StrikeData
    {
        public Task<List<Strike>> GetStrikesAsync(ulong guildId, ulong userId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return Task.FromResult(connection.QueryAsync<Strike>($"select * from get_strikes('{guildId}', '{userId}')").Result.ToList());
        }

        public Task AddStrikeAsync(ulong guildId, ulong userId, ulong modId, string reason, string date)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return connection.ExecuteAsync($"select add_strike('{guildId}', '{userId}', '{modId}', '{reason}', '{date}')");
        }

        public Task RemoveStrikeAsync(int strikeId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return connection.ExecuteAsync($"select remove_strike({strikeId})");
        }

        public Task RemoveAllStrikesFromUserAsync(ulong guildId, ulong userId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return connection.ExecuteAsync($"select remove_all_strikes('{guildId}', '{userId}')");
        }
    }
}
