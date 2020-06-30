using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public class ConfigModule : ModuleBase<SocketCommandContext>
{
    StrikesHandler _strikeHandler;
    ConfigHandler _config;

    public ConfigModule(StrikesHandler strikeHandler, ConfigHandler config)
    {
        _strikeHandler = strikeHandler;
        _config = config;
		Logger.Log("Config Module loaded");
    }

    [Command("setModChannel")]
	[RequireUserPermission(Discord.ChannelPermission.ManageMessages)]
    public async Task SetModChannel(SocketTextChannel channel)
    {
        await Logger.Log("test started");
        await _config.SetModChannel(channel);
        await ReplyAsync($"Mod channel set to {_config.ModChannel.Name}");
    }
}