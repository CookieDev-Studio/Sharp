using Moq;
using Sharp.Data;
using Sharp.Service.Depricated;
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
        var mock = new Mock<IStrikeData>(MockBehavior.Strict);
        mock.Setup(x => x.AddStrike(1, 2, 3, "reason", "date"));
        var guildService = new StrikeService(mock.Object);

        guildService.AddStrike(1, 2, 3, "reason", "date");

        mock.Verify(x => x.AddStrike(1, 2, 3, "reason", "date"), Times.Exactly(1));
    }
    [Fact]
    public void AddStrikeAsync()
    {
        var mock = new Mock<IStrikeData>(MockBehavior.Strict);
        mock.Setup(x => x.AddStrikeAsync(1, 2, 3, "reason", "date")).Returns(Task.CompletedTask);
        var guildService = new StrikeService(mock.Object);

        guildService.AddStrikeAsync(1, 2, 3, "reason", "date");

        mock.Verify(x => x.AddStrikeAsync(1, 2, 3, "reason", "date"), Times.Exactly(1));
    }

    [Fact]
    public void RemoveStrike()
    {
        var mock = new Mock<IStrikeData>(MockBehavior.Strict);
        mock.Setup(x => x.RemoveStrike(1, 2));
        var guildService = new StrikeService(mock.Object);

        guildService.RemoveStrike(1, 2);

        mock.Verify(x => x.RemoveStrike(1, 2), Times.Exactly(1));
    }
    [Fact]
    public void RemoveStrikeAsync()
    {
        var mock = new Mock<IStrikeData>(MockBehavior.Strict);
        mock.Setup(x => x.RemoveStrikeAsync(1, 2)).Returns(Task.CompletedTask);
        var guildService = new StrikeService(mock.Object);

        guildService.RemoveStrikeAsync(1, 2);

        mock.Verify(x => x.RemoveStrikeAsync(1, 2), Times.Exactly(1));
    }

    [Fact]
    public void RemoveAllStrikesFromUser()
    {
        var mock = new Mock<IStrikeData>(MockBehavior.Strict);
        mock.Setup(x => x.RemoveAllStrikesFromUser(1, 2));
        var guildService = new StrikeService(mock.Object);

        guildService.RemoveAllStrikesFromUser(1, 2);

        mock.Verify(x => x.RemoveAllStrikesFromUser(1, 2), Times.Exactly(1));
    }
    [Fact]
    public void RemoveAllStrikesAsync()
    {
        var mock = new Mock<IStrikeData>(MockBehavior.Strict);
        mock.Setup(x => x.RemoveAllStrikesFromUserAsync(1, 2)).Returns(Task.CompletedTask);
        var guildService = new StrikeService(mock.Object);

        guildService.RemoveAllStrikesFromUserAsync(1, 2);

        mock.Verify(x => x.RemoveAllStrikesFromUserAsync(1, 2), Times.Exactly(1));
    }

    [Fact]
    public void GetStrikes()
    {
        var mock = new Mock<IStrikeData>(MockBehavior.Strict);
        mock.Setup(x => x.GetStrikes(0,0)).Returns(GetTestStrikes());

        var StrikeService = new StrikeService(mock.Object);

        var actual = StrikeService.GetStrikes(0, 0);

        for (int i = 0; i < actual.Count; i++)
        {
            Assert.Equal(1 * ((ulong)i + 1), actual[i].Guild);
            Assert.Equal(2 * ((ulong)i + 1), actual[i].Mod);
            Assert.Equal(3 * ((ulong)i + 1), actual[i].User);
            Assert.Equal($"reason{1 * (i + 1)}", actual[i].Reason);
            Assert.Equal($"date{1 * (i + 1)}", actual[i].Date);
            Assert.Equal(10 * (i + 1), actual[i].Id);
        }
    }

    [Fact]
    public void GetStrikesAsync()
    {
        var mock = new Mock<IStrikeData>(MockBehavior.Strict);
        mock.Setup(x => x.GetStrikesAsync(0, 0)).Returns(Task.FromResult(GetTestStrikes()));

        var StrikeService = new StrikeService(mock.Object);

        var actual = StrikeService.GetStrikesAsync(0, 0).Result;

        for (int i = 0; i < actual.Count; i++)
        {
            Assert.Equal(1 * ((ulong)i + 1), actual[i].Guild);
            Assert.Equal(2 * ((ulong)i + 1), actual[i].Mod);
            Assert.Equal(3 * ((ulong)i + 1), actual[i].User);
            Assert.Equal($"reason{1 * (i + 1)}", actual[i].Reason);
            Assert.Equal($"date{1 * (i + 1)}", actual[i].Date);
            Assert.Equal(10 * (i + 1), actual[i].Id);
        }
    }

    List<Sharp.Data.Strike> GetTestStrikes()
    {
        return new List<Sharp.Data.Strike>()
        {
            new Sharp.Data.Strike()
            {
                guildId = "1",
                modId = "2",
                userId = "3",
                reason = "reason1",
                date = "date1",
                Id = 10
            },
            new Sharp.Data.Strike()
            {
                guildId = "2",
                modId = "4",
                userId = "6",
                reason = "reason2",
                date = "date2",
                Id = 20
            },
        };
    }
}