using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Sharp.Service;
using Sharp.FSharp.Service;
using Sharp.FSharp.Domain;
using System;
using System.Threading.Tasks;
using System.Diagnostics;

[Name("StrikeModule")]
[Group("strike")]
public class StrikeModule : ModuleBase<SocketCommandContext>
{
    readonly GuildService _guildHandler;
	readonly CommandExtentions _commandExtentions;

	public StrikeModule(GuildService guildService, CommandExtentions commandExtentions)
	{
		_guildHandler = guildService;
		_commandExtentions = commandExtentions;
	}

	[Command("help")]
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

	[Command("")]
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

		Sharp.FSharp.Service.StrikeService.addStrike(
			GuildId.NewGuildId(Context.Guild.Id),
			UserId.NewUserId(user.Id),
			ModId.NewModId(Context.User.Id),
			reason,
			DateTime.UtcNow);
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
		StrikeService.removeStrike(GuildId.NewGuildId(Context.Guild.Id), (int)strikeId);
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
		StrikeService.removeAllStrikesFromUser(GuildId.NewGuildId(Context.Guild.Id), UserId.NewUserId(user.Id));
		await ReplyAsync($"Removed all of {user.Mention}'s strikes");
	}

	private async Task ShowStrikes(SocketUser user)
    {
		var strikes = Sharp.FSharp.Service.StrikeService.getStrikes(GuildId.NewGuildId(Context.Guild.Id), UserId.NewUserId(user.Id));

		var builder = new EmbedBuilder()
		{
			Color = new Color(150, 0, 0),
		};

		foreach (var strike in strikes)
			builder.AddField($"Id: {strike.id}",
				$"Date: {strike.date.ToDateTime():yyyy-MM-dd}\n" +
				$"Time: {strike.date.ToDateTime():HH:mm}\n" +
				$"Mod: {Context.Guild.GetUser(strike.modId.Item)}" +
				$"\n\n" +
				$"{strike.reason}",
				true);

		await Context.Guild.GetTextChannel(await _guildHandler.GetModChannelAsync(Context.Guild.Id))
			.SendMessageAsync($"Strikes logged against {user.Mention}:", embed: builder.Build());
	}
}
