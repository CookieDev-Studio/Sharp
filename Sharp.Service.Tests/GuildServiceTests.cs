using Moq;
using Moq.AutoMock;
using Sharp.Data;
using Sharp.Service;
using System;
using Xunit;

public class GuildServiceTests
{
    [Fact]
    public void GetConfig()
    {
        var mock = new Mock<IGuildData>();
        mock.Setup(x => x.GetGuildConfig(0)).Returns(
            new Sharp.Data.Config()
            {
                mod_Channel_Id = "123456789123456789",
                prefix = '-',
                message_log = false
            });

        var guildService = new GuildService(mock.Object);

        var actual = guildService.GetConfig(0);
        var expected = new Sharp.Service.Config()
        {
            ModChannelId = 123456789123456789,
            Prefix = '-',
            MessageLog = false
        };

        Assert.Equal(expected, actual);
    }
}
