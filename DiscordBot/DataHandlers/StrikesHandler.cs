using Discord.WebSocket;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class StrikesHandler
{
    public struct Strike
    {
        public string user;
        public string mod;
        public string reason;
        public string date;
    }

    private readonly string strikesPath = Path.Combine(Directory.GetCurrentDirectory(), "strikes.json").Replace(@"\", @"\\");

    public async Task SaveStrike(string user, string mod, string reason, string date)
    {
        Strike newStrike = new Strike()
        {
            user = user,
            mod = mod,
            reason = reason,
            date = date
        };

        StreamWriter sw = File.AppendText(strikesPath);
        await sw.WriteLineAsync(JsonConvert.SerializeObject(newStrike));
        await sw.FlushAsync();
        sw.Close();
    }

    public List<Strike> LoadStrikes(SocketUser user)
    {
        List<Strike> strikes = File.ReadAllLines(strikesPath).Select(x => JsonConvert.DeserializeObject<Strike>(x)).ToList();
        strikes = strikes.Where(x => x.user == user.Username).ToList();

        return strikes;
    }
}
