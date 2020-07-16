using Discord.WebSocket;
using SharpBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

public class MessageHandler
{
    MessageService _messageService;

    public MessageHandler(MessageService messageService)
    {
        _messageService = messageService;
    }

    public void AddMessage(SocketGuild guild, SocketMessage message)
    {
        string messageContent = message.Content;

        if (message.Attachments.Count > 0)
        {
            messageContent += "\n";
            foreach (var attachment in message.Attachments)
                messageContent += attachment.ProxyUrl + "\n";
        }

        _messageService.AddMessage(
            guild.Id,
            message.Channel.Id,
            message.Author.Id,
            messageContent,
            message.CreatedAt.DateTime);
    }
}
