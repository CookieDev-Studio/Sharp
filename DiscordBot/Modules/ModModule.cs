using Discord;
using Discord.Commands;
using Discord.WebSocket;
using SharpBot.Data;
using System;
using System.Threading.Tasks;

public class ModModule : ModuleBase<SocketCommandContext>
{
    readonly StrikeHandler _strikesLoader;
    readonly GuildHandler _config;
    readonly StrikeService _strikeService;

	public ModModule(StrikeHandler strikesHandler, GuildHandler configHandler, StrikeService strikeService)
	{
		_strikesLoader = strikesHandler;
		_config = configHandler;
		_strikeService = strikeService;
	}

	[Command("setmodchannel")]
	[Summary("!setmodchannel _#channel_\n Sets the mod channel")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task SetModChannel(SocketTextChannel channel)
	{
		await LoggerExtensions.Log(Context.Guild, $"Mod channel set to {channel.Id}");
		await _config.SetModChannel(Context.Guild, channel);
		await ReplyAsync($"Mod channel set to {channel.Name}");
	}

	[Command("strike")]
	[Summary("!strike _@user_ _\"message\"_\n Gives a user a strike")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task Strike(SocketUser user, string reason)
	{
		await Context.Message.DeleteAsync();

		await _strikesLoader.SaveStrike(Context.Guild, user, Context.User, reason, DateTime.Today.ToString("d"));
		await ShowStrikes(user);
	}

	[Command("strikes")]
	[Summary("!strikes _@user_\n Displays all of the user's srtrikes")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task Strikes(SocketUser user)
	{
		await Context.Message.DeleteAsync();
		await ShowStrikes(user);
	}

	[Command("removestrike")]
	[Summary("!removestrike _strikeid_\n removes the specified strike")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task RemoveStrikes(int strikeId)
	{
		_strikeService.RemoveStrike(strikeId);
		await ReplyAsync("strike removed");
	}

	private async Task ShowStrikes(SocketUser user)
	{
		var strikes = _strikesLoader.LoadStrikes(Context.Guild, user);
		
		string message = "";
		message += $"User : {user.Mention}\n";
		message += "\n";

		foreach (var strike in strikes)
		{
			message += $"Strike [{strike.date}]:\n";
			message += $"Id: {strike.Id}\n";
			message += $"Mod: {strike.mod.Mention}\n";
			message += $"```{(strike.reason != "" ? strike.reason : " ")}```\n";
		}

		message += "-------------------------------------------------------------------------------\n";

		await _config.GetModChannel(Context.Guild).SendMessageAsync(message);
	}
}
