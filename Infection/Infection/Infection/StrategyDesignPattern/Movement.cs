using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Infection.StrategyDesignPattern
{
    public abstract class Movement
    {

        public abstract float UpdateRotation(float targetX, float targetY, float entityX, float entityY);
        public abstract Vector2 UpdateMovement(Vector2 position, float rotation);
        public abstract float CorrectRotation(float rotation);
    }
}
