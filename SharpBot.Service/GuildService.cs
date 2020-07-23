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

        public Task<ulong> GetModChannel(ulong guildId)
        {
            return Task.FromResult(GetConfig(guildId).Result.modChannelId);
        }

        public Task<char> GetPrefix(ulong guildId)
        {
            return Task.FromResult(GetConfig(guildId).Result.prefix);
        }

        public Task<bool> GetMessageLog(ulong guildId)
        {
            return Task.FromResult(GetConfig(guildId).Result.messageLog);
        }

        public Task SetModChannel(ulong guildId, ulong channelId)
        {
            return _guildData.SetModChannel(guildId, channelId);
        }

        public Task SetPrefix(ulong guildId, char prefix)
        {
            return _guildData.SetPrefix(guildId, prefix);
        }

        public Task SetMessageLog(ulong guildId, bool value)
        {
            return _guildData.SetMessageLog(guildId, value);
        }

        private Task<Config> GetConfig(ulong guildId)
        {
            var config = _guildData.GetGuildConfig(guildId).Result;

            return Task.FromResult(new Config()
            {
                modChannelId = ulong.Parse(config.mod_Channel_Id),
                prefix = config.prefix,
                messageLog = config.message_log
            });
        }
    }
}