namespace Sharp.Service
{
    /// <summary>
    /// Data class storing Strike data.
    /// </summary>
    public struct Strike
    {
        public int Id;
        public ulong guild;
        public ulong user;
        public ulong mod;
        public string reason;
        public string date;
    }
}