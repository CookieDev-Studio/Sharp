using Microsoft.AspNetCore.Mvc;
using Sharp.Service;
using Sharp.Domain;
using System.Threading.Tasks;

namespace Sharp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageLogController : ControllerBase
    {
        [HttpGet("{guildId}")]
        public async Task<ActionResult> GetMessages(ulong guildId) => Ok(await MessageService.getMessagesAsync(GuildId.NewGuildId(guildId)));
    }
}
