using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sharp.Service;

namespace Sharp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly GuildService _guildService;
        public UserController(GuildService guildService) => _guildService = guildService;

        [HttpGet("commonguilds/{token}")]
        public async Task<ActionResult> GetCommonGuildsAsync(string token)
        {
            //list of all guilds user is in
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            var response = await client.GetAsync("https://discordapp.com/api/users/@me/guilds");

            var userGuilds = await response.Content.ReadAsStringAsync();
            //EXTRACT GUILD ID
            var userGuildIds = new ulong[0];

            //list of all guilds sharp is in 
            var guildIds = await _guildService.GetAllGuildsAsync();

            //return list of common guilds
            return Ok(userGuildIds.Where(x => guildIds.Contains(x)));
        }
    }
}
