using Moq;
using Sharp.Data;
using Sharp.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        for (int i = 0; i < actual.Count; i++)
        {
            Assert.Equal(1 * ((ulong)i + 1), actual[i].GuildId);
            Assert.Equal(2 * ((ulong)i + 1), actual[i].ChannelId);
            Assert.Equal(3 * ((ulong)i + 1), actual[i].UserId);
            Assert.Equal($"message{1 * (i + 1)}", actual[i].message);
            Assert.Equal(new DateTime(), actual[i].Date);
        }
    }

    [Fact]
    public void GetMessagesAsync()
    {
        var mock = new Mock<IMessageData>(MockBehavior.Strict);
        mock.Setup(x => x.GetMessagesAsync(0)).Returns(Task.FromResult(GetTestMessages()));

        var messageService = new MessageService(mock.Object);

        var actual = messageService.GetMessagesAsync(0).Result;

        for (int i = 0; i < actual.Count; i++)
        {
            Assert.Equal(1 * ((ulong)i + 1), actual[i].GuildId);
            Assert.Equal(2 * ((ulong)i + 1), actual[i].ChannelId);
            Assert.Equal(3 * ((ulong)i + 1), actual[i].UserId);
            Assert.Equal($"message{1 * (i + 1)}", actual[i].message);
            Assert.Equal(new DateTime(), actual[i].Date);
        }
    }

    [Fact]
    public void AddMessage()
    {
        var mock = new Mock<IMessageData>(MockBehavior.Strict);
        mock.Setup(x => x.AddMessage(1, 2, 3, "message", new DateTime()));
        var messageService = new MessageService(mock.Object);

        messageService.AddMessage(1, 2, 3, "message", new string[0], new DateTime());

        mock.Verify(x => x.AddMessage(1, 2, 3, "message", new DateTime()), Times.Exactly(1));
    }

    [Fact]
    public void AddMessageAsync()
    {
        var mock = new Mock<IMessageData>(MockBehavior.Strict);
        mock.Setup(x => x.AddMessageAsync(1, 2, 3, "message", new DateTime())).Returns(Task.CompletedTask);
        var messageService = new MessageService(mock.Object);

        messageService.AddMessageAsync(1, 2, 3, "message", new string[0], new DateTime());

        mock.Verify(x => x.AddMessageAsync(1, 2, 3, "message", new DateTime()), Times.Exactly(1));
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
                message = "message1",
                date_time = new DateTime()
            },
            new Sharp.Data.Message()
            {
                guild_id = "2",
                channel_id = "4",
                user_id = "6",
                message = "message2",
                date_time = new DateTime()
            }
        };
    }
}