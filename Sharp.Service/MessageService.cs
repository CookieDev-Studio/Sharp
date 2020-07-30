using Sharp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Service
{
    public class MessageService
    {
        readonly IMessageData _messageData;

        public MessageService(IMessageData messageData) => _messageData = messageData;
        
        public List<Message> GetMessages(ulong guildId)
        {
            return _messageData.GetMessages(guildId).Select(x => new Message()
            {
                GuildId = ulong.Parse(x.guild_id),
                ChannelId = ulong.Parse(x.channel_id),
                UserId = ulong.Parse(x.user_id),
                message = x.message,
                Date = x.date_time
            }).ToList();
        }
        public Task<List<Message>> GetMessagesAsync(ulong guildId)
        {
            return Task.FromResult(_messageData.GetMessagesAsync(guildId).Result.Select(x => new Message()
            {
                GuildId = ulong.Parse(x.guild_id),
                ChannelId = ulong.Parse(x.channel_id),
                UserId = ulong.Parse(x.user_id),
                message = x.message,
                Date = x.date_time
            }).ToList());
        }

        /// <summary>
        /// Adds a message to the message log
        /// </summary>
        /// <param name="guildId"></param>
        /// <param name="channelId"></param>
        /// <param name="authorId"></param>
        /// <param name="message"></param>
        /// <param name="attachments"></param>
        /// <param name="dateTime"></param>
        public void AddMessage(ulong guildId, ulong channelId, ulong authorId, string message, string[] attachments, DateTime dateTime)
        {
            _messageData.AddMessage(
                guildId,
                channelId,
                authorId,
                FormatMessage(message, attachments),
                dateTime);
        }
        public Task AddMessageAsync(ulong guildId, ulong channelId, ulong authorId, string message, string[] attachments, DateTime dateTime)
        {
            return _messageData.AddMessageAsync(
                guildId,
                channelId,
                authorId,
                FormatMessage(message, attachments),
                dateTime);
        }

        public string FormatMessage(string message, string[] attachments)
        {
            string formatedMessage = "";

            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] == '\'')
                    formatedMessage += "\\'";
                else
                    formatedMessage += message[i];
            }

            if (attachments.Length > 0)
            {
                formatedMessage += "\n";
                foreach (var attachment in attachments)
                    formatedMessage += attachment + "\n";
            }

            return formatedMessage;
        }
    }
}