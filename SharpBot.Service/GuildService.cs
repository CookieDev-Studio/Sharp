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

        public Task<ulong> GetModChannelAsync(ulong guildId)
        {
            return Task.FromResult(GetConfigAsync(guildId).Result.modChannelId);
        }

        public Task<char> GetPrefixAsync(ulong guildId)
        {
            return Task.FromResult(GetConfigAsync(guildId).Result.prefix);
        }

        public Task<bool> GetMessageLogAsync(ulong guildId)
        {
            return Task.FromResult(GetConfigAsync(guildId).Result.messageLog);
        }

        public Task SetModChannelAsync(ulong guildId, ulong channelId)
        {
            return _guildData.SetModChannelAsync(guildId, channelId);
        }

        public Task SetPrefixAsync(ulong guildId, char prefix)
        {
            return _guildData.SetPrefixAsync(guildId, prefix);
        }

        public Task SetMessageLogAsync(ulong guildId, bool value)
        {
            return _guildData.SetMessageLogAsync(guildId, value);
        }

        private Task<Config> GetConfigAsync(ulong guildId)
        {
            var config = _guildData.GetGuildConfigAsync(guildId).Result;

            return Task.FromResult(new Config()
            {
                modChannelId = ulong.Parse(config.mod_Channel_Id),
                prefix = config.prefix,
                messageLog = config.message_log
            });
        }
    }
}