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
    public SocketTextChannel ModChannel { get; private set; }

    private Config config;
    private DiscordSocketClient _client;
    private readonly string configPath = Path.Combine(Directory.GetCurrentDirectory(), "config.json").Replace(@"\", @"\\");

    private struct Config
    {
        public string token;
        public ulong modChannelId;
    }

    public ConfigHandler(DiscordSocketClient client)
    {
        _client = client;

        LoadConfig();
        client.Ready += GetModchannelById;
    }

    public string GetToken() => config.token;

   
    public async Task SetModChannel(SocketTextChannel channel)
    {
        await Logger.Log("Setting modchannel");
        ModChannel = channel;
        config.modChannelId = channel.Id;
        await SaveConfig();
    } 
    private Task GetModchannelById()
    {
        ModChannel = _client.GetChannel(config.modChannelId) as SocketTextChannel ?? _client.Guilds.First().DefaultChannel;
        Logger.Log(ModChannel.Name);
        return Task.CompletedTask;
    }

    private async Task SaveConfig()
    {
        await Logger.Log("saving config");
        File.WriteAllText(configPath, JsonConvert.SerializeObject(config));

        await Logger.Log("config saved");
    }

    private Task LoadConfig()
    {
        try
        {
            Logger.Log("Loading Config");
            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath));
        }
        catch
        {
            Console.WriteLine("No TOKEN found, please enter TOKEN:");
            config.token = Console.ReadLine();
            SaveConfig();
        }
        return Task.CompletedTask;
    }
}
