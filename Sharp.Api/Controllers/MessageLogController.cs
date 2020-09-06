using Microsoft.AspNetCore.Mvc;
using Sharp.FSharp.Service;
using Sharp.FSharp.Domain;

namespace Sharp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageLogController : ControllerBase
    {
        [HttpGet("{guildId}")]
        public ActionResult GetMessages(ulong guildId) => Ok(MessageService.getMessages(GuildId.NewGuildId(guildId)));
    }
}
