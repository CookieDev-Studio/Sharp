using Microsoft.AspNetCore.Mvc;
using Sharp.Service;
using Sharp.Domain;

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
