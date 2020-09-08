namespace Sharp.Service.Deprecated
{
    /// <summary>
    /// Configuration class to store modChannel data.
    /// </summary>
    public struct Config
    {
        public ulong ModChannelId { get; set; }
        public char Prefix { get; set; }
        public bool MessageLog { get; set; }
    }
}