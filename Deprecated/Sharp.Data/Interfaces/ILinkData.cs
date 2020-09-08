using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sharp.Data.Deprecated
{
    public interface ILinkData
    {
        Task AddLinkRolePair(ulong guildId, string linkCode, ulong roleId, int uses);
        Task<List<Link>> GetLinkRolePairsAsync(ulong guildId);
        Task UpdateUsesAsync(string code, int uses);
    }
}