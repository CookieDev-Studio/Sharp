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
    }

    public async Task<SocketTextChannel> GetModChannel(SocketGuild guild)
    {
        return await Task.Run(() => GetConfig(guild).Result.modChannel);
    }

    public async Task<char> GetPrefix(SocketGuild guild)
    {
        return await Task.Run(() => GetConfig(guild).Result.prefix);
    }

    public async Task<bool> GetMessageLog(SocketGuild guild)
    {
        return await Task.Run(() => GetConfig(guild).Result.messageLog);
    }

    public async Task SetModChannel(SocketGuild guild, SocketTextChannel channel)
    {
        await Task.Run(() => _guildService.SetModChannel(guild.Id, channel.Id));
    }

    public async Task SetPrefix(SocketGuild guild, char prefix)
    {
        await Task.Run(() => _guildService.SetPrefix(guild.Id, prefix));
    }

    public async Task SetMessageLog(SocketGuild guild, bool value)
    {
        await Task.Run(() => _guildService.SetMessageLog(guild.Id, value));
    }

    private async Task<Config> GetConfig(SocketGuild guild)
    {
        var config = await Task.Run(() => _guildService.GetGuildConfig(guild.Id));

        return await Task.Run(() => new Config()
        {
            modChannel = config.mod_Channel_Id == null ? guild.DefaultChannel : guild.GetTextChannel(ulong.Parse(config.mod_Channel_Id)),
            prefix = config.prefix
        });
    }
}
