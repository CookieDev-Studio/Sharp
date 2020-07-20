using SharpBot.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBot.Service
{
    public class StrikeService
    {
        readonly StrikeData _strikeService;

        public StrikeService(StrikeData strikeService)
        {
            _strikeService = strikeService;

           
        }

        public Task SaveStrike(ulong guildId, ulong userId, ulong modId, string reason, string date)
        {
            _strikeService.AddStrike(guildId, userId, modId, reason, date);
            return Task.CompletedTask;
        }

        public List<Strike> LoadStrikes(ulong guildId, ulong userId)
        {
            return _strikeService.GetStrikes(guildId, userId).Where(x => ulong.Parse(x.guildId) == guildId && ulong.Parse(x.userId) == userId).Select(x =>
                new Strike()
                {
                    Id = x.Id,
                    user = ulong.Parse(x.userId),
                    mod = ulong.Parse(x.modId),
                    reason = x.reason,
                    date = x.date
                }).ToList();
        }

        public Task RemoveStrike(int strikeId)
        {
            _strikeService.RemoveStrike(strikeId);
            return Task.CompletedTask;
        }

        public Task RemoveAllStrikesFromUser(ulong userId, ulong guildId)
        {
            _strikeService.RemoveAllStrikesFromUser(guildId, userId);
            return Task.CompletedTask;
        }
    }
}