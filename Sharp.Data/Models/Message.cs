using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp.Data
{
    public struct Message
    {
        public string guild_id;
        public string channel_id;
        public string user_id;
        public string message;
        public DateTime date_time;
    }
}
