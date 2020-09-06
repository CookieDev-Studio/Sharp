using Sharp.Service;
using Sharp.Domain;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

public class EventHandler
{

    public EventHandler(DiscordSocketClient client)
    {
        client.UserBanned += RemoveAllStrikesFromUser;
        client.JoinedGuild += AddGuild;
        client.UserJoined += CheckLink; 
    }

    private async Task RemoveAllStrikesFromUser(SocketUser user, SocketGuild guild)
        => StrikeService.removeAllStrikesFromUser(GuildId.NewGuildId(guild.Id),UserId.NewUserId(guild.Id));

    private async Task AddGuild(SocketGuild guild)
        => GuildConfigService.addConfig(GuildId.NewGuildId(guild.Id), ModChannelId.NewModChannelId(guild.DefaultChannel.Id));
    

    private async Task CheckLink(SocketGuildUser user)
    {
        var invites = await user.Guild.GetInvitesAsync();
        //create a LinkRole object
        var pairs = LinkService.getLinkRolePairs(GuildId.NewGuildId(user.Guild.Id));

        foreach (var invite in invites)
        {
            foreach (var pair in pairs)
            {
                if (invite.Code == pair.linkCode && invite.Uses > pair.uses)
                {
                    await user.AddRoleAsync(user.Guild.GetRole(pair.roleId.Item));
                    LinkService.updateUses(invite.Code, (int)invite.Uses);
                }
            }
        }
    }
}
