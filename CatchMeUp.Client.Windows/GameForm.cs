using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using CatchMeUp.Core.Game;
using CatchMeUp.Core.Networking.Local;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace CatchMeUp.Client
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameForm : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textureHuntedPlayerSheet;
        private Texture2D _textureHunterPlayerSheet;

        private SpriteFont _fontSprite;
        private Vector2 _fontSpritePos;

        /// <summary>
        /// Stores our player
        /// </summary>
        private Player _player;

        /// <summary>
        /// Stores other player
        /// </summary>
        private List<Player> _players;

        /// <summary>
        /// Stores game engine
        /// </summary>
        private Engine _engine;
        private int _cellSize;

        private GameMulticastPacket _packet;

        /// <summary>
        /// Game session
        /// </summary>
        private GameSession _session;

        public GameForm(GameSession session, GameSettings settings)
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);

            _engine = new Engine(session.FieldWidth, session.FieldWidth);
            _engine.Players = _players = new List<Player>();

            var cords = _engine.GetRandomPosition();
            while (_engine.HasPlayer((int)cords.X, (int)cords.Y))
            {
                cords = _engine.GetRandomPosition();
            }

            _player = new Player(settings.PlayerName, settings.Team, cords);
            _player.HandleKeyboard = true;
            _player.OnKeyPressed += (s, e) => { _engine.MovePlayer(_player); };
            _player.ViewPortWidth = _engine.Width;
            _player.ViewPortHeight = _engine.Height;
            _players.Add(_player);
            _engine.AddPlayer(_player);

            _engine.CurrentPlayer = _player;

            _cellSize = 50;
            _graphics.PreferredBackBufferWidth = _engine.Width;
            _graphics.PreferredBackBufferHeight = _engine.Height;

            Content.RootDirectory = "Content";

            _session = session;

            _packet = new GameMulticastPacket()
            {
                PlayerName = settings.PlayerName,
                Team = settings.Team
            };

            Multicaster.MulticastGameSession<GameMulticastPacket>(_session.IP, () =>
            {
                if (_player.IsDead)
                {
                    Multicaster.Send = false;
                }

                _packet.Move = _player.Move;
                _packet.IsDead = _player.IsDead;
                _packet.Score = _player.Score;
                _packet.PosX = _player.Postion.X;
                _packet.PosY = _player.Postion.Y;

                return _packet;
            });

            Multicaster.ListenToGameSession<GameMulticastPacket>(_session.IP,
                // Listen callback
                (p, sender) =>
                {
                    if (_textureHunterPlayerSheet != null && _textureHuntedPlayerSheet != null && !_player.Name.Equals(p.PlayerName))
                    {
                        var player = _players.FirstOrDefault(op => op.Name.Equals(p.PlayerName));
                        if (player == null)
                        {
                            player = new Player(p.PlayerName, p.Team, new Vector2(p.PosX, p.PosY));
                            player.Texture = p.Team == Team.Hunter ? _textureHunterPlayerSheet : _textureHuntedPlayerSheet;
                            player.ViewPortWidth = _engine.Width;
                            player.ViewPortHeight = _engine.Height;
                            _players.Add(player);
                            _engine.AddPlayer(player);
                        }

                        if (!p.IsDead)
                        {
                            player.Move = p.Move;
                            player.IsDead = p.IsDead;
                            player.Team = p.Team;

                            if (player.Postion.X != p.PosX && player.Postion.Y != p.PosY)
                            {
                                player.Postion = new Vector2(p.PosX, p.PosY);
                            }
                            _engine.MovePlayer(player);
                        }
                        else
                        {
                            _engine.RemovePlayer(player);
                        }

                        System.Diagnostics.Debug.WriteLine(p.PlayerName + "-" + p.Move);
                    }
                }
                //// Send response
                //() =>
                //{
                //    if (_player.IsDead)
                //    {
                //        Multicaster.Send = false;
                //    }

                //    _packet.Move = _player.Move;
                //    _packet.Team = _player.Team;
                //    _packet.IsDead = _player.IsDead;
                //    _packet.Score = _player.Score;
                //    _packet.PosX = _player.Postion.X;
                //    _packet.PosY = _player.Postion.Y;

                //    return _packet;
                //}
                );
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _textureHuntedPlayerSheet = Content.Load<Texture2D>("huntedPlayerSheet");
            _textureHunterPlayerSheet = Content.Load<Texture2D>("hunterPlayerSheet");

            _fontSprite = Content.Load<SpriteFont>("Font");
            _fontSpritePos = new Vector2(50, 20);

            ////Instantiates our player at the position X = 100, Y = 100;
            //_player = new Player(new Vector2(100, 100));
            _player.Texture = _player.Team == Team.Hunter ? _textureHunterPlayerSheet : _textureHuntedPlayerSheet;

            //var pr = new Player("Tim", Team.Hunted, new Vector2(100, 100));
            //pr.Texture = _textureHuntedPlayerSheet;
            //pr.ViewPortWidth = _engine.Width;
            //pr.ViewPortHeight = _engine.Height;
            //_players.Add(pr);
            //_engine.AddPlayer(pr);

            //var tr = new Thread(() =>
            //{
            //    var rnd = new Random();
            //    var move = Move.None;
            //    while (!pr.IsDead)
            //    {
            //        pr.Move = move;
            //        _engine.MovePlayer(pr);

            //        Thread.Sleep(1000);

            //        move = (Move)rnd.Next(0, 4);
            //    }
            //});
            //tr.Start();

            //var tr1 = new Thread(() =>
            //{
            //    while (!pr.IsDead)
            //    {
            //        if (_engine.GetWindowToMapPosition(_player.Postion.X, _player.Postion.Y).Equals(_engine.GetWindowToMapPosition(pr.Postion.X, pr.Postion.Y)))
            //        {
            //            pr.IsDead = true;
            //        }

            //        Thread.Sleep(100);
            //    }
            //});
            //tr1.Start();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                Exit();
                Application.Exit();
            }

            // TODO: Add your update logic here

            foreach (var item in _players)
            {
                item.Update(gameTime);
            }

            base.Update(gameTime);
        }

        private void DrawGrid(SpriteBatch spriteBatch)
        {
            var texture1px = new Texture2D(GraphicsDevice, 1, 1);
            texture1px.SetData(new Color[] { Color.White });

            for (float x = 1 - _engine.Cols / 2; x < _engine.Cols / 2; x++)
            {
                var rectangle = new Rectangle((int)(_engine.CenterX + x * _cellSize), 0, 1, _engine.Height);
                spriteBatch.Draw(texture1px, rectangle, Color.Red);
            }
            for (float y = 1 - _engine.Rows / 2; y < _engine.Rows / 2; y++)
            {
                var rectangle = new Rectangle(0, (int)(_engine.CenterY + y * _cellSize), _engine.Width, 1);
                spriteBatch.Draw(texture1px, rectangle, Color.Red);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            DrawGrid(_spriteBatch);

            var output = string.Format("Score: " + _player.Score);
            var fontOrigin = _fontSprite.MeasureString(output) / 2;
            _spriteBatch.DrawString(_fontSprite, output, _fontSpritePos, Color.Black, 0, fontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            foreach (var item in _players.Where(p => !p.IsDead).OrderBy(p => p.Postion.Y))
            {
                item.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
