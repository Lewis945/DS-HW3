using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CatchMeUp.Core.Game
{
    public class GameSession : IGameSession
    {
        public Guid Id { get; set; }

        public string SessionName { get; set; }
        public string SessionCreator { get; set; }

        public int JoinedPlayersNumber { get; set; }
        public int MaxPlayersNumber { get; set; }
        public int FieldHeight { get; set; }
        public int FieldWidth { get; set; }

        public IPEndPoint CreatorIp { get; set; }

        public string Ip{ get; set; }

        public DateTime BroadcastReceivedTimeStamp { get; set; }

        public string GetKey()
        {
            return string.Format("{0} {1} {2}*{3}", SessionName, SessionCreator, FieldWidth, FieldHeight);
        }
    }
}
