using System;
using System.Threading.Tasks;

namespace Sharp.Data
{
    public interface IMessageData
    {
        void AddMessage(ulong guildId, ulong modChannelId, ulong userId, string message, DateTime dateTime);
        Task AddMessageAsync(ulong guildId, ulong modChannelId, ulong userId, string message, DateTime dateTime);
    }
}