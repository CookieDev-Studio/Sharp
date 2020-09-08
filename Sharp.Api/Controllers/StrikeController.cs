using Microsoft.AspNetCore.Mvc;
using Sharp.Service;
using Sharp.Domain;
using System.Threading.Tasks;

namespace Sharp.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StrikeController : ControllerBase
    {
        [HttpGet("{guildId}/{userId}")]
        public async Task<ActionResult> GetStrikes(ulong guildId, ulong userId)
        {
            return Ok( await StrikeService.getStrikesAsync(GuildId.NewGuildId(guildId), UserId.NewUserId(userId)));
        }

        [HttpDelete("{guildId}")]
        public ActionResult RemoveStrike(ulong guildId, [FromBody] StrikeId strikeId)
        {
            StrikeService.removeStrike(GuildId.NewGuildId(guildId), strikeId.Id);
            return Ok($"Strike id: {strikeId.Id} was removed");
        }

        [HttpDelete("{guildId}/{userId}")]
        public ActionResult RemoveAllStrikes(ulong guildId, ulong userId)
        {
            StrikeService.removeAllStrikesFromUser(GuildId.NewGuildId(guildId), UserId.NewUserId(userId));
            return Ok($"strikes removed from user {userId}");
        }
    }
}
