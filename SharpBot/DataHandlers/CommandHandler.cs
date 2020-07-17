using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CommandHandler
{
    readonly DiscordSocketClient _client;
    readonly CommandService _commands;
    readonly GuildHandler _guildHandler;
    readonly MessageHandler _messageHandler;
    readonly IServiceProvider _services;

    public CommandHandler(DiscordSocketClient client, CommandService commands, GuildHandler guildHandler, MessageHandler messageHandler, IServiceProvider services)
    {
        _client = client;
        _commands = commands;
        _guildHandler = guildHandler;
        _messageHandler = messageHandler;
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
        if (message.HasCharPrefix(_guildHandler.GetPrefix(context.Guild).Result, ref argPos))
        {
            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: _services);
        }
        else if (_guildHandler.GetMessageLog(context.Guild).Result)
            _messageHandler.AddMessage(context.Guild, messageParam);
    }
}
