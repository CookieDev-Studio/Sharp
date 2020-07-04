using Discord.WebSocket;
/// <summary>
/// Data class storing Strike data.
/// </summary>
public struct Strike
{
    public int Id;
    public SocketGuild guild;
    public SocketUser user;
    public SocketUser mod;
    public string reason;
    public string date;
}
