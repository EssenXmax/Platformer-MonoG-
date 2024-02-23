using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer_MonoG.Audio;
using Platformer_MonoG.Graphics;
using Platformer_MonoG.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer_MonoG.Core.Entities.Player
{

    public class Player : IGameEntity, ITransformable
    {
        private const float PLAYER_MASS = 80;


        private const int IDLE_ANIM_SPRITE_COUNT = 4;
        private const int WALK_OR_ATTACK_ANIM_SPRITE_COUNT = 6;
        private const int ANIM_SPRITE_SIZE = 48;

        private const float IDLE_ANIM_FPS = 7;
        private const float ATTACK_ANIM_FPS = 12;


        private readonly RenderingStateMachine _renderStateMachine = new RenderingStateMachine();
        private SoundPool _attackSoundPool;
        private CoolDown _attackCoolDown = new CoolDown(1);
        
        public Collider Collider { get; private set; }


        public int DrawOrder { get; set; }
        public int UpdateOrder { get; set; }
        public float Speed { get; set; } = 200f;
        public bool isFacingRight { get; set; } = true;
        public bool CanWalk
        {
            get
            {
                return _renderStateMachine.CurrentState.Name != nameof(PlayerTextureContainer.Attack1);
            }
        }


        public Vector2 Position { get; set; }
        public Vector2 Velocity => Collider.Velocity; 

        public Player(IPlatformGame game,PlayerTextureContainer textureContainer, SoundPool attackSounds)
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





            Collider = game.Services.GetService<IPhysicsManager>().CreateCollider(this,PLAYER_MASS,new Vector2(20,48), true);



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

            if(Collider.Velocity.Length() > MathUtil.EPSILON)
            {

            }

            _renderStateMachine.Update(gameTime);
            _attackCoolDown.Update(gameTime);


            Position = Collider.Position;

        }

        public void Halt()
        {
            Collider.Velocity = new Vector2(0, Collider.Velocity.Y);

            

            _renderStateMachine.SetState(nameof(PlayerTextureContainer.Idle));
            _renderStateMachine.CurrentState.Animation.Play();
        }

        public void Move(bool right = true)
        {

            int direction = right ? 1 : -1;

            isFacingRight = right;

            _renderStateMachine.SetState(nameof(PlayerTextureContainer.Walk));
            _renderStateMachine.CurrentState.Animation.Play();

            Collider.Velocity = new Vector2(Speed * direction, Velocity.Y);


            //Collider.ApplyImpulse(new Vector2(direction * 1000, 0));
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

            Collider.Velocity = new Vector2(0, Collider.Velocity.Y);

            _renderStateMachine.SetState(nameof(PlayerTextureContainer.Attack1));
            _renderStateMachine.CurrentState.Animation.Play();

            });


        }


        public bool Jump()
        {
            Collider.ApplyImpulse(Vector2.UnitY * -200000f);

            return true;
        }


    }
}
