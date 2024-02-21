using Microsoft.Xna.Framework.Graphics;

namespace Platformer_MonoG.Core.Entities.Player
{
    public struct PlayerTextureContainer
    {
        public Texture2D Walk { get; set; }
        public Texture2D WalkArmed { get; set; }
        public Texture2D Idle { get; set; }
        public Texture2D IdleArmed { get; set; }
        public Texture2D Jump { get; set; }
        public Texture2D Run { get; set; }
        public Texture2D Attack1 { get; set; }
        public Texture2D Attack2 { get; set; }
        public Texture2D Attack3 { get; set; }
        public Texture2D Hurt { get; set; }
        public Texture2D Death { get; set; }
    }
}
