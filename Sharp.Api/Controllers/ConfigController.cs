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

        [HttpGet]
        public ActionResult<List<Config>> GetConfigs(string token)
        {
            // call discord api with token to get list of servers user is in (with token)
            // call discord api to get list of servers BOT is in
            // filter the two lists to get what servers the user AND the bot are in
            // send filtered list to client with info the client needs
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
