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

        public void SaveStrike(ulong guildId, ulong userId, ulong modId, string reason, string date) => _strikeData.AddStrike(guildId, userId, modId, reason, date);
        public Task SaveStrikeAsync(ulong guildId, ulong userId, ulong modId, string reason, string date) => _strikeData.AddStrikeAsync(guildId, userId, modId, reason, date);

        public void RemoveStrike(int strikeId) => _strikeData.RemoveStrike(strikeId);
        public Task RemoveStrikeAsync(int strikeId) => _strikeData.RemoveStrikeAsync(strikeId);

        public void RemoveAllStrikesFromUser(ulong userId, ulong guildId) => _strikeData.RemoveAllStrikesFromUser(guildId, userId);
        public Task RemoveAllStrikesFromUserAsync(ulong userId, ulong guildId) => _strikeData.RemoveAllStrikesFromUserAsync(guildId, userId);

        public List<Strike> LoadStrikes(ulong guildId, ulong userId)
        {
            return _strikeData.GetStrikesAsync(guildId, userId).Result.Where(x => ulong.Parse(x.guildId) == guildId && ulong.Parse(x.userId) == userId).Select(x =>
                new Strike()
                {
                    Id = x.Id,
                    user = ulong.Parse(x.userId),
                    mod = ulong.Parse(x.modId),
                    reason = x.reason,
                    date = x.date
                }).ToList();
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
    }
}