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
        _messageService.AddMessage(guild.Id, message.Channel.Id, message.Author.Id, message.Content, message.CreatedAt.DateTime);
    }
}
