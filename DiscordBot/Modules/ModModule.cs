using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

public class ModModule : ModuleBase<SocketCommandContext>
{
	StrikesHandler _strikesLoader;
	GuildHandler _config;

	public ModModule(StrikesHandler strikesHandler, GuildHandler configHandler)
	{
		_strikesLoader = strikesHandler;
		_config = configHandler;
	}

	[Command("setModChannel")]
	[RequireUserPermission(Discord.ChannelPermission.ManageMessages)]
	public async Task SetModChannel(SocketTextChannel channel)
	{
		await LoggerExtensions.Log(Context.Guild, $"Mod channel set to {channel.Id}");
		await _config.SetModChannel(Context.Guild, channel);
		await ReplyAsync($"Mod channel set to {channel.Name}");
	}

	[Command("strike")]
	[Summary("gives a user a strike")]
	[RequireUserPermission(Discord.ChannelPermission.ManageMessages)]
	public async Task Strike(SocketUser user, string reason)
	{
		await Context.Message.DeleteAsync();

		await _strikesLoader.SaveStrike(Context.Guild, user.Id, Context.User.Id, reason, DateTime.Today.ToString("d"));
		await ShowStrikes(user);
	}

	[Command("strikes")]
	[Summary("check how many strikes a user has")]
	[RequireUserPermission(Discord.ChannelPermission.ManageMessages)]
	public async Task Strikes(SocketUser user)
	{
		await Context.Message.DeleteAsync();
		await ShowStrikes(user);
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
			message += $"Mod: <@!{strike.modId}>\n";
			message += $"```{(strike.reason != "" ? strike.reason : " ")}```\n";
		}

		message += "-------------------------------------------------------------------------------\n";

		await _config.ModChannels[Context.Guild].SendMessageAsync(message);
	}
}
