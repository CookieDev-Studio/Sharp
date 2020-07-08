using Discord.WebSocket;
using SharpBot.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class StrikeHandler
{
    readonly DiscordSocketClient _client;
    readonly StrikeService _strikeService;

    public StrikeHandler(DiscordSocketClient client, StrikeService strikeService)
    {
        _client = client;
        _strikeService = strikeService;

        client.UserBanned += RemoveAllStrikesFromUser;
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

    public Task RemoveStrike(int strikeId)
    {
        _strikeService.RemoveStrike(strikeId);
        return Task.CompletedTask;
    }

    public Task RemoveAllStrikesFromUser(SocketUser user, SocketGuild guild)
    {
        _strikeService.RemoveAllStrikesFromUser(guild.Id, user.Id);
        return Task.CompletedTask;
    }
}
