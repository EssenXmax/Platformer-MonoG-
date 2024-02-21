using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer_MonoG.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer_MonoG.Core.Entities.Player
{

    public class Player : IGameEntity
    {
        private const int IDLE_ANIM_SPRITE_COUNT = 4;
        private const int WALK_ANIM_SPRITE_COUNT = 6;
        private const int ANIM_SPRITE_SIZE = 48;

        private const float EPSILON = 0.0001f;



        private readonly RenderingStateMachine _renderStateMachine = new RenderingStateMachine();



        public int DrawOrder { get; set; }
        public int UpdateOrder { get; set; }
        public float Speed { get; set; } = 50;
        public bool isFacingRight { get; set; } = true;


        public Vector2 Position { get; set; }
        public Vector2 _velocity;
        public Vector2 Velocity => _velocity; 

        public Player(PlayerTextureContainer textureContainer)
        {
            _renderStateMachine.AddState(nameof(PlayerTextureContainer.Idle), new SpriteAnimation(textureContainer.Idle, IDLE_ANIM_SPRITE_COUNT, ANIM_SPRITE_SIZE, ANIM_SPRITE_SIZE));
            _renderStateMachine.AddState(nameof(PlayerTextureContainer.IdleArmed), new SpriteAnimation(textureContainer.IdleArmed, IDLE_ANIM_SPRITE_COUNT, ANIM_SPRITE_SIZE, ANIM_SPRITE_SIZE));
            _renderStateMachine.AddState(nameof(PlayerTextureContainer.Walk), new SpriteAnimation(textureContainer.Walk, WALK_ANIM_SPRITE_COUNT, ANIM_SPRITE_SIZE, ANIM_SPRITE_SIZE));
            _renderStateMachine.AddState(nameof(PlayerTextureContainer.WalkArmed), new SpriteAnimation(textureContainer.WalkArmed, WALK_ANIM_SPRITE_COUNT, ANIM_SPRITE_SIZE, ANIM_SPRITE_SIZE));


            _renderStateMachine.SetState(nameof(PlayerTextureContainer.Idle));

            _renderStateMachine.CurrentState.Animation?.Play();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            _renderStateMachine.Draw(spriteBatch, Position);

        }

        public void Update(GameTime gameTime)
        {

            if(_velocity.Length() > EPSILON)
            {
                Position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            _renderStateMachine.Update(gameTime);

        }

        public void Halt()
        {
            _velocity = Vector2.Zero;

            

            _renderStateMachine.SetState(nameof(PlayerTextureContainer.Idle));
            _renderStateMachine.CurrentState.Animation.Play();
        }

        public void Move(bool right = true)
        {

            int direction = right ? 1 : -1;

            bool wasFacingRight = isFacingRight;

            isFacingRight = right;

            if (wasFacingRight != isFacingRight)
            {
                _renderStateMachine.CurrentState.Animation.Flip();
            }

            _renderStateMachine.SetState(nameof(PlayerTextureContainer.Walk));
            _renderStateMachine.CurrentState.Animation.Play();

            _velocity = new Vector2(1, 0) * Speed * direction;



        }
    }
}
