using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer_MonoG.Core.Entities
{
    public class EntityManager: IUpdateable
    {

        private readonly List<IGameEntity> _entities = new List<IGameEntity>();
        private readonly List<IGameEntity> _entitiesToAdd = new List<IGameEntity>();
        private readonly List<IGameEntity> _entitiesToRemove = new List<IGameEntity>();


        public EntityManager()
        {

        }

        public bool AddEntity(IGameEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (HasEntity(entity))
            {
                return false;
            }

            _entitiesToAdd.Add(entity);

            return true;
        }

        public bool RemoveEntities(IGameEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if(HasEntity(entity))
            {
                return false;
            }

            _entitiesToRemove.Add(entity);

            return true;
        }

        public bool HasEntity(IGameEntity entity) => _entities.Contains(entity) || _entitiesToAdd.Contains(entity) || _entitiesToRemove.Contains(entity);

        public void Update(GameTime gameTime)
        {

            foreach (IGameEntity entity in _entities.OrderBy(e => e.UpdateOrder))
            {
                entity.Update(gameTime);
            }

            foreach (IGameEntity entity in _entitiesToAdd)
            {
                _entities.Add(entity);
            }

            foreach (IGameEntity entity in _entitiesToRemove)
            {
                _entities.Remove(entity);
            }

        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {

            foreach (IGameEntity entity in _entities.OrderBy(e => e.DrawOrder))
            {
                entity.Draw(spriteBatch, gameTime);
            }

        }
    }

}
