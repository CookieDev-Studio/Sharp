using SharpBot.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBot.Service
{
    public class StrikeService
    {
        readonly StrikeData _strikeData;

        public StrikeService(StrikeData strikeData) => _strikeData = strikeData;

        /// <summary>
        /// Adds a strike to a user
        /// </summary>
        /// <param name="guildId"></param>
        /// <param name="userId"></param>
        /// <param name="modId"></param>
        /// <param name="reason"></param>
        /// <param name="date"></param>
        public void AddStrike(ulong guildId, ulong userId, ulong modId, string reason, string date) => _strikeData.AddStrike(guildId, userId, modId, reason, date);
        public Task AddStrikeAsync(ulong guildId, ulong userId, ulong modId, string reason, string date) => _strikeData.AddStrikeAsync(guildId, userId, modId, reason, date);

        /// <summary>
        /// Removes a strike from a user
        /// </summary>
        /// <param name="strikeId"></param>
        public void RemoveStrike(int strikeId) => _strikeData.RemoveStrike(strikeId);
        public Task RemoveStrikeAsync(int strikeId) => _strikeData.RemoveStrikeAsync(strikeId);

        /// <summary>
        /// Removes all strikes from the user in the specified guild
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="guildId"></param>
        public void RemoveAllStrikesFromUser(ulong userId, ulong guildId) => _strikeData.RemoveAllStrikesFromUser(guildId, userId);
        public Task RemoveAllStrikesFromUserAsync(ulong userId, ulong guildId) => _strikeData.RemoveAllStrikesFromUserAsync(guildId, userId);

        /// <summary>
        /// Gets all strikes logged against the user for the specified guild
        /// </summary>
        /// <param name="guildId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Strike> GetStrikes(ulong guildId, ulong userId)
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
        public Task<List<Strike>> GetStrikesAsync(ulong guildId, ulong userId)
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