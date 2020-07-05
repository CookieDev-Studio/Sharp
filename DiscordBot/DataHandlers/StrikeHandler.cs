using Discord.WebSocket;
using SharpBot.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class StrikeHandler
{
    readonly StrikeService _strikeService;
    public StrikeHandler(StrikeService strikeService)
    {
        _strikeService = strikeService;
    }

    public Task SaveStrike(SocketGuild guild, SocketUser user, SocketUser mod, string reason, string date)
    {
        _strikeService.AddStrike(guild.Id, user.Id, mod.Id, reason, date);
        return Task.CompletedTask;
    }

    public List<Strike> LoadStrikes(SocketGuild guild, SocketUser user)
    {
        return _strikeService.GetStrikes(guild.Id, user.Id).Where(x => ulong.Parse(x.guildId) == guild.Id && ulong.Parse(x.userId) == user.Id).Select(x =>
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
