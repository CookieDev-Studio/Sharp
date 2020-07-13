namespace SharpBot.Data
{
    /// <summary>
    /// Data model for guild configurations.
    /// </summary>
    public struct Config
    {
        public string? mod_Channel_Id;
        public char prefix;
        public bool message_log;
    }
}
