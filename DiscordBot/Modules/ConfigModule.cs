using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public class ConfigModule : ModuleBase<SocketCommandContext>
{
	readonly GuildHandler _config;

	public ConfigModule(GuildHandler configHandler)
	{
		_config = configHandler;
	}

	[Command("set modchannel")]
	[Summary("set modchannel _#channel_\n Sets the mod channel")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task SetModChannel(SocketTextChannel channel)
	{
		await LoggerExtensions.Log(Context.Guild, $"Mod channel set to {channel.Id}");
		await _config.SetModChannel(Context.Guild, channel);
		await ReplyAsync($"Mod channel set to {channel.Name}");
	}

	[Command("set prefix")]
	[Summary("set prefix _prefix_\n sets the command prefix")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task SetPrefix(char prefix)
	{
		await _config.SetPrefix(Context.Guild, prefix);
		await ReplyAsync($"prefix set to {prefix}");
	}

	[Command("enable messagelog")]
	[Summary("set prefix _prefix_\n sets the command prefix")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task EnableMessageLog(bool value)
	{
		await _config.SetMessageLog(Context.Guild, value);

        if (value)
            await ReplyAsync($"Message log enabled");
		else
			await ReplyAsync($"Message log disabled");
	}
}
