using SharpBot.Service;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using SharpBot.Data;
using System;
using System.Threading.Tasks;

[Name("StrikeModule")]
[Group("strike")]
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

	[Command("help")]
	[Alias("", "?")]
	[Summary("Display strike commands")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task Strike()
	{
		var builder = new EmbedBuilder()
		{
			Color = new Color(150, 0, 0),
			Description = $"Strike: "
		};

		foreach (var command in _commandExtentions.GetCommands("StrikeModule"))
			builder.AddField(command.Name, command.Summary, false);

		await ReplyAsync("", false, builder.Build());
	}

	[Command("add")]
	[Summary("strike add _@user_ _\"message\"_\n Gives a user a strike")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task StrikeAdd(SocketUser user = null, string reason = "unspecified")
	{
		if (user == null)
        {
			await ReplyAsync("User not specified");
			return;
        }

		await Context.Message.DeleteAsync();

		await _strikesHandler.SaveStrikeAsync(Context.Guild.Id, user.Id, Context.User.Id, reason, DateTime.Today.ToString("d"));
		await ShowStrikes(user);
	}

	[Command("list")]
	[Summary("strike list _@user_\n Displays all of the user's srtrikes")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task Strikes(SocketUser user = null)
	{
		if (user == null)
		{
			await ReplyAsync("User not specified");
			return;
		}

		await Context.Message.DeleteAsync();
		await ShowStrikes(user);
	}

	[Command("remove")]
	[Summary("strike remove _strikeid_\n removes the specified strike")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task RemoveStrikes(int? strikeId = null)
	{
		if (strikeId == null)
		{
			await ReplyAsync("Strike id not specified");
			return;
		}

		await _strikesHandler.RemoveStrikeAsync((int)strikeId);
		await ReplyAsync("strike removed");
	}

	[Command("remove all")]
	[Summary("strike remove all _@user_\n removes the specified strike")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task RemoveAllStrikes(SocketUser user = null)
	{
		if (user == null)
		{
			await ReplyAsync("User not specified");
			return;
		}

		await Context.Message.DeleteAsync();
		await _strikesHandler.RemoveAllStrikesFromUserAsync(user.Id, Context.Guild.Id);
		await ReplyAsync($"Removed all of {user.Mention}'s strikes");
	}

	private async Task ShowStrikes(SocketUser user)
	{
		var strikes = await _strikesHandler.LoadStrikesAsync(Context.Guild.Id, user.Id);
		
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

		await Context.Guild.GetTextChannel(await _guildHandler.GetModChannelAsync(Context.Guild.Id)).SendMessageAsync(message);
	}
}
