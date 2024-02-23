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
        public Vector2 Velocity
        {
            get
            {
                var velo = Body.GetLinearVelocity();
                return new Vector2(velo.X, velo.Y);
            }

            set => Body.SetLinearVelocity(new Box2DX.Common.Vec2(value.X, value.Y));
        }
        public Vector2 Position
        {
            get
            {
                return new Vector2(_body.GetPosition().X, _body.GetPosition().Y);
            }

        }
        public Rectangle BoundingBox => new Rectangle(MathUtil.RoundToInt(Position.X - Size.X / 2f), MathUtil.RoundToInt(Position.Y - Size.Y / 2f), MathUtil.RoundToInt(Size.X), MathUtil.RoundToInt(Size.Y));
        public ITransformable Transformable { get; private set; }
        public Body Body => _body;

        public Collider(ITransformable transformable,Body body, Shape shape, Vector2 size)
        {
            _body = body;
            _shape = shape;
            Size = size;
            Transformable = transformable;
        }


        public void DebugDraw(SpriteBatch spriteBatch,Texture2D texture, Color color)
        {
            
            spriteBatch.Draw(texture, BoundingBox, color);
        }

        public void Update(GameTime gameTime)
        {
            //if(Transformable != null)
            //{
            //    Transformable.Position = Position;

            //}
        }

        public void ApplyForce(Vector2 force)
        {
            Point center = (Size * 0.5f).ToPoint();
            _body.ApplyForce(new Box2DX.Common.Vec2(force.X, force.Y), new Box2DX.Common.Vec2(center.X, center.Y));
        }

        public void ApplyImpulse(Vector2 impulse)
        {
            _body.ApplyImpulse(new Box2DX.Common.Vec2(impulse.X, impulse.Y),new Box2DX.Common.Vec2(Position.X, Position.Y));
        }
    }
}
