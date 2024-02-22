using Box2DX.Collision;
using Box2DX.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Platformer_MonoG.Physics
{
    public class PhysicsManager
    {
        
        private readonly World _world;

        public Vector2 Gravity
        {
            get => new Vector2(_world.Gravity.X, _world.Gravity.Y);
            set => _world.Gravity = new Box2DX.Common.Vec2(value.X, value.Y);
        }


        public PhysicsManager(Vector2 gravity)
        {
            AABB boundingBox = new AABB()
            { 
                LowerBound = new Box2DX.Common.Vec2(-1000,-1000),
                UpperBound = new Box2DX.Common.Vec2(1000,1000)
            };

            

            _world = new World(boundingBox, new Box2DX.Common.Vec2(gravity.X, gravity.Y),false);

            
            
        }


        public Collider CreateCollider(ITransformable transformable, float mass, Vector2 size, bool isDynamic)
        {
            BodyDef bodyDef = new BodyDef()
            {
                UserData = transformable,
                Angle = 0,
                IsBullet = false,
                Position = new Box2DX.Common.Vec2(transformable.Position.X, transformable.Position.Y),
                FixedRotation = true,
                IsSleeping = false,
                MassData = new MassData { Mass = mass },
            
            };
            
            Body body = _world.CreateBody(bodyDef);


            PolygonDef polygonDef = new PolygonDef();

            polygonDef.SetAsBox(size.X,size.Y);

            polygonDef.Density = isDynamic ? 1 : 0;

            
            Shape shape = body.CreateShape(polygonDef);

            body.SetMassFromShapes();

            Collider collider = new Collider(body, shape, size);

            return collider;
        }



        public void CreateTestBodies()
        {
            var testBodyDef = new BodyDef
            {
                AllowSleep = false,
                Position = new Box2DX.Common.Vec2(10,10)
            };
            var testBody = _world.CreateBody(testBodyDef);
            ShapeDef testShapeDef = new ShapeDef
            {
                Density = 1,
                Type = ShapeType.PolygonShape

            };


            var shape = testBody.CreateShape(testShapeDef);
            

        }

        public void Update(GameTime gameTime)
        {

            _world.Step((float)gameTime.ElapsedGameTime.TotalSeconds,6,2);

        }
    }
}
