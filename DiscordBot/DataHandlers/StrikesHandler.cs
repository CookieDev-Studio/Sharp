using Discord.WebSocket;
using Newtonsoft.Json;
using Sharpbot.data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class StrikesHandler
{
    public Task SaveStrike(SocketGuild guild, SocketUser user, SocketUser mod, string reason, string date)
    {
        StrikeService.AddStrike(user.Id, mod.Id, reason, date);
        return Task.CompletedTask;
    }

    public List<Strike> LoadStrikes(SocketGuild guild, SocketUser user)
    {
        return StrikeService.GetStrikes().Where(x => ulong.Parse(x.userId) == user.Id).Select(x =>
            new Strike()
            {
                Id = x.Id,
                user = guild.GetUser(ulong.Parse(x.userId)),
                mod = guild.GetUser(ulong.Parse(x.modId)),
                reason = x.reason,
                date = x.date
            }).ToList();
    }
}
