﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer_MonoG.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using IUpdateable = Platformer_MonoG.Core.IUpdateable;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Platformer_MonoG.Graphics
{

    public class SpriteAnimation : IUpdateable, IAnimation
    {
        private const double DEFAULT_FPS = 10;



        public int SpriteCount { get; set; }
        public Vector2 SpriteOrigin { get; set; }
        public int SpriteWidth { get; set; }
        public int SpriteHeight { get; set; }
        public int CurrentOffset => (int)(PlaybackTime / (1 / Fps));

        public double Fps { get; set; } = DEFAULT_FPS;
        public double TotalDuration => SpriteCount * (1 / Fps);
        public double PlaybackTime { get; set; } = 0;



        public Texture2D Texture { get; set; }

        public AnimationState State { get; set; } = AnimationState.Stopped;

        public event EventHandler<AnimationCompletedEventArgs> AnimationCompleted;




        public SpriteAnimation(Texture2D texture, int spriteCount, int spriteWidth, int spriteHeight)
        {
            SpriteCount = spriteCount;
            SpriteWidth = spriteWidth;
            SpriteHeight = spriteHeight;
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            SpriteOrigin = new Vector2(spriteWidth / 2f, spriteHeight / 2f);
        }

        public void Update(GameTime gameTime)
        {

            if (State == AnimationState.Playing)
            {
                PlaybackTime += gameTime.ElapsedGameTime.TotalSeconds;

                if (PlaybackTime > TotalDuration)
                {
                    PlaybackTime = 0;
                    OnAnimationCompleted();
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            var drawRect = new Rectangle(CurrentOffset * SpriteWidth, 0, SpriteWidth, SpriteHeight);
            //spriteBatch.Draw(Texture,position, sourceRectangle: drawRect, color:Color.White,0,Vector2.Zero,1, effects:_spriteEffect,0 );
            spriteBatch.Draw(Texture, new Rectangle(position.ToPoint(), new Point(SpriteWidth, SpriteHeight)), drawRect, Color.White, 0, SpriteOrigin, spriteEffects, 0);
        }

        public void Play()
        {
            State = AnimationState.Playing;
        }

        public void Stop()
        {
            State = AnimationState.Stopped;
            PlaybackTime = 0;
            
        }

        public void Pause()
        {
            State = AnimationState.Paused;
        }

        protected virtual void OnAnimationCompleted()
        {
            var handler = AnimationCompleted;
            handler?.Invoke(this, new AnimationCompletedEventArgs());
        }
    }
}

