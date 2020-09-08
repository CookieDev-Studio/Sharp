using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp.Service.Deprecated
{
    public class Link
    {
        public ulong GuildId { get; set; }
        public string Code { get; set; }
        public ulong RoleId { get; set; }
        public int Uses { get; set; }
    }
}
