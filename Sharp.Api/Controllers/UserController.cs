using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sharp.Service;

namespace Sharp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("guilds/{token}")]
        public async Task<ActionResult> GetCommonGuildsAsync(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            var response = await client.GetAsync("https://discordapp.com/api/users/@me/guilds");
            var userGuilds = JsonConvert.DeserializeObject<PartialGuild[]>(await response.Content.ReadAsStringAsync());

            var guildIds = GuildConfigService.getAllConfigs.Select(x => x.guildId.Item);

            foreach (var guild in userGuilds)
            {
                if (guild.Icon != null)
                guild.Icon = $"https://cdn.discordapp.com/icons/{guild.Id}/{guild.Icon}";
            }

            return Ok(JsonConvert.SerializeObject(userGuilds.Where(x => guildIds.Contains(x.Id))));
        }
    }
}
