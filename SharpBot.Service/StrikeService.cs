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

        public Task SaveStrike(ulong guildId, ulong userId, ulong modId, string reason, string date)
        {
            return _strikeData.AddStrike(guildId, userId, modId, reason, date);
        }

        public Task<List<Strike>> LoadStrikes(ulong guildId, ulong userId)
        {
            return Task.FromResult(_strikeData.GetStrikes(guildId, userId).Result.Where(x => ulong.Parse(x.guildId) == guildId && ulong.Parse(x.userId) == userId).Select(x =>
                new Strike()
                {
                    Id = x.Id,
                    user = ulong.Parse(x.userId),
                    mod = ulong.Parse(x.modId),
                    reason = x.reason,
                    date = x.date
                }).ToList());
        }

        public Task RemoveStrike(int strikeId)
        {
            return _strikeData.RemoveStrike(strikeId);
        }

        public Task RemoveAllStrikesFromUser(ulong userId, ulong guildId)
        {
            return _strikeData.RemoveAllStrikesFromUser(guildId, userId);
        }
    }
}