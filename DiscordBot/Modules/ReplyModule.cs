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
}
