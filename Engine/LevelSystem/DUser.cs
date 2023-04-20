using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch_Bot.Engine.LevelSystem
{
    public class DUser
    {
        public string UserName { get; set; }
        public int XP { get; set; }
        public int MaxXP { get; set; }
        public int Level { get; set; }
        public ulong GuildID { get; set; }
        public string avatarURL { get; set; }
        public TimeSpan Timespan { get; set; }
    }
}
