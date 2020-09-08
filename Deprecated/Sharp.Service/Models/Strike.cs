namespace Sharp.Service.Deprecated
{
    /// <summary>
    /// Data class storing Strike data.
    /// </summary>
    public struct Strike
    {
        public int Id { get; set; }
        public ulong Guild { get; set; }
        public ulong User { get; set; }
        public ulong Mod { get; set; }
        public string Reason { get; set; }
        public string Date { get; set; }
    }
}