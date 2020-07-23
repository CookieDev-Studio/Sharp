using SharpBot.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBot.Service
{
    public class MessageService
    {
        readonly MessageData _messageData;

        public MessageService(MessageData messageData)
        {
            _messageData = messageData;
        }

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

        private string FormatMessage(string message, string[] attachments)
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