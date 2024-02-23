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

    public class PhysicsManager : IPhysicsManager
    {


        public const int DEFAULT_WORLD_BOUNDS_LOWER_X = -1000;
        public const int DEFAULT_WORLD_BOUNDS_LOWER_Y = -1000;
        public const int DEFAULT_WORLD_BOUNDS_UPPER_X = 1000;
        public const int DEFAULT_WORLD_BOUNDS_UPPER_Y = 1000;

        private const int VELOCITY_ITERATIONS = 6;
        private const int POSITION_ITERATIONS = 2;



        private readonly World _world;


        public Vector2 Gravity
        {
            get => new Vector2(_world.Gravity.X, _world.Gravity.Y);
            set => _world.Gravity = new Box2DX.Common.Vec2(value.X, value.Y);
        }


        public PhysicsManager(Vector2 gravity, Vector2? lowerWorldBounds = null, Vector2? upperWorldBounds = null)
        {
            lowerWorldBounds = lowerWorldBounds ?? new Vector2(DEFAULT_WORLD_BOUNDS_LOWER_X, DEFAULT_WORLD_BOUNDS_LOWER_Y);
            upperWorldBounds = upperWorldBounds ?? new Vector2(DEFAULT_WORLD_BOUNDS_UPPER_X, DEFAULT_WORLD_BOUNDS_UPPER_Y);

            AABB boundingBox = new AABB()
            {
                LowerBound = new Box2DX.Common.Vec2(lowerWorldBounds.Value.X, lowerWorldBounds.Value.Y),
                UpperBound = new Box2DX.Common.Vec2(upperWorldBounds.Value.X, upperWorldBounds.Value.Y)
            };



            _world = new World(boundingBox, new Box2DX.Common.Vec2(gravity.X, gravity.Y), false);


            Box2DX.Common.Settings.


        }

        public void DestroyCollider(Collider collider)
        {

            if (collider is null)
            {
                throw new ArgumentNullException(nameof(collider));
            }

            _world.DestroyBody(collider.Body);

        }


        public Collider CreateCollider(ITransformable transformable, float mass, Vector2 size, bool isDynamic)
        {
            return CreateCollider(transformable,mass,size, isDynamic ? 1 : 0);
        }

        public Collider CreateCollider(ITransformable transformable, float mass, Vector2 size, float density)
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
            polygonDef.SetAsBox(size.X / 2f, size.Y / 2f);
            polygonDef.Density = density;
            polygonDef.Friction = 0;



            Shape shape = body.CreateShape(polygonDef);

            body.SetMassFromShapes();

            Collider collider = new Collider(transformable, body, shape, size);

            return collider;
        }


        public void Update(GameTime gameTime)
        {

            _world.Step((float)gameTime.ElapsedGameTime.TotalSeconds, VELOCITY_ITERATIONS, POSITION_ITERATIONS);

        }


    }
}
