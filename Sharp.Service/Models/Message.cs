using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp.Service
{
    public struct Message
    {
        public ulong GuildId { get; set; }
        public ulong ChannelId { get; set; }
        public ulong UserId { get; set; }
        public string message { get; set; }
        public DateTime Date { get; set; }
    }
}
