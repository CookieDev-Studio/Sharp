using Discord.WebSocket;
using Newtonsoft.Json;
using SharpBot.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

public class GuildHandler
{
    readonly GuildService _guildService;
    public GuildHandler(DiscordSocketClient client, GuildService guildService)
    {
        _guildService = guildService;

        client.GuildAvailable += InitializeGuild;
        client.JoinedGuild += InitializeGuild;
    }

    public Task InitializeGuild(SocketGuild guild)
    {
        try { _guildService.AddConfig(guild.Id, guild.DefaultChannel.Id); }
        catch { }

        return Task.CompletedTask;
    }

    public SocketTextChannel GetModChannel(SocketGuild guild)
    {
        return GetConfig(guild).Result.modChannel;
    }

    public char GetPrefix(SocketGuild guild)
    {
        return GetConfig(guild).Result.prefix;
    }

    private async Task<Config> GetConfig(SocketGuild guild)
    {
        await Task.Run(() => guild.GetTextChannel(ulong.Parse(_guildService.GetGuildConfig(guild.Id).mod_Channel_Id)));
        var config = _guildService.GetGuildConfig(guild.Id);

        return new Config()
        {
            modChannel = guild.GetTextChannel(ulong.Parse(config.mod_Channel_Id)),
            prefix = config.prefix
        };
    }

    public Task SetModChannel(SocketGuild guild, SocketTextChannel channel)
    {
        _guildService.SetModChannel(guild.Id, channel.Id);
        return Task.CompletedTask;
    }

    public Task SetPrefix(SocketGuild guild, char prefix)
    {
        _guildService.SetPrefix(guild.Id, prefix);
        return Task.CompletedTask;
    }
}
