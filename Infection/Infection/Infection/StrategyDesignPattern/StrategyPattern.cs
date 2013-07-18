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

        public bool isInfected;

        public Vector2 destination;

        public bool isDestinationSet;

        public StrategyPattern(Vector2 position, Vector2 speed)
        {
            this.position = position;
            this.speed = speed;
            rotation = 0f;
            scale = 1f;
            isInfected = false;
            isDestinationSet = false;
        }

        public void AddPlayerMovement()
        {
            character = new Infection.StrategyDesignPattern.Person(new PlayerMovement());
        }

        public void AddAIMovement()
        {
            character = new Infection.StrategyDesignPattern.Person(new AIMovement());
        }

        public void AddAINotInfectedMovement()
        {
            character = new Infection.StrategyDesignPattern.Person(new AINotInfectedMovement());
        }

        public void UpdateRotation(float targetX, float targetY)
        {

            Rotation = character.UpdateRotation(targetX, targetY, Position.X, Position.Y);
        }

        public void UpdateNotInfectedMovement(float targetX, float targetY)
        {
            Rotation = character.CorrectRoation(Rotation);
            Position = character.UpdateMovement(position, rotation, speed);
        }

        public void UpdateInfectedMovement()
        {
            Rotation = character.CorrectRoation(Rotation);
            Position = character.UpdateMovement(position, rotation, speed);
        }

        public void setDestination(float targetX, float targetY)
        {
            destination.X = targetX;
            destination.Y = targetY;
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

        public bool IsInfected
        {
            get { return isInfected; }
            set { isInfected = value; }
        }

        public Vector2 Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public bool IsDestinationSet
        {
            get { return isDestinationSet; }
            set { isDestinationSet = value; }
        }

    }
}
