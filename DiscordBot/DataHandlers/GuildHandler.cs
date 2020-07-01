using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GuildHandler
{
    public Dictionary<SocketGuild, SocketTextChannel> ModChannels;

    public struct Config
    {
        public ulong modChannelId;
    }

    public GuildHandler(DiscordSocketClient client)
    {
        ModChannels = new Dictionary<SocketGuild, SocketTextChannel>();

        client.GuildAvailable += InitializeGuild;
        client.JoinedGuild += CreateGuild;
    }

    public Task InitializeGuild(SocketGuild guild)
    {
        ModChannels.Add(guild, GetModChannel(guild).Result);

        return Task.CompletedTask;
    }

    public Task CreateGuild(SocketGuild guild)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), guild.Id.ToString());
        Config conf = new Config() { modChannelId = guild.DefaultChannel.Id };

        Directory.CreateDirectory(path);
        File.WriteAllText(Path.Combine(path, "config.json"), JsonConvert.SerializeObject(conf));

        return Task.CompletedTask;
    }

    private async Task<SocketTextChannel> GetModChannel(SocketGuild guild)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), guild.Id.ToString(), "config.json");
        return guild.GetTextChannel(JsonConvert.DeserializeObject<Config>(File.ReadAllText(path)).modChannelId);
    }

    public async Task SetModChannel(SocketGuild guild, SocketTextChannel channel)
    {
        //set mod channel
        ModChannels[guild] = channel;

        //save new mod channel
        Config newConfig = new Config();
        newConfig.modChannelId = channel.Id;
        string path = Path.Combine(Directory.GetCurrentDirectory(), guild.Id.ToString(), "config.json");
        File.WriteAllText(path, JsonConvert.SerializeObject(newConfig));
    }
}
