using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer_MonoG.Core
{
    public class CoolDown
    {
        private bool _isRunning;
        public float Duration { get; }
        public float Progress { get; set; }
        public bool IsReady => Progress <= MathUtil.EPSILON;



        public CoolDown(float duration)
        {
            Duration = duration;
            
        }




        public void Update(GameTime gameTime)
        {
            if (!_isRunning)
            {
                return;
            }
            Progress += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(Progress > Duration)
            {
                Progress = 0;
                _isRunning = false;
            }

        }


        public bool Do (Action action)
        {
            if (!IsReady)
            {
                return false;
            }

            action?.Invoke();
            _isRunning = true;
            return true;
        }

    }
}
