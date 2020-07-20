using SharpBot.Service;
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

    private IServiceProvider _services;

    /// <summary>
    /// Main method.
    /// </summary>
    /// <returns>A delayed task.</returns>
    public async Task MainAsync()
    {
        DiscordSocketClient _client = new DiscordSocketClient();
        CommandService _commands = new CommandService();

        _client.Log += Log;

        _services = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .AddSingleton<StrikeService>()
            .AddSingleton<StrikeData>()
            .AddSingleton<GuildService>()
            .AddSingleton<MessageService>()
            .AddSingleton<MessageData>()
            .AddSingleton<GuildData>()
            .AddSingleton<CommandExtentions>()
            .BuildServiceProvider();

        var commandHandler = new CommandHandler(_client, _commands, _services.GetService<GuildService>(), _services.GetService<MessageService>(), _services);

        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        await _client.LoginAsync(TokenType.Bot, GetToken().Result);
        await _client.StartAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
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
