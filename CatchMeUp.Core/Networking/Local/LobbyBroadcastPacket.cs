using CatchMeUp.Core.Game;
using CatchMeUp.Core.Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CatchMeUp.Core.Networking.Local
{
    [Serializable]
    public class LobbyBroadcastPacket : BytePacket<LobbyBroadcastPacket>, IGameSession
    {
        public Guid Id { get; set; }

        public string SessionName { get; set; }
        public string SessionCreator { get; set; }

        public int FieldWidth { get; set; }
        public int FieldHeight { get; set; }

        public int MaxPlayersNumber { get; set; }
        public int JoinedPlayersNumber { get; set; }

        public string Ip { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}*{3} {4}/{5}", SessionName, SessionCreator, FieldWidth, FieldHeight, JoinedPlayersNumber, MaxPlayersNumber);
        }
    }
}
