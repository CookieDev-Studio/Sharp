using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SpaghettiHandler
{
    public Dictionary<SocketGuild, SocketTextChannel> ModChannels { get; private set; }

    private Config config;
    private DiscordSocketClient _client;
    private readonly string configPath = Path.Combine(Directory.GetCurrentDirectory(), "config.json");

    public SpaghettiHandler(DiscordSocketClient client)
    {
        ModChannels = new Dictionary<SocketGuild, SocketTextChannel>();

        _client = client;

        LoadConfig();
        client.Ready += ConvertConfig;
        client.GuildAvailable += OnJoinNewGuild;
    }

    public async Task OnJoinNewGuild(SocketGuild guild)
    {
        ModChannels.Add(guild, guild.DefaultChannel);
        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), guild.Id.ToString()).Replace(@"\", @"\\"));
    }

    public async Task SetModChannel(SocketGuild guild, SocketTextChannel channel)
    {
        ModChannels[guild] = channel;
        await SaveConfig();
    } 
    private Task ConvertConfig()
    {
        foreach (var pair in config.modChannels)
        {
            SocketGuild guild = _client.GetGuild(pair.Key);
            ModChannels[guild] = guild.GetTextChannel(pair.Value);
        }
        return Task.CompletedTask;
    }

    private struct Config
    {
        public Dictionary<ulong, ulong> modChannels;
    }

    private async Task SaveConfig()
    {
        config.modChannels = ModChannels.ToDictionary(x => x.Key.Id, x => x.Value.Id);

        File.WriteAllText(configPath, JsonConvert.SerializeObject(config));
    }

    
    private Task LoadConfig()
    {
        config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath));
        return Task.CompletedTask;
    }
    
}
