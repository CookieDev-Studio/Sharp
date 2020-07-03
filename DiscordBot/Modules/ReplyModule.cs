﻿using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ReplyModule : ModuleBase<SocketCommandContext>
{
	CommandService _commands;

	public ReplyModule(CommandService commands)
    {
		_commands = commands;
    }

	[Command("greet")]
	[Summary("Greets the user")]
	[RequireUserPermission(Discord.ChannelPermission.ManageMessages)]
	public async Task Say()
	{
		await ReplyAsync("hello " + Context.User.Mention);
	}
	[Command("help")]
	[Summary("Shows all Commands")]
	public async Task Help()
    {
		var builder = new EmbedBuilder()
		{
			Color = new Color(114, 137, 218),
			Description = "These are the commands you can use"
		};

		foreach (var module in _commands.Modules)
			foreach (var command in module.Commands)
				builder.AddField(command.Name, command.Summary, false);

		await ReplyAsync("", false, builder.Build());
	}
}
