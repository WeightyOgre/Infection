using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infection
{
    class Person
    {

        //texture/destination/source/color/rotation/origin/scale/sprite effect/layer

        public Vector2 position;
        public Vector2 speed;

        public Texture2D personTexture;

        public float rotation;

        public float scale;

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

        public Texture2D Texture
        {
            get { return personTexture; }
            set { personTexture = value; }
        }


        public Person(Vector2 position, Vector2 speed, Texture2D personTexture)
        {
            this.position = position;
            this.speed = speed;
            this.personTexture = personTexture;
            rotation = 0f;
            scale = 1f;
        }
    }
}
