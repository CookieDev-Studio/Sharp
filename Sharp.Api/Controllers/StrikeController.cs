using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sharp.Service;

namespace Sharp.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StrikeController : ControllerBase
    {
        readonly StrikeService _strikeService;
        public StrikeController(StrikeService strikeService)
        {
            _strikeService = strikeService;
        }

        [HttpGet("{guildId}/{userId}")]
        public ActionResult GetStrikes(ulong guildId, ulong userId)
        {
            return Ok(_strikeService.GetStrikes(guildId, userId));
        }

        [HttpDelete("{guildId}")]
        public ActionResult RemoveStrike(ulong guildId, [FromBody] StrikeId strikeId)
        {
            _strikeService.RemoveStrike(guildId, strikeId.Id);
            return Ok($"Strike id: {strikeId.Id} was removed");
        }

        [HttpDelete("{guildId}/{userId}")]
        public ActionResult RemoveAllStrikes(ulong guildId, ulong userId)
        {
            _strikeService.RemoveAllStrikesFromUser(guildId, userId);
            return Ok($"strikes removed from user {userId}");
        }
    }
}
