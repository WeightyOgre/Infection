using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infection.StrategyDesignPattern
{
    public class StrategyPattern
    {
        Person character;

        public Vector2 position;
        public Vector2 speed;

        public Texture2D personTexture;

        public float rotation;

        public float scale;

        public StrategyPattern(Vector2 position, Vector2 speed)
        {
            this.position = position;
            this.speed = speed;
            rotation = 0f;
            scale = 1f;
        }

        public void AddPlayerMovement()
        {
            character = new Infection.StrategyDesignPattern.Person(new PlayerMovement());
        }

        public void AddAIMovement()
        {
            character = new Infection.StrategyDesignPattern.Person(new AIMovement());
        }

        public void UpdateRotation(float targetX, float targetY)
        {

            Rotation = character.UpdateRotation(targetX, targetY, Position.X, Position.Y);
        }

        public void UpdateMovement()
        {
            Rotation = character.CorrectRoation(Rotation);
            Position = character.UpdateMovement(position, rotation, speed);
        }

        public Texture2D PersonTexture
        {
            get { return personTexture; }
            set { personTexture = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float XPosition
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float YPosition
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }

    }
}
