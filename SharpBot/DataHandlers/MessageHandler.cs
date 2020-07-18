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
        string formatedContent = "";

        for (int i = 0; i < message.Content.Length; i++)
        {
            if (messageContent[i] == '\'')
                formatedContent += "\\'";
            else
                formatedContent += messageContent[i];
        }

        Console.WriteLine(messageContent);
        Console.WriteLine(formatedContent);

        if (message.Attachments.Count > 0)
        {
            formatedContent += "\n";
            foreach (var attachment in message.Attachments)
                formatedContent += attachment.ProxyUrl + "\n";
        }

        _messageService.AddMessage(
            guild.Id,
            message.Channel.Id,
            message.Author.Id,
            formatedContent,
            message.CreatedAt.DateTime);
    }
}
