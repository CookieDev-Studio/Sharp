using Moq;
using Moq.AutoMock;
using Sharp.Data;
using Sharp.Service;
using System;
using System.Threading.Tasks;
using Xunit;

public class GuildServiceTests
{
    private readonly Sharp.Data.Config testConfig = new Sharp.Data.Config()
    {
        mod_Channel_Id = "123456789123456789",
        prefix = '-',
        message_log = false
    };

    [Fact]
    public void GetModChannel()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.GetGuildConfig(0)).Returns(testConfig);

        var guildService = new GuildService(mock.Object);

        ulong actual = guildService.GetModChannel(0);
        ulong expected = 123456789123456789;

        Assert.Equal(expected, actual);
    }
    [Fact]
    public void GetModChannelAsync()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.GetGuildConfigAsync(0)).Returns(Task.FromResult(testConfig));

        var guildService = new GuildService(mock.Object);

        var actual = guildService.GetModChannelAsync(0);
        var expected = Task.FromResult<ulong>(123456789123456789);

        Assert.Equal(expected.Result, actual.Result);
    }

    [Fact]
    public void SetModChannel()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.SetModChannel(1, 2));
        var guildService = new GuildService(mock.Object);

        guildService.SetModChannel(1, 2);

        mock.Verify(x => x.SetModChannel(1, 2), Times.Exactly(1));
    }
    [Fact]
    public void SetModChannelAsync()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.SetModChannelAsync(1, 2)).Returns(Task.CompletedTask);
        var guildService = new GuildService(mock.Object);

        guildService.SetModChannelAsync(1, 2);

        mock.Verify(x => x.SetModChannelAsync(1, 2), Times.Exactly(1));
    }

    [Fact]
    public void GetPrefix()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.GetGuildConfig(0)).Returns(testConfig);

        var guildService = new GuildService(mock.Object);

        char actual = guildService.GetPrefix(0);
        char expected = '-';

        Assert.Equal(expected, actual);
    }
    [Fact]
    public void GetPrefixAsync()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.GetGuildConfigAsync(0)).Returns(Task.FromResult(testConfig));

        var guildService = new GuildService(mock.Object);

        var actual = guildService.GetPrefixAsync(0);
        var expected = Task.FromResult('-');

        Assert.Equal(expected.Result, actual.Result);
    }

    [Fact]
    public void SetPrefix()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.SetPrefix(1, '-'));
        var guildService = new GuildService(mock.Object);

        guildService.SetPrefix(1, '-');

        mock.Verify(x => x.SetPrefix(1, '-'), Times.Exactly(1));
    }
    [Fact]
    public void SetPrefixAsync()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.SetPrefixAsync(1, '-')).Returns(Task.FromResult(testConfig));
        var guildService = new GuildService(mock.Object);

        guildService.SetPrefixAsync(1, '-');

        mock.Verify(x => x.SetPrefixAsync(1, '-'), Times.Exactly(1));
    }

    [Fact]
    public void GetMessageLog()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.GetGuildConfig(0)).Returns(testConfig);

        var guildService = new GuildService(mock.Object);

        bool actual = guildService.GetMessageLog(0);
        bool expected = false;

        Assert.Equal(expected, actual);
    }
    [Fact]
    public void GetMessageLogAsync()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.GetGuildConfigAsync(0)).Returns(Task.FromResult(testConfig));

        var guildService = new GuildService(mock.Object);

        var actual = guildService.GetMessageLogAsync(0);
        var expected = Task.FromResult(false);

        Assert.Equal(expected.Result, actual.Result);
    }

    [Fact]
    public void SetMessageLog()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.SetMessageLog(1, false));
        var guildService = new GuildService(mock.Object);

        guildService.SetMessageLog(1, false);

        mock.Verify(x => x.SetMessageLog(1, false), Times.Exactly(1));
    }
    [Fact]
    public void SetMessageLogAsync()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.SetMessageLogAsync(1, false)).Returns(Task.CompletedTask);
        var guildService = new GuildService(mock.Object);

        guildService.SetMessageLogAsync(1, false);

        mock.Verify(x => x.SetMessageLogAsync(1, false), Times.Exactly(1));
    }

    [Fact]
    public void GetConfig()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.GetGuildConfig(0)).Returns(testConfig);

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
    [Fact]
    public void GetConfigAsync()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.GetGuildConfigAsync(0)).Returns(
            Task.FromResult(new Sharp.Data.Config()
            {
                mod_Channel_Id = "123456789123456789",
                prefix = '-',
                message_log = false
            }));

        var guildService = new GuildService(mock.Object);

        var actual = guildService.GetConfigAsync(0);
        var expected = Task.FromResult(new Sharp.Service.Config
        {
            ModChannelId = 123456789123456789,
            Prefix = '-',
            MessageLog = false
        });

        Assert.Equal(expected.Result, actual.Result);
    }

    [Fact]
    public void AddConfig()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.AddConfig(1, 2, '-', false));
        mock.Setup(x => x.AddConfig(1, 2, '!', false));
        mock.Setup(x => x.AddConfig(1, 2, '-', true));
        mock.Setup(x => x.AddConfig(1, 2, '?', true));
        var guildService = new GuildService(mock.Object);

        guildService.AddConfig(1, 2);
        guildService.AddConfig(1, 2, prefix: '!');
        guildService.AddConfig(1, 2, messagelog: true);
        guildService.AddConfig(1, 2, prefix: '?', true);

        mock.Verify(x => x.AddConfig(1, 2, '-', false), Times.Exactly(1));
        mock.Verify(x => x.AddConfig(1, 2, '!', false), Times.Exactly(1));
        mock.Verify(x => x.AddConfig(1, 2, '-', true), Times.Exactly(1));
        mock.Verify(x => x.AddConfig(1, 2, '?', true), Times.Exactly(1));
    }
    [Fact]
    public void AddConfigAsync()
    {
        var mock = new Mock<IGuildData>(MockBehavior.Strict);
        mock.Setup(x => x.AddConfigAsync(1, 2, '-', false)).Returns(Task.CompletedTask);
        mock.Setup(x => x.AddConfigAsync(1, 2, '!', false)).Returns(Task.CompletedTask);
        mock.Setup(x => x.AddConfigAsync(1, 2, '-', true)).Returns(Task.CompletedTask);
        mock.Setup(x => x.AddConfigAsync(1, 2, '?', true)).Returns(Task.CompletedTask);
        var guildService = new GuildService(mock.Object);

        guildService.AddConfigAsync(1, 2);
        guildService.AddConfigAsync(1, 2, prefix: '!');
        guildService.AddConfigAsync(1, 2, messagelog: true);
        guildService.AddConfigAsync(1, 2, prefix: '?', true);

        mock.Verify(x => x.AddConfigAsync(1, 2, '-', false), Times.Exactly(1));
        mock.Verify(x => x.AddConfigAsync(1, 2, '!', false), Times.Exactly(1));
        mock.Verify(x => x.AddConfigAsync(1, 2, '-', true), Times.Exactly(1));
        mock.Verify(x => x.AddConfigAsync(1, 2, '?', true), Times.Exactly(1));
    }
}
