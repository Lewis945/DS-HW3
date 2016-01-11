using CatchMeUp.Core.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatchMeUp.Client
{   
    abstract class AnimatedSprite
    {
        #region Variables

        /// <summary>
        /// Determines the direction of the current animation
        /// </summary>
        protected Move Direction { get; set; }

        /// <summary>
        /// The texture of the sprite
        /// </summary>
        protected Texture2D sTexture;

        /// <summary>
        /// The position of the SpriteObject
        /// </summary>
        protected Vector2 sPostion;

        /// <summary>
        /// Number of frames in the animation
        /// </summary>
        private int _frameIndex;

        /// <summary>
        /// Time that has passed since last frame change 
        /// </summary>
        private double _timeElapsed;

        /// <summary>
        /// Time it takes to update a frame
        /// </summary>
        private double _timeToUpdate;

        /// <summary>
        /// Keeps track of the current animation
        /// </summary>
        protected string currentAnimation;

        /// <summary>
        /// The velocity of the SpriteObject
        /// </summary>
        protected Vector2 sDirection = Vector2.Zero;

        #endregion

        #region Properties
        /// <summary>
        /// Our time per frame is equal to 1 divided by frames per second(we are deciding FPS)
        /// </summary>
        public int FramesPerSecond
        {
            set { _timeToUpdate = (1f / value); }
        }

        #endregion

        #region Collections

        /// <summary>
        /// Dictionary that contains all animations
        /// </summary>
        private Dictionary<string, Rectangle[]> sAnimations = new Dictionary<string, Rectangle[]>();

        /// <summary>
        /// Dictionary that contains all animation offsets
        /// </summary>
        private Dictionary<string, Vector2> sOffsets = new Dictionary<string, Vector2>();

        #endregion

        /// <summary>
        /// Constructor of the AnimatedSprite
        /// </summary>
        public AnimatedSprite(Vector2 position)
        {
            sPostion = position;
        }

        /// <summary>
        /// Adds an animation to the AnimatedSprite
        /// </summary>
        public void AddAnimation(int frames, int yPos, int xStartFrame, string name, int width, int height, Vector2 offset)
        {   

            //Creates an array of rectangles which will be used when playing an animation
            Rectangle[] Rectangles = new Rectangle[frames];

            //Fills up the array of rectangles
            for (int i = 0; i < frames; i++)
            {
                Rectangles[i] = new Rectangle((i + xStartFrame) * width, yPos, width, height);
            }
            sAnimations.Add(name, Rectangles);
            sOffsets.Add(name, offset);
        }

        /// <summary>
        /// Determines when we have to change frames
        /// </summary>
        /// <param name="GameTime">GameTime</param>
        public virtual void Update(GameTime gameTime)
        {   
            //Adds time that has elapsed since our last draw
            _timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            //We need to change our image if our timeElapsed is greater than our timeToUpdate(calculated by our framerate)
            if (_timeElapsed > _timeToUpdate)
            {   
                //Resets the timer in a way, so that we keep our desired FPS
                _timeElapsed -= _timeToUpdate;

                //Adds one to our frameIndex
                if (_frameIndex < sAnimations[currentAnimation].Length -1)
                {
                    _frameIndex++;
                }
                else //Restarts the animation
                {
                    AnimationDone(currentAnimation);
                    _frameIndex = 0;
                }
            }
        }

        /// <summary>
        /// Draws the sprite on the screen
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sTexture, sPostion+sOffsets[currentAnimation], sAnimations[currentAnimation][_frameIndex], Color.White);
        }

        /// <summary>
        /// Plays an animation
        /// </summary>
        /// <param name="name">Animation to play</param>
        public void PlayAnimation(string name)
        {
            //Makes sure we won't start a new annimation unless it differs from our current animation
            if (currentAnimation != name && Direction == Move.None)
            {
                currentAnimation = name;
                _frameIndex = 0;
            }
        }

        /// <summary>
        /// Method that is called every time an animation finishes
        /// </summary>
        /// <param name="animationName">Ended animation</param>
        public abstract void AnimationDone(string animation);
    }
}
