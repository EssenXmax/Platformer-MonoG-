using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer_MonoG.Core.Entities
{
    public interface IGameEntity : IUpdateable
    {

        int DrawOrder { get; }
        int UpdateOrder { get; }




        void Draw(SpriteBatch spriteBatch, GameTime gameTime);

    }
}
