﻿using SharpBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBot.Service
{
    public class MessageService
    {
        readonly MessageData _messageService;

        public MessageService(MessageData messageService)
        {
            _messageService = messageService;
        }

        public void AddMessage(ulong guildId, ulong channelId, ulong authorId, string message, string[] attachments, DateTime dateTime)
        {
            string formatedContent = "";

            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] == '\'')
                    formatedContent += "\\'";
                else
                    formatedContent += message[i];
            }

            if (attachments.Length > 0)
            {
                formatedContent += "\n";
                foreach (var attachment in attachments)
                    formatedContent += attachment + "\n";
            }

            _messageService.AddMessage(
                guildId,
                channelId,
                authorId,
                formatedContent,
                dateTime);
        }
    }
}