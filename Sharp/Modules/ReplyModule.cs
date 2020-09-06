using Sharp.Service;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Sharp.Domain;

public class ReplyModule : ModuleBase<SocketCommandContext>
{
    readonly CommandService _commands;
	readonly CommandExtentions _commandExtentions;

	public ReplyModule(CommandService commands, CommandExtentions commandExtentions)
    {
        _commands = commands;
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
			Description = $"The prefix for this community is { GuildConfigService.getPrefix(GuildId.NewGuildId(Context.Guild.Id))}"
		};

			foreach (var command in _commandExtentions.GetAllCommands())
				builder.AddField($"{command.Module.Group} {command.Name}", command.Summary, false); 

		await ReplyAsync("", false, builder.Build());
	}
}
