using Moq;
using Sharp.Data;
using Sharp.Service;
using System;
using System.Collections.Generic;
using Xunit;

public class MessageServiceTests
{
    [Fact]
    public void GetMessages()
    {
        var mock = new Mock<IMessageData>(MockBehavior.Strict);
        mock.Setup(x => x.GetMessages(0)).Returns(GetTestMessages());

        var messageService = new MessageService(mock.Object);

        var actual = messageService.GetMessages(0);

        Assert.Equal<ulong>(1, actual[0].GuildId);
        Assert.Equal<ulong>(2, actual[0].ChannelId);
        Assert.Equal<ulong>(3, actual[0].UserId);
        Assert.Equal($"test1", actual[0].message);
        Assert.Equal(new DateTime(), actual[0].Date);

        Assert.Equal<ulong>(4, actual[1].GuildId);
        Assert.Equal<ulong>(5, actual[1].ChannelId);
        Assert.Equal<ulong>(6, actual[1].UserId);
        Assert.Equal($"test2", actual[1].message);
        Assert.Equal(new DateTime(), actual[1].Date);
    }

    [Fact]
    public void AddMessage()
    {
        var mock = new Mock<IMessageData>();
        var guildService = new MessageService(mock.Object);

        guildService.AddMessage(0, 0, 0, "", new string[0], new DateTime());

        mock.Verify(x => x.AddMessage(0, 0, 0, "", new DateTime()), Times.Exactly(1));
        mock.Verify(x => x.AddMessageAsync(0, 0, 0, "", new DateTime()), Times.Never);
    }

    [Fact]
    public void AddMessageAsync()
    {
        var mock = new Mock<IMessageData>();
        var guildService = new MessageService(mock.Object);

        guildService.AddMessageAsync(0, 0, 0, "", new string[0], new DateTime());

        mock.Verify(x => x.AddMessage(0, 0, 0, "", new DateTime()), Times.Never);
        mock.Verify(x => x.AddMessageAsync(0, 0, 0, "", new DateTime()), Times.Exactly(1));
    }

    [Fact]
    public void FormatMessage()
    {
        string message = "it's fine";
        string[] attachements =
            {
                "https://url",
                "https://url2"
            };

        var actual = new MessageService(null).FormatMessage(message, attachements);

        string expected =
            @"it\'s fine" + "\n" +
            "https://url\n" +
            "https://url2\n";

        Assert.Equal(expected, actual);
    }

    private List<Sharp.Data.Message> GetTestMessages()
    {
        return new List<Sharp.Data.Message>()
        {
            new Sharp.Data.Message()
            {
                guild_id = "1",
                channel_id = "2",
                user_id = "3",
                message = "test1",
                date_time = new DateTime()
            },
            new Sharp.Data.Message()
            {
                guild_id = "4",
                channel_id = "5",
                user_id = "6",
                message = "test2",
                date_time = new DateTime()
            }
        };
    }
}