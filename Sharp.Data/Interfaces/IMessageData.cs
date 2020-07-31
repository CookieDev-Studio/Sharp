using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sharp.Data
{
    public interface IMessageData
    {
        List<Message> GetMessages(ulong guildId);
        Task<List<Message>> GetMessagesAsync(ulong guildId);

        void AddMessage(ulong guildId, ulong modChannelId, ulong userId, string message, DateTime dateTime);
        Task AddMessageAsync(ulong guildId, ulong modChannelId, ulong userId, string message, DateTime dateTime);
    }
}