using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Infection.StrategyDesignPattern
{
    class PlayerMovement : Movement
    {

        private float Speed = 1.5f;

        public override float UpdateRotation(float targetX, float targetY, float entityX, float entityY)
        {
            float deltaX = (targetX) - (entityX);
            float deltaY = (targetY) - (entityY);

            float angle = (float)Math.Atan2(deltaY, deltaX);
            float offSet = (90.0f / 360) * MathHelper.Pi * 2;
            angle += offSet;

            return angle;
        }

        public override Vector2 UpdateMovement(Vector2 position, float rotation)
        {
            //http://gamedev.stackexchange.com/questions/50793/moving-a-sprite-in-the-direction-its-facing-xna
            Vector2 direction = new Vector2((float)Math.Cos(rotation),
                                            (float)Math.Sin(rotation));

            direction.Normalize();
            position += direction * Speed;
            return position;
        }

        public override float CorrectRotation(float rotation)
        {
            float offSet = (90.0f / 360) * MathHelper.Pi * 2;
            return rotation -= offSet;
        }
    }
}
