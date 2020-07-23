using SharpBot.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SharpBot.Service
{
    public class GuildService
    {
        readonly GuildData _guildData;
        public GuildService(GuildData guildData)
        {
            _guildData = guildData;
        }
            
        public ulong GetModChannel(ulong guildId) => GetConfig(guildId).ModChannelId;
        public Task<ulong> GetModChannelAsync(ulong guildId) => Task.FromResult(GetConfigAsync(guildId).Result.ModChannelId);

        public char GetPrefix(ulong guildId) => GetConfig(guildId).Prefix;
        public Task<char> GetPrefixAsync(ulong guildId) => Task.FromResult(GetConfigAsync(guildId).Result.Prefix);

        public bool GetMessageLog(ulong guildId) => GetConfig(guildId).MessageLog;
        public Task<bool> GetMessageLogAsync(ulong guildId) => Task.FromResult(GetConfigAsync(guildId).Result.MessageLog);

        public void SetModChannel(ulong guildId, ulong channelId) => _guildData.SetModChannel(guildId, channelId);
        public Task SetModChannelAsync(ulong guildId, ulong channelId) => _guildData.SetModChannelAsync(guildId, channelId);
            
        public void SetPrefix(ulong guildId, char prefix) => _guildData.SetPrefix(guildId, prefix);
        public Task SetPrefixAsync(ulong guildId, char prefix) => _guildData.SetPrefixAsync(guildId, prefix);
            
        public void SetMessageLog(ulong guildId, bool value) => _guildData.SetMessageLog(guildId, value);
        public Task SetMessageLogAsync(ulong guildId, bool value) => _guildData.SetMessageLogAsync(guildId, value);

        public Config GetConfig(ulong guildId)
        {
            var config = _guildData.GetGuildConfig(guildId);

            return new Config()
            {
                ModChannelId = ulong.Parse(config.mod_Channel_Id),
                Prefix = config.prefix,
                MessageLog = config.message_log
            };
        }
        public async Task<Config> GetConfigAsync(ulong guildId)
        {
            var config = await _guildData.GetGuildConfigAsync(guildId);

            return new Config()
            {
                ModChannelId = ulong.Parse(config.mod_Channel_Id),
                Prefix = config.prefix,
                MessageLog = config.message_log
            };
        }
    }
}