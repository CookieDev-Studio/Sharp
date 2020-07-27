using Moq;
using Sharp.Data;
using Sharp.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

public class StrikeServiceTests
{
    [Fact]
    public void AddStrike()
    {
        var mock = new Mock<IStrikeData>();
        var guildService = new StrikeService(mock.Object);

        guildService.AddStrike(0,0,0,"","");

        mock.Verify(x => x.AddStrike(0, 0, 0, "", ""), Times.Exactly(1));
        mock.Verify(x => x.AddStrikeAsync(0, 0, 0, "", ""), Times.Never);
    }
    [Fact]
    public void AddStrikeAsync()
    {
        var mock = new Mock<IStrikeData>();
        var guildService = new StrikeService(mock.Object);

        guildService.AddStrikeAsync(0, 0, 0, "", "");

        mock.Verify(x => x.AddStrike(0, 0, 0, "", ""), Times.Never);
        mock.Verify(x => x.AddStrikeAsync(0, 0, 0, "", ""), Times.Exactly(1));
    }

    [Fact]
    public void RemoveStrike()
    {
        var mock = new Mock<IStrikeData>();
        var guildService = new StrikeService(mock.Object);

        guildService.RemoveStrike(0, 0);

        mock.Verify(x => x.RemoveStrike(0, 0), Times.Exactly(1));
        mock.Verify(x => x.RemoveStrikeAsync(0, 0), Times.Never);
    }
    [Fact]
    public void RemoveStrikeAsync()
    {
        var mock = new Mock<IStrikeData>();
        var guildService = new StrikeService(mock.Object);

        guildService.RemoveStrikeAsync(0, 0);

        mock.Verify(x => x.RemoveStrike(0, 0), Times.Never);
        mock.Verify(x => x.RemoveStrikeAsync(0, 0), Times.Exactly(1));
    }

    [Fact]
    public void RemoveAllStrikes()
    {
        var mock = new Mock<IStrikeData>();
        var guildService = new StrikeService(mock.Object);

        guildService.RemoveAllStrikesFromUser(0, 0);

        mock.Verify(x => x.RemoveAllStrikesFromUser(0, 0), Times.Exactly(1));
        mock.Verify(x => x.RemoveAllStrikesFromUserAsync(0, 0), Times.Never);
    }
    [Fact]
    public void RemoveAllStrikesAsync()
    {
        var mock = new Mock<IStrikeData>();
        var guildService = new StrikeService(mock.Object);

        guildService.RemoveAllStrikesFromUserAsync(0, 0);

        mock.Verify(x => x.RemoveAllStrikesFromUser(0, 0), Times.Never);
        mock.Verify(x => x.RemoveAllStrikesFromUserAsync(0, 0), Times.Exactly(1));
    }

    [Fact]
    public void GetStrikes()
    {
        var mock = new Mock<IStrikeData>(MockBehavior.Strict);
        mock.Setup(x => x.GetStrikes(0,0)).Returns(GetTestStrikes());

        var StrikeService = new StrikeService(mock.Object);

        var actual = StrikeService.GetStrikes(0, 0);

        foreach (var item in actual)
        {
            Assert.Equal<ulong>(123456789123456781, item.Guild);
            Assert.Equal<ulong>(123456789123456782, item.Mod);
            Assert.Equal<ulong>(123456789123456782, item.User);
            Assert.Equal("sample", item.Reason);
            Assert.Equal("", item.Date);
            Assert.Equal(1, item.Id);
        }
    }

    [Fact]
    public void GetStrikesAsync()
    {
        var mock = new Mock<IStrikeData>(MockBehavior.Strict);
        mock.Setup(x => x.GetStrikesAsync(0, 0)).Returns(Task.FromResult(GetTestStrikes()));

        var StrikeService = new StrikeService(mock.Object);

        var actual = StrikeService.GetStrikesAsync(0, 0);

        foreach (var item in actual.Result)
        {
            Assert.Equal<ulong>(123456789123456781, item.Guild);
            Assert.Equal<ulong>(123456789123456782, item.Mod);
            Assert.Equal<ulong>(123456789123456782, item.User);
            Assert.Equal("sample", item.Reason);
            Assert.Equal("", item.Date);
            Assert.Equal(1, item.Id);
        }
    }

    List<Sharp.Data.Strike> GetTestStrikes()
    {
        return new List<Sharp.Data.Strike>()
        {
            new Sharp.Data.Strike()
            {
                guildId = "123456789123456781",
                modId = "123456789123456782",
                userId = "123456789123456783",
                reason = "sample",
                date = "",
                Id = 1
            },
            new Sharp.Data.Strike()
            {
                guildId = "123456789123456781",
                modId = "123456789123456782",
                userId = "123456789123456783",
                reason = "sample",
                date = "",
                Id = 1
            },
            new Sharp.Data.Strike()
            {
                guildId = "123456789123456781",
                modId = "123456789123456782",
                userId = "123456789123456783",
                reason = "sample",
                date = "",
                Id = 1
            }
        };
    }
}