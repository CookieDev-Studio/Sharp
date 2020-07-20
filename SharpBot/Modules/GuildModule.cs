using SharpBot.Service;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GuildModule : ModuleBase<SocketCommandContext>
{
	readonly GuildService _config;

	public GuildModule(GuildService configHandler)
	{
		_config = configHandler;
	}

	[Command("set modchannel")]
	[Summary("set modchannel _#channel_\n Sets the mod channel")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task SetModChannel(SocketTextChannel channel)
	{
		await LoggerExtensions.Log(Context.Guild, $"Mod channel set to {channel.Id}");
		await _config.SetModChannel(Context.Guild.Id, channel.Id);
		await ReplyAsync($"Mod channel set to {channel.Name}");
	}

	[Command("set prefix")]
	[Summary("set prefix _prefix_\n sets the command prefix")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task SetPrefix(char prefix)
	{
		if (prefix == null)
        {
			await ReplyAsync("No prefix specified");
			return;
		}
		await _config.SetPrefix(Context.Guild.Id, (char)prefix);
		await ReplyAsync($"prefix set to {prefix}");
	}

	[Command("enable messagelog")]
	[Summary("enables the message log")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task EnableMessageLog()
	{
		await _config.SetMessageLog(Context.Guild.Id, true);
        await ReplyAsync($"Message log enabled");
	}

	[Command("disable messagelog")]
	[Summary("disables the message log")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task DisableMessageLog()
	{
		await _config.SetMessageLog(Context.Guild.Id, false);
		await ReplyAsync($"Message log disabled");
	}
}
