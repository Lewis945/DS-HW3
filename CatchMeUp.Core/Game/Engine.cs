using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatchMeUp.Core.Sharp;
using Microsoft.Xna.Framework;
using System.Threading;

namespace CatchMeUp.Core.Game
{
    public class Engine
    {
        private object _synch = new object();

        public int Time { get; set; } = 120000;

        public int CellSize { get; set; } = 50;

        public int Width { get; set; }
        public int Height { get; set; }

        public int Cols { get; set; }
        public int Rows { get; set; }

        public int CenterX { get; set; }
        public int CenterY { get; set; }

        public string[,] _map;

        private Random _random;

        public Player CurrentPlayer { get; set; }
        public List<Player> Players { get; set; }

        public Engine(int cols = 20, int rows = 20)
        {
            Cols = cols;
            Rows = rows;

            Width = Cols * CellSize;
            Height = Rows * CellSize;

            CenterX = Width / 2;
            CenterY = Height / 2;

            _map = new string[Cols, Rows];

            _random = new Random();

            Players = new List<Player>();

            Run();
        }

        private void Run()
        {
            var runtThread = new Thread(() =>
            {
                while (true)
                {
                    if (CurrentPlayer != null)
                    {
                        var now = DateTime.Now;

                        if (CurrentPlayer.Team == Team.Hunter && (CurrentPlayer.TimeStamp - DateTime.Now).TotalMilliseconds > Time)
                        {
                            CurrentPlayer.IsDead = true;
                            CurrentPlayer.Team = Team.Spectator;
                        }
                        else if (CurrentPlayer.Team == Team.Hunted && (CurrentPlayer.TimeStamp - DateTime.Now).TotalMilliseconds > Time)
                        {
                            CurrentPlayer.Score++;
                        }

                        Thread.Sleep(500);

                        if (!CurrentPlayer.IsDead) break;
                    }
                }
            });

            runtThread.Start();
        }

        public Tuple<int, int> GetWindowToMapPosition(float x, float y)
        {
            var nx = (int)Math.Round(x / CellSize);
            var ny = (int)Math.Round(y / CellSize);

            return new Tuple<int, int>(nx, ny);
        }

        public Vector2 GetRandomPosition()
        {
            return new Vector2(_random.Next(2, Width - Player.Offset), _random.Next(2, Height - Player.Offset));
        }

        public void MovePlayer(Player player)
        {
            var currentMapPosition = _map.IndexesOf(player.Name);
            var currentWindowPosition = GetWindowToMapPosition(player.Postion.X, player.Postion.Y);

            if (!currentMapPosition.Equals(currentWindowPosition))
            {
                if (!DetectCollision(player, currentWindowPosition.Item1, currentWindowPosition.Item2))
                {
                    lock (_synch)
                    {
                        _map[currentMapPosition.Item1, currentMapPosition.Item2] = null;
                        _map[currentWindowPosition.Item1, currentWindowPosition.Item2] = player.Name;
                    }
                }
            }
        }

        public void AddPlayer(Player player)
        {
            var currentMapPosition = GetWindowToMapPosition(player.Postion.X, player.Postion.Y);
            _map[currentMapPosition.Item1, currentMapPosition.Item2] = player.Name;
        }

        public void RemovePlayer(Player player)
        {
            var currentMapPosition = _map.IndexesOf(player.Name);
            _map[currentMapPosition.Item1, currentMapPosition.Item2] = null;
        }

        public bool HasPlayer(int x, int y)
        {
            var currentMapPosition = GetWindowToMapPosition(x, y);
            return _map[currentMapPosition.Item1, currentMapPosition.Item2] == null ? false : true;
        }

        private bool DetectCollision(Player currentPlayer, int x, int y)
        {
            var nextCell = _map[x, y];
            if (nextCell != null)
            {
                var player = Players.FirstOrDefault(p => p.Name == nextCell);
                if (player != null)
                {
                    if (player.Team == currentPlayer.Team)
                    {
                        return true;
                    }
                    else if (CurrentPlayer.Name == currentPlayer.Name && currentPlayer.Team == Team.Hunter)
                    {
                        CurrentPlayer.Score++;
                    }
                    else if (CurrentPlayer.Name == currentPlayer.Name && currentPlayer.Team == Team.Hunted)
                    {
                        CurrentPlayer.IsDead = true;
                    }
                }
            }

            return false;
        }
    }
}
