using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using SharpBot.Data;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

class Program
{
    /// <summary>
    /// Gets the result from the MainAsync method.
    /// </summary>
    /// <param name="args">Commandline arguments.</param>
    static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

    private DiscordSocketClient _client;
    private CommandService _commands; 
    private IServiceProvider _services;

    /// <summary>
    /// Main method.
    /// </summary>
    /// <returns>A delayed task.</returns>
    public async Task MainAsync()
    {
        _client = new DiscordSocketClient();
        _commands = new CommandService();

        _client.Log += Log;
        _client.MessageReceived += HandleCommandAsync;

        _services = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .AddSingleton<GuildService>()
            .AddSingleton<StrikeService>()
            .AddSingleton<StrikeHandler>()
            .AddSingleton<GuildHandler>()
            .BuildServiceProvider();

        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        await _client.LoginAsync(TokenType.Bot, GetToken().Result);
        await _client.StartAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
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

        // Create a number to track where the prefix ends and the command begins
        int argPos = 0;

        // Determine if the message is a command based on the prefix and make sure no bots trigger commands
        if (!(message.HasCharPrefix('!', ref argPos) ||
            message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
            message.Author.IsBot)
            return;

        // Create a WebSocket-based command context based on the message
        var context = new SocketCommandContext(_client, message);

        // Execute the command with the command context we just
        // created, along with the service provider for precondition checks.
        await _commands.ExecuteAsync(
            context: context,
            argPos: argPos,
            services: _services);
    }

    /// <summary>
    /// Asynchronous Task to get the Discord bot's token.
    /// </summary>
    /// <returns>The token.</returns>
    public async Task<string> GetToken()
    {
        try { return File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "token.txt")); }
        catch
        {
            Console.WriteLine("Enter TOKEN:");
            string token = Console.ReadLine();
            await File.WriteAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "token.txt"), token);
            return token;
        }
    }

    /// <summary>
    /// Log task that is added as an event to the main program execution.
    /// </summary>
    /// <param name="msg">The LogMessage to write to the console.</param>
    /// <returns>A completed task.</returns>
    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}
