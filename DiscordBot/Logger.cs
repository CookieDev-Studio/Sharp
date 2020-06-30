using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public static class Logger
{
    public static Task Log(SocketGuild guild, string msg)
    {
        Console.WriteLine($"{DateTime.Now:HH:mm:ss} {guild.Id} {msg}");
        return Task.CompletedTask;
    }
}
