using Discord.Commands;
using Discord.WebSocket;
using SharpBot.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

public class CommandHandler
{
    readonly DiscordSocketClient _client;
    readonly CommandService _commands;
    readonly GuildService _guildService;
    readonly MessageService _messageService;
    readonly IServiceProvider _services;

    public CommandHandler(DiscordSocketClient client, CommandService commands, GuildService guildHandler, MessageService messageHandler, IServiceProvider services)
    {
        _client = client;
        _commands = commands;
        _guildService = guildHandler;
        _messageService = messageHandler;
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
        if (message.HasCharPrefix(_guildService.GetPrefixAsync(context.Guild.Id).Result, ref argPos))
        {
            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: _services);
        }
        else if (_guildService.GetMessageLogAsync(context.Guild.Id).Result)
            await _messageService.AddMessageAsync(
                context.Guild.Id,
                context.Channel.Id,
                context.User.Id,
                message.Content,
                message.Attachments.Select(x => x.ProxyUrl).ToArray(),
                message.Timestamp.UtcDateTime);
    }
}
