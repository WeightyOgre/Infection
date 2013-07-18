using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Infection.StrategyDesignPattern
{
    class Person
    {
        public Movement movingStrategyObject;
        // Constructor
        public Person(Movement movingStrategyObject)
        {
            this.movingStrategyObject = movingStrategyObject;
        }

        public float UpdateRotation(float targetX, float targetY, float EntityX, float EntityY)
        {
            return movingStrategyObject.UpdateRotation(targetX, targetY, EntityX, EntityY);
        }

        public Vector2 UpdateMovement( Vector2 position, float rotation, Vector2 speed)
        {
            return movingStrategyObject.UpdateMovement(position, rotation);
        }

        public float CorrectRoation(float rotation)
        {
            return movingStrategyObject.CorrectRotation(rotation);
        }

    }
}
