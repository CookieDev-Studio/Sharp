using SharpBot.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBot.Service
{
    public class StrikeService
    {
        readonly StrikeData _strikeData;

        public StrikeService(StrikeData strikeData)
        {
            _strikeData = strikeData;
        }

        public Task SaveStrikeAsync(ulong guildId, ulong userId, ulong modId, string reason, string date)
        {
            return _strikeData.AddStrikeAsync(guildId, userId, modId, reason, date);
        }

        public Task<List<Strike>> LoadStrikesAsync(ulong guildId, ulong userId)
        {
            return Task.FromResult(_strikeData.GetStrikesAsync(guildId, userId).Result.Where(x => ulong.Parse(x.guildId) == guildId && ulong.Parse(x.userId) == userId).Select(x =>
                new Strike()
                {
                    Id = x.Id,
                    user = ulong.Parse(x.userId),
                    mod = ulong.Parse(x.modId),
                    reason = x.reason,
                    date = x.date
                }).ToList());
        }

        public Task RemoveStrikeAsync(int strikeId)
        {
            return _strikeData.RemoveStrikeAsync(strikeId);
        }

        public Task RemoveAllStrikesFromUserAsync(ulong userId, ulong guildId)
        {
            return _strikeData.RemoveAllStrikesFromUserAsync(guildId, userId);
        }
    }
}