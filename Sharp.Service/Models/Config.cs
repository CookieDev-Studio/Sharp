namespace Sharp.Service
{
    /// <summary>
    /// Configuration class to store modChannel data.
    /// </summary>
    public class Config
    {
        public ulong ModChannelId { get; set; }
        public char Prefix { get; set; }
        public bool MessageLog { get; set; }
    }
}