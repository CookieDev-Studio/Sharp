using Discord.WebSocket;
using System;
using System.Threading.Tasks;

public static class LoggerExtensions
{
    /// <summary>
    /// Logs a guild Id and message to the console.
    /// </summary>
    /// <param name="guild">The SocketGuild.</param>
    /// <param name="msg">The message.</param>
    /// <returns>A completed task.</returns>
    public static Task Log(SocketGuild guild, string msg)
    {
        Console.WriteLine($"{DateTime.Now:HH:mm:ss} {guild.Id} {msg}");
        return Task.CompletedTask;
    }
}