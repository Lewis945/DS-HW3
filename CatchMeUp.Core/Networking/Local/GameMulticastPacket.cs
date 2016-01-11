using CatchMeUp.Core.Game;
using CatchMeUp.Core.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatchMeUp.Core.Networking.Local
{
    [Serializable]
    public class GameMulticastPacket : BytePacket<GameMulticastPacket>
    {
        public string PlayerName { get; set; }
        public Team Team { get; set; }
        public Move Move { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", PlayerName, Move);
        }
    }
}
