using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Infection
{
    class GameArt
    {

        public Texture2D personTexture;
        public Texture2D personTextureMoving;
        public Texture2D infectedPersonTexture;
        public Texture2D infectedPersonTextureMoving;

        public Texture2D PersonTexture
        {
            get { return personTexture; }
            set { personTexture = value; }
        }

        public Texture2D PersonTextureMoving
        {
            get { return personTextureMoving; }
            set { personTextureMoving = value; }
        }

        public Texture2D InfectedPersonTexture
        {
            get { return infectedPersonTexture; }
            set { infectedPersonTexture = value; }
        }

        public Texture2D InfectedPersonTextureMoving
        {
            get { return infectedPersonTextureMoving; }
            set { infectedPersonTextureMoving = value; }
        }

        public GameArt(ContentManager content)
        {
            personTexture = content.Load<Texture2D>("Person27x27");
            personTextureMoving = content.Load<Texture2D>("Person27x27Moving");
            infectedPersonTexture = content.Load<Texture2D>("InfectedPerson27x27");
            infectedPersonTextureMoving = content.Load<Texture2D>("InfectedPerson27x27Moving");
        }

    }
}
