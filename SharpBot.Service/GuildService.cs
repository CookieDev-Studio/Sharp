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
        readonly GuildData _guildService;
        public GuildService(GuildData guildService)
        {
            _guildService = guildService;
        }

        public async Task<ulong> GetModChannel(ulong guildId)
        {
            return await Task.Run(() => GetConfig(guildId).Result.modChannelId);
        }

        public async Task<char> GetPrefix(ulong guildId)
        {
            return await Task.Run(() => GetConfig(guildId).Result.prefix);
        }

        public async Task<bool> GetMessageLog(ulong guildId)
        {
            return await Task.Run(() => GetConfig(guildId).Result.messageLog);
        }

        public async Task SetModChannel(ulong guildId, ulong channelId)
        {
            await Task.Run(() => _guildService.SetModChannel(guildId, channelId));
        }

        public async Task SetPrefix(ulong guildId, char prefix)
        {
            await Task.Run(() => _guildService.SetPrefix(guildId, prefix));
        }

        public async Task SetMessageLog(ulong guildId, bool value)
        {
            await Task.Run(() => _guildService.SetMessageLog(guildId, value));
        }

        private async Task<Config> GetConfig(ulong guildId)
        {
            var config = await Task.Run(() => _guildService.GetGuildConfig(guildId));

            return await Task.Run(() => new Config()
            {
                modChannelId = ulong.Parse(config.mod_Channel_Id),
                prefix = config.prefix,
                messageLog = config.message_log
            });
        }
    }
}