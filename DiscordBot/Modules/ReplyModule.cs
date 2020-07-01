using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public class ReplyModule : ModuleBase<SocketCommandContext>
{
	[Command("greet")]
	[Summary("Echoes a message.")]
	[RequireUserPermission(Discord.ChannelPermission.ManageMessages)]
	public async Task Say()
	{
		await ReplyAsync("hello " + Context.User.Mention);
	}

	[Command("test")]
	[Summary("Echoes a message.")]
	[RequireUserPermission(Discord.ChannelPermission.ManageMessages)]
	public async Task StressTest(int testAmount)
	{
		for (int i = 0; i < testAmount; i++)
		await ReplyAsync("!strikes <@!143546410883743745>");
	}
}
