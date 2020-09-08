using Sharp.Service;
using Sharp.Domain;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

public class CommandHandler
{
    readonly DiscordSocketClient _client;
    readonly CommandService _commands;
    readonly IServiceProvider _services;

    public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider services)
    {
        _client = client;
        _commands = commands;
        _services = services;

        _client.MessageReceived += HandleCommandAsync;
    }

    /// <summary>
    /// Asynchronously handles commands to the bot, which are SocketMessages.
    /// </summary>
    /// <param name="messageParam">The SocketMessage</param>
    /// <returns>Is an async Task.</returns>
    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        // Don't process the command if it was a system message
        var message = messageParam as SocketUserMessage;
        if (message == null) return;

        if (message.Author.IsBot)
            return;

        // Create a WebSocket-based command context based on the message
        var context = new SocketCommandContext(_client, message);

        // Create a number to track where the prefix ends and the command begins
        int argPos = 0;

        // Determine if the message is a command based on the prefix and make sure no bots trigger commands
        if (message.HasCharPrefix(await GuildConfigService.getPrefixAsync(GuildId.NewGuildId(context.Guild.Id)), ref argPos))
        {
            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: _services);
        }
        else if (await GuildConfigService.getMessageLog(GuildId.NewGuildId(context.Guild.Id)))
        {
            MessageService.addMessage(
                GuildId.NewGuildId(context.Guild.Id),
                ModChannelId.NewModChannelId(context.Channel.Id),
                UserId.NewUserId(context.User.Id),
                message.Timestamp.UtcDateTime,
                message.Attachments.Select(x => x.ProxyUrl),
                message.Content);
        }
    }
}
