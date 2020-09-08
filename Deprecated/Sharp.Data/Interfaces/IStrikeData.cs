using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sharp.Data.Deprecated
{
    public interface IStrikeData
    {
        void AddStrike(ulong guildId, ulong userId, ulong modId, string reason, string date);
        Task AddStrikeAsync(ulong guildId, ulong userId, ulong modId, string reason, string date);
        List<Strike> GetStrikes(ulong guildId, ulong userId);
        Task<List<Strike>> GetStrikesAsync(ulong guildId, ulong userId);
        void RemoveAllStrikesFromUser(ulong guildId, ulong userId);
        Task RemoveAllStrikesFromUserAsync(ulong guildId, ulong userId);
        void RemoveStrike(ulong guildId, int strikeId);
        Task RemoveStrikeAsync(ulong guildId, int strikeId);
    }
}