﻿using Microsoft.Xna.Framework.Graphics;
using System.Numerics;

namespace Platformer_MonoG.Graphics
{
    public class RenderingState
    {

        public string Name { get; }
        public IAnimation Animation { get; }

        public RenderingState(string name, IAnimation animation)
        {
            Name = name;
            Animation = animation;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            Animation?.Draw(spriteBatch, position, spriteEffects);
        }
    }
}
