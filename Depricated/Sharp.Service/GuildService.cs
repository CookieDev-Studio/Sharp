using Sharp.Data.Depricated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sharp.Service.Deprecated
{
    public class GuildService
    {
        readonly IGuildData _guildData;
        public GuildService(IGuildData guildData) => _guildData = guildData;

        /// <summary>
        /// Gets the mod channel of a guild
        /// </summary>
        /// <param name="guildId"></param>
        /// <returns></returns>
        public ulong GetModChannel(ulong guildId) => GetConfig(guildId).ModChannelId;
        public Task<ulong> GetModChannelAsync(ulong guildId) => Task.FromResult(GetConfigAsync(guildId).Result.ModChannelId);
        /// <summary>
        /// Sets the Modchanel of a guild
        /// </summary>
        /// <param name="guildId"></param>
        /// <param name="channelId"></param>
        public void SetModChannel(ulong guildId, ulong channelId) => _guildData.SetModChannel(guildId, channelId);
        public Task SetModChannelAsync(ulong guildId, ulong channelId) => _guildData.SetModChannelAsync(guildId, channelId);

        /// <summary>
        /// Gets the prefix of a guild
        /// </summary>
        /// <param name="guildId"></param>
        /// <returns></returns>
        public char GetPrefix(ulong guildId) => GetConfig(guildId).Prefix;
        public Task<char> GetPrefixAsync(ulong guildId) => Task.FromResult(GetConfigAsync(guildId).Result.Prefix);
        /// <summary>
        /// Sets the prefix of a guild
        /// </summary>
        /// <param name="guildId"></param>
        /// <param name="prefix"></param>
        public void SetPrefix(ulong guildId, char prefix) => _guildData.SetPrefix(guildId, prefix);
        public Task SetPrefixAsync(ulong guildId, char prefix) => _guildData.SetPrefixAsync(guildId, prefix);

        /// <summary>
        /// Gets whether the message log is enabled or disabled for a guild
        /// </summary>
        /// <param name="guildId"></param>
        /// <returns></returns>
        public bool GetMessageLog(ulong guildId) => GetConfig(guildId).MessageLog;
        public Task<bool> GetMessageLogAsync(ulong guildId) => Task.FromResult(GetConfigAsync(guildId).Result.MessageLog);
        /// <summary>
        /// Enables or disables the message log for a guild
        /// </summary>
        /// <param name="guildId"></param>
        /// <param name="value"></param>
        public void SetMessageLog(ulong guildId, bool value) => _guildData.SetMessageLog(guildId, value);
        public Task SetMessageLogAsync(ulong guildId, bool value) => _guildData.SetMessageLogAsync(guildId, value);

        public ulong[] GetAllGuilds() => _guildData.GetAllGuilds().Select(x => ulong.Parse(x)).ToArray();
        public Task<ulong[]> GetAllGuildsAsync() => Task.FromResult(_guildData.GetAllGuildsAsync().Result.Select(x => ulong.Parse(x)).ToArray());

        /// <summary>
        /// Adds a new guild config
        /// </summary>
        /// <param name="guildId"></param>
        /// <param name="ModchannelId"></param>
        /// <param name="prefix"></param>
        /// <param name="messagelog"></param>
        public void AddConfig(ulong guildId, ulong ModchannelId, char prefix = '-', bool messagelog = false)
            => _guildData.AddConfig(guildId, ModchannelId, prefix, messagelog);
        public Task AddConfigAsync(ulong guildId, ulong ModchannelId, char prefix = '-', bool messagelog = false)
            => _guildData.AddConfigAsync(guildId, ModchannelId, prefix, messagelog);
        /// <summary>
        /// Gets the whole config of a guild
        /// </summary>
        /// <param name="guildId"></param>
        /// <returns></returns>
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