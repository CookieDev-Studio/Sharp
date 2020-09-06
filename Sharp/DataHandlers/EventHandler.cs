using Sharp.Service;
using Sharp.FSharp.Service;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Sharp.FSharp.Domain;

public class EventHandler
{
    readonly GuildService _guildService;
    readonly LinkService _linkService;

    public EventHandler(DiscordSocketClient client, GuildService guildService, LinkService linkService)
    {
        _guildService = guildService;
        _linkService = linkService;

        client.UserBanned += RemoveAllStrikesFromUser;
        client.JoinedGuild += AddGuild;
        client.UserJoined += CheckLink; 
    }

    private async Task RemoveAllStrikesFromUser(SocketUser user, SocketGuild guild)
        => StrikeService.removeAllStrikesFromUser(GuildId.NewGuildId(guild.Id),UserId.NewUserId(guild.Id));

    private async Task AddGuild(SocketGuild guild)
        => await _guildService.AddConfigAsync(guild.Id, guild.DefaultChannel.Id);
    

    private async Task CheckLink(SocketGuildUser user)
    {
        var invites = await user.Guild.GetInvitesAsync();
        //create a LinkRole object
        var pairs = await _linkService.GetLinkRolePairsAsync(user.Guild.Id);

        foreach (var invite in invites)
        {
            foreach (var pair in pairs)
            {
                if (invite.Code == pair.Code && invite.Uses > pair.Uses)
                {
                    await user.AddRoleAsync(user.Guild.GetRole(pair.RoleId));
                    await _linkService.UpdateUsesAsync(invite.Code, (int)invite.Uses);
                }
            }
        }
    }
}
