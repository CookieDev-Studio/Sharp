﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sharp.Service;

namespace Sharp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly GuildService _guildService;
        public UserController(GuildService guildService) => _guildService = guildService;

        [HttpGet("guildswithsharp/{token}")]
        public async Task<ActionResult> GetCommonGuildsAsync(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            var response = await client.GetAsync("https://discordapp.com/api/users/@me/guilds");
            var userGuilds = JsonConvert.DeserializeObject<PartialGuild[]>(await response.Content.ReadAsStringAsync());

            var guildIds = await _guildService.GetAllGuildsAsync();

            foreach (var guild in userGuilds)
                guild.Icon = $"https://cdn.discordapp.com/icons/{guild.Id}/{guild.Icon}";
            return Ok(JsonConvert.SerializeObject(userGuilds.Where(x => guildIds.Contains(x.Id))));
        }
    }
}