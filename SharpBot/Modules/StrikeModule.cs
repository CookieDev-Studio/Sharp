using SharpBot.Service;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using SharpBot.Data;
using System;
using System.Threading.Tasks;

public class StrikeModule : ModuleBase<SocketCommandContext>
{
    readonly StrikeService _strikesHandler;
    readonly GuildService _guildHandler;
	readonly CommandExtentions _commandExtentions;

	public StrikeModule(StrikeService strikesHandler, GuildService guildService, CommandExtentions commandExtentions)
	{
		_strikesHandler = strikesHandler;
		_guildHandler = guildService;
		_commandExtentions = commandExtentions;
	}

	[Command("strike add")]
	[Summary("strike add _@user_ _\"message\"_\n Gives a user a strike")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task Strike(SocketUser user, string reason)
	{
		await Context.Message.DeleteAsync();

		await _strikesHandler.SaveStrike(Context.Guild.Id, user.Id, Context.User.Id, reason, DateTime.Today.ToString("d"));
		await ShowStrikes(user);
	}

	[Command("strike list")]
	[Summary("strike list _@user_\n Displays all of the user's srtrikes")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task Strikes(SocketUser user)
	{
		await Context.Message.DeleteAsync();
		await ShowStrikes(user);
	}

	[Command("strike remove")]
	[Summary("strike remove _strikeid_\n removes the specified strike")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task RemoveStrikes(int strikeId)
	{
		await _strikesHandler.RemoveStrike(strikeId);
		await ReplyAsync("strike removed");
	}

	[Command("strike remove all")]
	[Summary("strike remove all _@user_\n removes the specified strike")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task RemoveAllStrikes(SocketUser user)
	{
		await Context.Message.DeleteAsync();
		await _strikesHandler.RemoveAllStrikesFromUser(user.Id, Context.Guild.Id);
		await ReplyAsync($"Removed all of {user.Mention}'s strikes");
	}

	private async Task ShowStrikes(SocketUser user)
	{
		var strikes = _strikesHandler.LoadStrikes(Context.Guild.Id, user.Id);
		
		string message = "";
		message += $"User : {user.Mention}\n";
		message += "\n";

		foreach (var strike in strikes)
		{
			message += $"Strike [{strike.date}]:\n";
			message += $"Id: {strike.Id}\n";
			message += $"Mod: {Context.Guild.GetUser(strike.mod).Mention}\n";
			message += $"```{(strike.reason != "" ? strike.reason : " ")}```\n";
		}

		message += "-------------------------------------------------------------------------------\n";

		await Context.Guild.GetTextChannel(_guildHandler.GetModChannel(Context.Guild.Id).Result).SendMessageAsync(message);
	}
}
