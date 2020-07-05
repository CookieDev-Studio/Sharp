using Discord.WebSocket;
using Newtonsoft.Json;
using SharpBot.Data;
using System.Collections.Generic;
using System.IO;
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

    private async Task<Config> GetConfig(SocketGuild guild)
    {
        Config config = new Config
        {
            modChannel = await Task.Run(() => guild.GetTextChannel(ulong.Parse(_guildService.GetGuildConfig(guild.Id).modChannelId)))
        };

        return config;
    }

    public Task SetModChannel(SocketGuild guild, SocketTextChannel channel)
    {
        _guildService.SetModChannel(guild.Id, channel.Id);
        return Task.CompletedTask;
    }
}
