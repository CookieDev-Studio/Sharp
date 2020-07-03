using Discord.WebSocket;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class StrikesHandler
{
    public async Task SaveStrike(SocketGuild guild, ulong userId, ulong modId, string reason, string date)
    {
        Strike newStrike = new Strike()
        {
            userId = userId,
            modId = modId,
            reason = reason,
            date = date
        };

        StreamWriter sw = File.AppendText(Path.Combine(Directory.GetCurrentDirectory(), guild.Id.ToString(), "strikes.json"));
        await sw.WriteLineAsync(JsonConvert.SerializeObject(newStrike));
        await sw.FlushAsync();
        sw.Close();
    }

    public List<Strike> LoadStrikes(SocketGuild guild, SocketUser user)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), guild.Id.ToString(), "strikes.json");

        if (!File.Exists(path))
            return new List<Strike>();

        List<Strike> strikes = File.ReadAllLines(path).Select(x => JsonConvert.DeserializeObject<Strike>(x)).ToList();
        strikes = strikes.Where(x => x.userId == user.Id).ToList();

        return strikes;
    }
}
