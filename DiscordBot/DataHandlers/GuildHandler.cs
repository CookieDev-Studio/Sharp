using Discord.WebSocket;
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

    public async Task InitializeGuild(SocketGuild guild)
    {
        try { await Task.Run(() =>_guildService.AddConfig(guild.Id, guild.DefaultChannel.Id)); }
        catch { }
    }

    public async Task<SocketTextChannel> GetModChannel(SocketGuild guild)
    {
        return await Task.Run(() => GetConfig(guild).Result.modChannel);
    }

    public async Task<char> GetPrefix(SocketGuild guild)
    {
        return await Task.Run(() => GetConfig(guild).Result.prefix);
    }

    private async Task<Config> GetConfig(SocketGuild guild)
    {
        var config = await Task.Run(() => _guildService.GetGuildConfig(guild.Id));

        return await Task.Run(() => new Config()
        {
            modChannel = guild.GetTextChannel(ulong.Parse(config.mod_Channel_Id)),
            prefix = config.prefix
        });
    }

    public async Task SetModChannel(SocketGuild guild, SocketTextChannel channel)
    {
        await Task.Run(() => _guildService.SetModChannel(guild.Id, channel.Id));
    }

    public async Task SetPrefix(SocketGuild guild, char prefix)
    {
        await Task.Run(() => _guildService.SetPrefix(guild.Id, prefix));
    }
}
