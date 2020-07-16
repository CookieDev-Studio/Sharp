using Discord;
using Discord.Commands;
using System.Threading.Tasks;

public class ReplyModule : ModuleBase<SocketCommandContext>
{
    readonly CommandService _commands;
	readonly GuildHandler _guildHandler;
	readonly CommandExtentions _commandExtentions;

	public ReplyModule(CommandService commands, GuildHandler guildHandler, CommandExtentions commandExtentions)
    {
        _commands = commands;
		_guildHandler = guildHandler;
		_commandExtentions = commandExtentions;
    }

	[Command("greet")]
	[Summary("Greets the user")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task Say()
	{
		await ReplyAsync("hello " + Context.User.Mention);
	}
	[Command("help")]
	[Summary("Shows all Commands")]
	[RequireUserPermission(ChannelPermission.ManageMessages)]
	public async Task Help()
    {
		var builder = new EmbedBuilder()
		{
			Color = new Color(150, 0, 0),
			Description = $"The prefix for this community is {_guildHandler.GetPrefix(Context.Guild).Result}"
		};

			foreach (var command in _commandExtentions.GetAllCommands().Result)
				builder.AddField(command.Name, command.Summary, false); 

		await ReplyAsync("", false, builder.Build());
	}
}
