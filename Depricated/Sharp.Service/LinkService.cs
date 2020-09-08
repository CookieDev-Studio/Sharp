using Sharp.Data.Depricated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Service.Deprecated
{
    public class LinkService
    {
        readonly ILinkData _linkData;
        public LinkService(ILinkData linkData) => _linkData = linkData;

        public Task AddLinkRolePairAsync(ulong guildId, string linkCode, ulong roleId, int uses)
            => _linkData.AddLinkRolePair(guildId, linkCode, roleId, uses);
        
        public Task UpdateUsesAsync(string code, int uses)
            => _linkData.UpdateUsesAsync(code, uses);

        public Task<List<Link>> GetLinkRolePairsAsync(ulong guildId)
        {
            return Task.FromResult(_linkData.GetLinkRolePairsAsync(guildId).Result
                .Select(x => new Link()
                {
                    GuildId = ulong.Parse(x.guild_id),
                    Code = x.code,
                    RoleId = ulong.Parse(x.role_id),
                    Uses = x.uses
                }).ToList());
        }
    }
}
