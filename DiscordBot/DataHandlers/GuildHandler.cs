using Discord.WebSocket;
using Newtonsoft.Json;
using SharpBot.Data;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class GuildHandler
{
    public GuildHandler(DiscordSocketClient client)
    {
        client.GuildAvailable += InitializeGuild;
        client.JoinedGuild += InitializeGuild;
    }

    public Task InitializeGuild(SocketGuild guild)
    {
        try
        {
            GuildService.AddConfig(guild.Id, guild.DefaultChannel.Id);
        }
        catch { }

        return Task.CompletedTask;
    }

    public SocketTextChannel GetModChannel(SocketGuild guild)
    {
        return GetConfig(guild).Result.modChannel;
    }

    private async Task<Config> GetConfig(SocketGuild guild)
    {
        return new Config()
        {
            modChannel = guild.GetTextChannel(ulong.Parse(GuildService.GetGuildConfig(guild.Id).modChannelId))
        };
    }

    public Task SetModChannel(SocketGuild guild, SocketTextChannel channel)
    {
        GuildService.SetModChannel(guild.Id, channel.Id);
        return Task.CompletedTask;
    }
}
