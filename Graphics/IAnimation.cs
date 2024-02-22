using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Platformer_MonoG.Graphics
{
    public interface IAnimation
    {
        void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects);
        void Update(GameTime gameTime);

        void Stop();
        void Play();
        void Pause();

    }
}
