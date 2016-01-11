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

        /// <summary>
        /// Creates our player
        /// </summary>
        private Player _player;

        private GameMulticastPacket _packet;

        private List<Player> _otherPlayers;

        /// <summary>
        /// Game session
        /// </summary>
        private GameSession _session;

        public GameForm(GameSession session, GameMulticastPacket gameMulticastPacket)
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Instantiates our player at the position X = 100, Y = 100;
            _player = new Player(gameMulticastPacket.PlayerName, gameMulticastPacket.Team, new Vector2(gameMulticastPacket.PosX, gameMulticastPacket.PosY));
            _player.HandleKeyboard = true;

            _session = session;
            _otherPlayers = new List<Player>();

            _packet = gameMulticastPacket;

            Multicaster.ListenToGameSession<GameMulticastPacket>(_session.Ip,
                (p, sender) =>
                {
                    if (_textureHunterPlayerSheet != null && _textureHuntedPlayerSheet != null && !_player.Name.Equals(p.PlayerName))
                    {
                        var player = _otherPlayers.FirstOrDefault(op => op.Name.Equals(p.PlayerName));
                        if (player == null)
                        {
                            player = new Player(p.PlayerName, p.Team, new Vector2(p.PosX, p.PosY));
                            player.Texture = p.Team == Team.Hunter ? _textureHunterPlayerSheet : _textureHuntedPlayerSheet;
                            player.Ip = sender;
                            _otherPlayers.Add(player);
                        }

                        player.Move = p.Move;
                    }
                },
                () =>
                {
                    _packet.Move = _player.Move;

                    return _packet;
                });
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

            ////Instantiates our player at the position X = 100, Y = 100;
            //_player = new Player(new Vector2(100, 100));
            _player.Texture = _player.Team == Team.Hunter ? _textureHunterPlayerSheet : _textureHuntedPlayerSheet;

            var pr = new Player("Tim", Team.Hunted, new Vector2(100, 100));
            pr.Texture = _textureHuntedPlayerSheet;
            _otherPlayers.Add(pr);

            var tr = new Thread(() =>
            {
                var rnd = new Random();
                var move = Move.None;
                while (true)
                {
                    _otherPlayers.FirstOrDefault().Move = move;

                    Thread.Sleep(1000);

                    move = (Move)rnd.Next(1, 4);
                }
            });

            tr.Start();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //Updates our players sprite Image
            _player.Update(gameTime);

            foreach (var item in _otherPlayers)
            {
                item.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            //Draws our player on the screen
            _player.Draw(_spriteBatch);

            foreach (var item in _otherPlayers)
            {
                item.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
