using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharpBot.Service;
using Npgsql;

namespace WebApiTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        readonly GuildService _guildService;

        public ConfigController(GuildService configService)
        {
            _guildService = configService;
        }

        [HttpGet("{guildId}")]
        public ActionResult<Config> Get(ulong guildId)
        {
            return Ok(_guildService.GetConfig(guildId));
        }

        [HttpPut("modchannel/{guildId}/{modchannelId}")]
        public ActionResult SetModChannel(ulong guildId, ulong modChannelId)
        {
            _guildService.SetModChannel(guildId, modChannelId);
            return Ok($"mod channel set to {modChannelId}");
        }

        [HttpPut("prefix/{guildId}")]
        public ActionResult SetPrefix(ulong guildId, [FromBody] Prefix prefix)
        {
            _guildService.SetPrefix(guildId, prefix.prefix);
            return Ok($"Prefix set to {prefix.prefix}");
        }
        [HttpPut]
        public ActionResult SetMessageLog(ulong guildId, bool value)
        {
            _guildService.SetMessageLog(guildId, value);

            if (value)
                return Ok("Message log enabled");
            else
                return Ok("Message log disabled");
        }
    }
}
