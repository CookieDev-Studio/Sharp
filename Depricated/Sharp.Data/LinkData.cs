using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Data.Depricated
{
    public class LinkData : ILinkData
    {
        public async Task AddLinkRolePair(ulong guildId, string linkCode, ulong roleId, int uses)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            await connection.ExecuteAsync($"SELECT add_link_role_pair('{guildId}', '{linkCode}', '{roleId}', {uses})");
        }
        public async Task<List<Link>> GetLinkRolePairsAsync(ulong guildId)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            var querryResult = await connection.QueryAsync<Link>($"SELECT * FROM link_role_pair WHERE guild_id = '{guildId}'");
            return querryResult.ToList();
        }

        public async Task UpdateUsesAsync(string code, int uses)
        {
            using var connection = DataExtentions.GetConnection();
            connection.Open();
            await connection.ExecuteAsync($"SELECT update_link_uses('{code}', {uses})");
        }
    }
}
