using Microsoft.AspNetCore.Mvc;
using Sharp.Domain;
using Sharp.Service;

namespace SharpApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        [HttpGet("{guildId}")]
        public ActionResult<GuildConfig> Get(ulong guildId)
        {
            return Ok(GuildConfigService.getConfig(GuildId.NewGuildId(guildId)));
        }

        [HttpPut("modchannel/{guildId}/{modchannelId}")]
        public ActionResult SetModChannel(ulong guildId, ulong modChannelId)
        {
            GuildConfigService.setModChannel(GuildId.NewGuildId(guildId), ModChannelId.NewModChannelId(modChannelId));
            return Ok($"mod channel set to {modChannelId}");
        }

        [HttpPut("prefix/{guildId}")]
        public ActionResult SetPrefix(ulong guildId, [FromBody] Prefix prefix)
        {
            GuildConfigService.setPrefix(GuildId.NewGuildId(guildId), prefix.prefix);
            return Ok($"Prefix set to {prefix.prefix}");
        }
        [HttpPut("messagelog/{guildId}/{value}")]
        public ActionResult SetMessageLog(ulong guildId, bool value)
        {
            GuildConfigService.setMessageLog(GuildId.NewGuildId(guildId), value);

            if (value)
                return Ok("Message log enabled");
            else
                return Ok("Message log disabled");
        }
    }
}
