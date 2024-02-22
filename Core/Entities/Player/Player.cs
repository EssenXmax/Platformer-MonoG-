using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer_MonoG.Audio;
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
        private const int WALK_OR_ATTACK_ANIM_SPRITE_COUNT = 6;
        private const int ANIM_SPRITE_SIZE = 48;

        private const float IDLE_ANIM_FPS = 7;
        private const float ATTACK_ANIM_FPS = 12;


        private readonly RenderingStateMachine _renderStateMachine = new RenderingStateMachine();
        private SoundPool _attackSoundPool;
        private CoolDown _attackCoolDown = new CoolDown(1);



        public int DrawOrder { get; set; }
        public int UpdateOrder { get; set; }
        public float Speed { get; set; } = 50;
        public bool isFacingRight { get; set; } = true;
        public bool CanWalk
        {
            get
            {
                return _renderStateMachine.CurrentState.Name != nameof(PlayerTextureContainer.Attack1);
            }
        }


        public Vector2 Position { get; set; }
        public Vector2 _velocity;
        public Vector2 Velocity => _velocity; 

        public Player(PlayerTextureContainer textureContainer, SoundPool attackSounds)
        {
            _attackSoundPool = attackSounds;

            // IDLE ANIMATION
            _renderStateMachine.AddState(nameof(PlayerTextureContainer.Idle), new SpriteAnimation(textureContainer.Idle, IDLE_ANIM_SPRITE_COUNT, ANIM_SPRITE_SIZE, ANIM_SPRITE_SIZE)
            {
                Fps = IDLE_ANIM_FPS
            });
            _renderStateMachine.AddState(nameof(PlayerTextureContainer.IdleArmed), new SpriteAnimation(textureContainer.IdleArmed, IDLE_ANIM_SPRITE_COUNT, ANIM_SPRITE_SIZE, ANIM_SPRITE_SIZE));

            //WALK ANIMATION
            _renderStateMachine.AddState(nameof(PlayerTextureContainer.Walk), new SpriteAnimation(textureContainer.Walk, WALK_OR_ATTACK_ANIM_SPRITE_COUNT, ANIM_SPRITE_SIZE, ANIM_SPRITE_SIZE));
            _renderStateMachine.AddState(nameof(PlayerTextureContainer.WalkArmed), new SpriteAnimation(textureContainer.WalkArmed, WALK_OR_ATTACK_ANIM_SPRITE_COUNT, ANIM_SPRITE_SIZE, ANIM_SPRITE_SIZE));

            //ATTACK ANIMATION
            var attackAnim = new SpriteAnimation(textureContainer.Attack1, WALK_OR_ATTACK_ANIM_SPRITE_COUNT, ANIM_SPRITE_SIZE, ANIM_SPRITE_SIZE)
            { 
                Fps = ATTACK_ANIM_FPS
            };
            _renderStateMachine.AddState(nameof(PlayerTextureContainer.Attack1), attackAnim);
            attackAnim.AnimationCompleted += AttackAnim_AnimationCompleted;



            _renderStateMachine.SetState(nameof(PlayerTextureContainer.Idle));

            _renderStateMachine.CurrentState.Animation?.Play();
        }

        private void AttackAnim_AnimationCompleted(object sender, AnimationCompletedEventArgs e)
        {

            _renderStateMachine.SetState(nameof(PlayerTextureContainer.Idle));

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            _renderStateMachine.Draw(spriteBatch, Position, isFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally);

        }

        public void Update(GameTime gameTime)
        {

            if(_velocity.Length() > MathUtil.EPSILON)
            {
                Position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            _renderStateMachine.Update(gameTime);
            _attackCoolDown.Update(gameTime);

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

            isFacingRight = right;

            _renderStateMachine.SetState(nameof(PlayerTextureContainer.Walk));
            _renderStateMachine.CurrentState.Animation.Play();

            _velocity = new Vector2(1, 0) * Speed * direction;

        }

        public bool Attack()
        {
            if(_renderStateMachine.CurrentState?.Name == nameof(PlayerTextureContainer.Attack1))
            {
                return false;
            }

            return _attackCoolDown.Do(() =>
            {

            _attackSoundPool.PlayRandom();

            _velocity = Vector2.Zero;

            _renderStateMachine.SetState(nameof(PlayerTextureContainer.Attack1));
            _renderStateMachine.CurrentState.Animation.Play();

            });


        }
    }
}
