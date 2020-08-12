using Discord.Commands;
using Discord.WebSocket;
using Sharp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LinkModule : ModuleBase
{
    readonly LinkService _linkService;
    public LinkModule(LinkService linkService) => _linkService = linkService;

    [Command("pair")]
    [Summary("adds a user to the role when they join from the link")]
    [RequireUserPermission(Discord.ChannelPermission.ManageChannels)]
    public async Task AddLinkRolePair(string linkCode, SocketRole role)
    {
        
        if (linkCode.StartsWith("https://discord.gg/"))
            linkCode = linkCode.Substring(19);

        var invites = await Context.Guild.GetInvitesAsync();
        //if link exists
        if (Context.Guild.GetInvitesAsync().Result.Any(x => x.Code == linkCode))
        {
            

            await _linkService.AddLinkRolePairAsync(Context.Guild.Id, linkCode, role.Id, (int)invites.First(x => x.Code == linkCode).Uses);
            await ReplyAsync($"users will now join {role.Mention} when joining from link");
        }
        else
            await ReplyAsync($"link does not exist");
    }
}

