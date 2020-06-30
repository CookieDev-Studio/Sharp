using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ModModule : ModuleBase<SocketCommandContext>
{
	StrikesHandler _strikesHandler;
	ConfigHandler _config;

	public ModModule(StrikesHandler strikesHandler, ConfigHandler configHandler)
	{
		_strikesHandler = strikesHandler;
		_config = configHandler;
	}

	[Command("strike")]
	[Summary("gives a user a strike")]
	[RequireUserPermission(Discord.ChannelPermission.ManageMessages)]
	public async Task Strike(SocketUser user, string reason)
	{
		await Context.Message.DeleteAsync();

		await _strikesHandler.SaveStrike(user.Username, Context.User.Username, reason, DateTime.Today.ToString("d"));
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
		var strikes = _strikesHandler.LoadStrikes(user);

		string message = "";
		message += $"User : {user.Mention}\n";
		message += $"Mod : {Context.User.Mention}\n";
		message += "\n";

		foreach (var strike in strikes)
		{
			message += $"Strike [{strike.date}]:\n";
			message += $"Mod: {strike.mod}\n";
			message += $"```{(strike.reason != "" ? strike.reason : " ")}```\n";
		}

		message += "-------------------------------------------------------------------------------\n";

		await _config.ModChannels[Context.Guild].SendMessageAsync(message);
	}

}
