using CatchMeUp.Client;
using CatchMeUp.Core.Game;
using CatchMeUp.Core.Networking.DHT;
using CatchMeUp.Core.Networking.Local;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatchMeUp.WinForms
{
    public partial class Home : Form
    {
        private object _synch = new object();
        private List<GameSession> _gameSessions = new List<GameSession>();

        public Home()
        {
            InitializeComponent();

            var tmrShow = new Timer();
            tmrShow.Interval = 2000;
            tmrShow.Enabled = true;
            tmrShow.Tick += (o, e) =>
            {
                var time = DateTime.Now;
                var sessionsToRemove = _gameSessions.Where(s => (time - s.BroadcastReceivedTimeStamp).TotalMilliseconds > Broadcaster<LobbyBroadcastPacket>.Time).ToList();

                foreach (var s in sessionsToRemove)
                {
                    lock (_synch)
                    {
                        _gameSessions.Remove(s);
                        var key = string.Format("{0} {1} {2}*{3}", s.SessionName, s.SessionCreator, s.FieldWidth, s.FieldHeight);

                        for (int i = 0; i < listBoxLobby.Items.Count; i++)
                        {
                            var item = listBoxLobby.Items[i];
                            var lobbyItem = ((string)item);
                            if (lobbyItem.Contains(key))
                            {
                                var index = listBoxLobby.Items.IndexOf(item);
                                if (index >= 0)
                                {
                                    listBoxLobby.Items.RemoveAt(index);
                                }
                            }
                        }
                    }
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var game = new GameForm(new GameSession(), new GameMulticastPacket()))
            {
                game.Run();
            }
        }

        private string GetMulticastIp()
        {
            var takenIps = _gameSessions.Select(s => s.Ip).ToList();

            var random = new Random();
            var firstNumber = random.Next(225, 238);
            var secondNumber = random.Next(0, 255);
            var thirdNumber = random.Next(0, 255);
            var forthNumber = random.Next(0, 255);

            var ip = string.Format("{0}.{1}.{2}.{3}", firstNumber, secondNumber, thirdNumber, forthNumber);
            while (takenIps.Contains(ip))
            {
                firstNumber = random.Next(225, 238);
                secondNumber = random.Next(0, 255);
                thirdNumber = random.Next(0, 255);
                forthNumber = random.Next(0, 255);

                ip = string.Format("{0}.{1}.{2}.{3}", firstNumber, secondNumber, thirdNumber, forthNumber);
            }

            return ip;
        }

        private void buttonCreateGame_Click(object sender, EventArgs e)
        {
            var packet = new LobbyBroadcastPacket();
            packet.Id = Guid.NewGuid();
            packet.SessionName = !string.IsNullOrWhiteSpace(textBoxSessionName.Text) ? textBoxSessionName.Text : "New game";
            packet.SessionCreator = "Jonny" + packet.SessionName;
            packet.JoinedPlayersNumber = 0;
            packet.MaxPlayersNumber = 20;
            packet.FieldHeight = 10;
            packet.FieldWidth = 10;
            packet.Ip = GetMulticastIp();

            Broadcaster<LobbyBroadcastPacket>.BroadcastGameSession(packet);

            var gameSession = AddLobbyGameSession(packet);

            buttonListenBroadcast_Click(sender, e);

            var random = new Random();
            var multicastPacket = new GameMulticastPacket()
            {
                PlayerName = "Jonny",
                Team = Team.Hunter,
                PosX = random.Next(0, 600),
                PosY = random.Next(0, 400)
            };

            Multicaster.MulticastGameSession<GameMulticastPacket>(packet.Ip, () => { return multicastPacket; });

            using (var game = new GameForm(gameSession, multicastPacket))
            {
                game.Run();
            }
        }

        private GameSession AddLobbyGameSession(LobbyBroadcastPacket packet, IPEndPoint ip = null)
        {
            var gameSession = _gameSessions.FirstOrDefault(p => p.Id == packet.Id);

            if (gameSession == null)
            {
                gameSession = new GameSession()
                {
                    Id = packet.Id,
                    SessionName = packet.SessionName,
                    SessionCreator = packet.SessionCreator,
                    JoinedPlayersNumber = packet.JoinedPlayersNumber,
                    MaxPlayersNumber = packet.MaxPlayersNumber,
                    FieldHeight = packet.FieldHeight,
                    FieldWidth = packet.FieldWidth,
                    CreatorIp = ip,
                    Ip = packet.Ip,
                    BroadcastReceivedTimeStamp = DateTime.Now
                };
                lock (_synch) { _gameSessions.Add(gameSession); }
                listBoxLobby.Items.Add(packet.ToString());
            }
            else
            {
                gameSession.JoinedPlayersNumber = packet.JoinedPlayersNumber;
                gameSession.BroadcastReceivedTimeStamp = DateTime.Now;

                for (int i = 0; i < listBoxLobby.Items.Count; i++)
                {
                    var item = listBoxLobby.Items[i];
                    var lobbyItem = ((string)item);
                    if (lobbyItem.Contains(gameSession.GetKey()))
                    {
                        var index = listBoxLobby.Items.IndexOf(item);
                        if (index >= 0)
                        {
                            listBoxLobby.Items.RemoveAt(index);
                            listBoxLobby.Items.Insert(index, packet.ToString());
                        }
                    }
                }
            }

            return gameSession;
        }

        private void buttonListenBroadcast_Click(object sender, EventArgs e)
        {
            Broadcaster<LobbyBroadcastPacket>.ListenToGameSessions((p, ip) =>
            {
                if (!IsDisposed)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        AddLobbyGameSession(p, ip);
                    });
                }
            });
        }

        private void buttonJoinGame_Click(object sender, EventArgs e)
        {
            var selected = listBoxLobby.SelectedItem.ToString();

            var gameSession = _gameSessions.FirstOrDefault(s => selected.Contains(s.GetKey()));
            if (gameSession != null)
            {
                var random = new Random();
                using (var game = new GameForm(gameSession, new GameMulticastPacket()
                {
                    PlayerName = "Mark",
                    Team = Team.Hunted,
                    PosX = random.Next(0, 600),
                    PosY = random.Next(0, 400)
                }))
                {
                    game.Run();
                }
            }
        }
    }
}
