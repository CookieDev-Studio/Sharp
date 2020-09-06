using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharp.Data.Depricated
{
    public class StrikeData : IStrikeData
    {
        public List<Strike> GetStrikes(ulong guildId, ulong userId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return connection.Query<Strike>($"select * from get_strikes('{guildId}', '{userId}')").ToList();
        }
        public async Task<List<Strike>> GetStrikesAsync(ulong guildId, ulong userId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            return await Task.FromResult(connection.QueryAsync<Strike>($"select * from get_strikes('{guildId}', '{userId}')").Result.ToList());
        }

        public void AddStrike(ulong guildId, ulong userId, ulong modId, string reason, string date)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.Execute($"select add_strike('{guildId}', '{userId}', '{modId}', '{reason}', '{date}')");
        }
        public async Task AddStrikeAsync(ulong guildId, ulong userId, ulong modId, string reason, string date)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            await connection.ExecuteAsync($"select add_strike('{guildId}', '{userId}', '{modId}', '{reason}', '{date}')");
        }

        public void RemoveStrike(ulong guildId, int strikeId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.ExecuteAsync($"select remove_strike('{guildId}', {strikeId})");
        }
        public async Task RemoveStrikeAsync(ulong guildId, int strikeId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            await connection.ExecuteAsync($"select remove_strike('{guildId}', {strikeId})");
        }

        public void RemoveAllStrikesFromUser(ulong guildId, ulong userId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            connection.ExecuteAsync($"select remove_all_strikes('{guildId}', '{userId}')");
        }
        public async Task RemoveAllStrikesFromUserAsync(ulong guildId, ulong userId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            await connection.ExecuteAsync($"select remove_all_strikes('{guildId}', '{userId}')");
        }
    }
}
