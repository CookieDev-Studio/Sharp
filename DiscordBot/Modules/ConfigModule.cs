using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public class ConfigModule : ModuleBase<SocketCommandContext>
{
    StrikesHandler _strikeHandler;
    SpaghettiHandler _config;

    public ConfigModule(StrikesHandler strikeHandler, SpaghettiHandler config)
    {
        _strikeHandler = strikeHandler;
        _config = config;
    }

    [Command("setModChannel")]
	[RequireUserPermission(Discord.ChannelPermission.ManageMessages)]
    public async Task SetModChannel(SocketTextChannel channel)
    {
        await Logger.Log(Context.Guild, $"Mod channel set to {_config.ModChannels[Context.Guild].Name}");
        await _config.SetModChannel(Context.Guild, channel);
        await ReplyAsync($"Mod channel set to {_config.ModChannels[Context.Guild].Name}");
    }
}