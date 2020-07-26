using System.Threading.Tasks;

namespace Sharp.Data
{
    public interface IGuildData
    {
        void AddConfig(ulong guildId, ulong ModchannelId, char prefix, bool messagelog);
        Task AddConfigAsync(ulong guildId, ulong ModchannelId, char prefix, bool messagelog);
        Config GetGuildConfig(ulong guildId);
        Task<Config> GetGuildConfigAsync(ulong guildId);
        void SetMessageLog(ulong guildId, bool value);
        Task SetMessageLogAsync(ulong guildId, bool value);
        void SetModChannel(ulong guildId, ulong modChannelId);
        Task SetModChannelAsync(ulong guildId, ulong modChannelId);
        void SetPrefix(ulong guildId, char prefix);
        Task SetPrefixAsync(ulong guildId, char prefix);
    }
}