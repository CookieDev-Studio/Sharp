using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

    private DiscordSocketClient _client;
    private CommandService _commands; 
    private IServiceProvider _services;
    private GuildHandler _config;

    public async Task MainAsync()
    {
        _client = new DiscordSocketClient();
        _commands = new CommandService();
        _config = new GuildHandler(_client);

        _client.Log += Log;
        _client.MessageReceived += HandleCommandAsync;

        _services = new ServiceCollection()
            .AddSingleton(this)
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .AddSingleton(_config)
            .AddSingleton<StrikesHandler>()
            .BuildServiceProvider();

        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        await _client.LoginAsync(TokenType.Bot, GetToken().Result);
        await _client.StartAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }

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

    public async Task<string> GetToken()
    {
        try { return File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "token.txt")); }
        catch
        {
            Console.WriteLine("Enter TOKEN:");
            string token = Console.ReadLine();
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "token.txt"), token);
            return token;
        }
    }


    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}
