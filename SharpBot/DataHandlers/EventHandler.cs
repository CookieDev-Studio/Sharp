using SharpBot.Service;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public class EventHandler
{
    readonly StrikeService _strikeService;
    readonly GuildService _guildService;

    public EventHandler(DiscordSocketClient client, StrikeService strikeService, GuildService guildService)
    {
        _strikeService = strikeService;
        _guildService = guildService;

        client.UserBanned += RemoveAllStrikesFromUser;
        client.JoinedGuild += AddGuild;
    }

    private async Task RemoveAllStrikesFromUser(SocketUser user, SocketGuild guild)
    {
        await _strikeService.RemoveAllStrikesFromUserAsync(user.Id, guild.Id);
    }

    private async Task AddGuild(SocketGuild guild)
    {
        await _guildService.AddConfigAsync(guild.Id, guild.DefaultChannel.Id);
    }
}
