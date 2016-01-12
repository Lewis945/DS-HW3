using CatchMeUp.Core.Game;
using CatchMeUp.Core.Networking.Local;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace CatchMeUp.Core.Game
{
    public class Player : AnimatedSprite
    {
        public Team Team { get; set; }

        /// <summary>
        /// Determines the name of the player
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Determines the direction of the current animation
        /// </summary>
        public Move Move { get; set; }

        public bool IsDead { get; set; }

        public int Score { get; set; }

        public DateTime TimeStamp { get; set; }

        //private float mySpeed = 200;
        private float mySpeed = 100;

        public Texture2D Texture
        {
            get { return sTexture; }
            set { sTexture = value; }
        }

        public Vector2 Postion
        {
            get { return sPostion; }
            set { sPostion = value; }
        }

        public bool HandleKeyboard { get; set; }

        public event EventHandler OnKeyPressed;

        public int ViewPortWidth { get; set; }
        public int ViewPortHeight { get; set; }

        public static int Offset { get; set; } = 47;

        /// <summary>
        /// The constructor of the Player class
        /// </summary>
        /// <param name="position">Initial position</param>
        public Player(string name, Team team, Vector2 position)
            : base(position)
        {
            FramesPerSecond = 10;

            //Adds all the players animations
            AddAnimation(12, 0, 0, "Down", 50, 50, new Vector2(0, 0));
            AddAnimation(1, 0, 0, "IdleDown", 50, 50, new Vector2(0, 0));
            AddAnimation(12, 50, 0, "Up", 50, 50, new Vector2(0, 0));
            AddAnimation(1, 50, 0, "IdleUp", 50, 50, new Vector2(0, 0));
            AddAnimation(8, 100, 0, "Left", 50, 50, new Vector2(0, 0));
            AddAnimation(1, 100, 0, "IdleLeft", 50, 50, new Vector2(0, 0));
            AddAnimation(8, 100, 8, "Right", 50, 50, new Vector2(0, 0));
            AddAnimation(1, 100, 8, "IdleRight", 50, 50, new Vector2(0, 0));

            //Plays our start animation
            PlayAnimation("IdleDown");

            Name = name;
            Team = team;
        }

        public Player(Vector2 position)
            : this("Player", Core.Game.Team.Hunter, position)
        {
        }

        /// <summary>
        /// Loads content specific to the player class
        /// </summary>
        public void LoadContent(ContentManager content)
        {
        }

        public override void Update(GameTime gameTime)
        {
            //Makes the player stop moving when no key is pressed
            sDirection = Vector2.Zero;

            if (HandleKeyboard)
            {
                //Handles the users input
                HandleInput(Keyboard.GetState());
            }
            else
            {
                HandleMove();
            }

            //Calculates how many seconds that has passed since last iteration of Update
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Applies our speed to our direction
            sDirection *= mySpeed;

            //Makes the movement framerate independent by multiplying with deltaTime
            sPostion += (sDirection * deltaTime);

            base.Update(gameTime);
        }

        private void HandleInput(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.W))
            {
                MoveUp();
                Move = Core.Game.Move.Up;
                OnKeyPressedAction(null);
            }
            else if (Move == Core.Game.Move.Up)
            {
                Move = Core.Game.Move.None;
            }

            else if (keyState.IsKeyDown(Keys.A))
            {
                MoveLeft();
                Move = Core.Game.Move.Left;
                OnKeyPressedAction(null);
            }
            else if (Move == Core.Game.Move.Left)
            {
                Move = Core.Game.Move.None;
            }

            else if (keyState.IsKeyDown(Keys.S))
            {
                MoveDown();
                Move = Core.Game.Move.Down;
                OnKeyPressedAction(null);
            }
            else if (Move == Core.Game.Move.Down)
            {
                Move = Core.Game.Move.None;
            }

            else if (keyState.IsKeyDown(Keys.D))
            {
                MoveRight();
                Move = Core.Game.Move.Right;
                OnKeyPressedAction(null);
            }
            else if (Move == Core.Game.Move.Right)
            {
                Move = Core.Game.Move.None;
            }

            StopMove();
        }

        private void HandleMove()
        {
            if (Move == Core.Game.Move.Up)
            {
                MoveUp();
            }
            if (Move == Core.Game.Move.Left)
            {
                MoveLeft();
            }
            if (Move == Core.Game.Move.Down)
            {
                MoveDown();
            }
            if (Move == Core.Game.Move.Right)
            {
                MoveRight();
            }

            StopMove();
        }

        private void MoveUp()
        {
            //Move char Up
            if (Postion.Y > 2)
            {
                sDirection += new Vector2(0, -1);
            }
            //sDirection += new Vector2(0, -1);
            PlayAnimation("Up");
            Direction = Core.Game.Move.Up;
        }

        private void MoveLeft()
        {
            //Move char Left
            if (Postion.X > 2)
            {
                sDirection += new Vector2(-1, 0);
            }
            //sDirection += new Vector2(-1, 0);
            PlayAnimation("Left");
            Direction = Core.Game.Move.Left;
        }

        private void MoveDown()
        {
            //Move char Down
            if (Postion.Y < ViewPortHeight - Offset)
            {
                sDirection += new Vector2(0, 1);
            }
            //sDirection += new Vector2(0, 1);
            PlayAnimation("Down");
            Direction = Core.Game.Move.Down;
        }

        private void MoveRight()
        {
            //Move char Right
            if (Postion.X < ViewPortWidth - Offset)
            {
                sDirection += new Vector2(1, 0);
            }
            //sDirection += new Vector2(1, 0);
            PlayAnimation("Right");
            Direction = Core.Game.Move.Right;
        }

        private void StopMove()
        {
            if (currentAnimation.Contains("Left"))
            {
                PlayAnimation("IdleLeft");
            }
            if (currentAnimation.Contains("Right"))
            {
                PlayAnimation("IdleRight");
            }
            if (currentAnimation.Contains("Up"))
            {
                PlayAnimation("IdleUp");
            }
            if (currentAnimation.Contains("Down"))
            {
                PlayAnimation("IdleDown");
            }

            Direction = Move.None;
        }

        /// <summary>
        /// Runs every time an animation has finished playing
        /// </summary>
        /// <param name="AnimationName">Name of the ended animation</param>
        public override void AnimationDone(string animation)
        {
        }

        protected virtual void OnKeyPressedAction(EventArgs e)
        {
            EventHandler handler = OnKeyPressed;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
