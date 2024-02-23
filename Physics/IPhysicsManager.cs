using Microsoft.Xna.Framework;

namespace Platformer_MonoG.Physics
{
    public interface IPhysicsManager
    {
        Vector2 Gravity { get; set; }

        Collider CreateCollider(ITransformable transformable, float mass, Vector2 size, bool isDynamic);
        Collider CreateCollider(ITransformable transformable, float mass, Vector2 size, float density);
        void DestroyCollider(Collider collider);
        void Update(GameTime gameTime);
    }
}