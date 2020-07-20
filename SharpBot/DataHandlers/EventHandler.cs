using SharpBot.Service;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public class EventHandler
{
    readonly StrikeService _strikeService;
    public EventHandler(DiscordSocketClient client, StrikeService strikeService)
    {
        _strikeService = strikeService;
        client.UserBanned += RemoveAllStrikesFromUser;
    }

    private Task RemoveAllStrikesFromUser(SocketUser user, SocketGuild guild)
    {
        return _strikeService.RemoveAllStrikesFromUser(user.Id, guild.Id);
    }
}
