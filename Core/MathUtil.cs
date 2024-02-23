using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Platformer_MonoG.Core
{
    public static class MathUtil
    {

        public const float EPSILON = 0.0001f;

        public static bool IsRoughlyZero(this float val)
        {
            return Math.Abs(val) < EPSILON;
        }

        public static int RoundToInt(float val) => (int)Math.Round(val);
    }
}
