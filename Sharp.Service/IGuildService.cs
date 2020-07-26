using System.Threading.Tasks;

namespace Sharp.Service
{
    public interface IGuildService
    {
        void AddConfig(ulong guildId, ulong ModchannelId, char prefix = '-', bool messagelog = false);
        Task AddConfigAsync(ulong guildId, ulong ModchannelId, char prefix = '-', bool messagelog = false);
        Config GetConfig(ulong guildId);
        Task<Config> GetConfigAsync(ulong guildId);
        bool GetMessageLog(ulong guildId);
        Task<bool> GetMessageLogAsync(ulong guildId);
        ulong GetModChannel(ulong guildId);
        Task<ulong> GetModChannelAsync(ulong guildId);
        char GetPrefix(ulong guildId);
        Task<char> GetPrefixAsync(ulong guildId);
        void SetMessageLog(ulong guildId, bool value);
        Task SetMessageLogAsync(ulong guildId, bool value);
        void SetModChannel(ulong guildId, ulong channelId);
        Task SetModChannelAsync(ulong guildId, ulong channelId);
        void SetPrefix(ulong guildId, char prefix);
        Task SetPrefixAsync(ulong guildId, char prefix);
    }
}