using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public static class Logger
{
    public static Task Log(string msg)
    {
        Console.WriteLine($"{DateTime.Now:HH:mm:ss} {msg}");
        return Task.CompletedTask;
    }
}
