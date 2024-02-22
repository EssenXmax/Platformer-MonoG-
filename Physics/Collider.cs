using Box2DX.Collision;
using Box2DX.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer_MonoG.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;

namespace Platformer_MonoG.Physics
{
    public class Collider
    {
        private readonly Body _body;
        private Shape _shape;
        public Vector2 Size{ get; }
        public Vector2 Position
        {
            get
            {
                return new Vector2(_body.GetPosition().X, _body.GetPosition().Y);
            }

        }
        public Rectangle BoundingBox => new Rectangle(MathUtil.RoundToInt(Position.X), MathUtil.RoundToInt(Position.Y), MathUtil.RoundToInt(Size.X), MathUtil.RoundToInt(Size.Y));

        public Collider(Body body, Shape shape, Vector2 size)
        {
            _body = body;
            _shape = shape;
            Size = size;
        }


        public void DebugDraw(SpriteBatch spriteBatch,Texture2D texture, Color color)
        {
            
            spriteBatch.Draw(texture, BoundingBox, color);
        }
    }
}
