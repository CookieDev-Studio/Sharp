using Moq;
using Sharp.Data;
using Sharp.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

public class MessageServiceTests
{
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
}