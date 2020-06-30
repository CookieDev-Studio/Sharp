using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public class ConfigHandler
{
    public Dictionary<SocketGuild, SocketTextChannel> ModChannels { get; private set; }

    private Config config;
    private DiscordSocketClient _client;
    private readonly string configPath = Path.Combine(Directory.GetCurrentDirectory(), "config.json").Replace(@"\", @"\\");

    public ConfigHandler(DiscordSocketClient client)
    {
        ModChannels = new Dictionary<SocketGuild, SocketTextChannel>();

        _client = client;

        LoadConfig();
        client.Ready += ConvertConfig;
        client.GuildAvailable += OnJoinNewGuild;
    }

    public string GetToken() => config.token;

    public async Task OnJoinNewGuild(SocketGuild guild)
    {
        ModChannels.Add(guild, guild.DefaultChannel);
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
        public string token;
        public Dictionary<ulong, ulong> modChannels;
    }

    private async Task SaveConfig()
    {
        config.modChannels = ModChannels.ToDictionary(x => x.Key.Id, x => x.Value.Id);

        File.WriteAllText(configPath, JsonConvert.SerializeObject(config));
    }

    private Task LoadConfig()
    {
        try
        {
            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath));
        }
        catch
        {
            Console.WriteLine("No TOKEN found, please enter TOKEN:");
            config.token = Console.ReadLine();
            config.modChannels = new Dictionary<ulong, ulong>();
            SaveConfig();
        }
        return Task.CompletedTask;
    }
}
