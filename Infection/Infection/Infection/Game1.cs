using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Infection
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Person aPerson;
        
        Person anotherPerson;

        GameArt ArtAssets;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {

            ArtAssets = new GameArt(this.Content);
            
            aPerson = new Person(new Vector2(300,300),new Vector2(0,0));

            anotherPerson = new Person(new Vector2(100, 100), new Vector2(0, 0));

            this.IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            MouseState mouseState;
            mouseState = Mouse.GetState();

            if (Vector2.Distance(aPerson.Position, anotherPerson.Position) >= 150)
            {
                updateAnotherPerson();
                anotherPerson.PersonTexture = ArtAssets.InfectedPersonTexture;

            }
            if (Vector2.Distance(aPerson.Position, anotherPerson.Position) <= 50 && Vector2.Distance(aPerson.Position, anotherPerson.Position) >2.0)
            {
                updateAnotherPerson();
                anotherPerson.PersonTexture = ArtAssets.InfectedPersonTextureMoving;
                MoveEntity(2.5f, anotherPerson);

            }
            if (Vector2.Distance(aPerson.Position, anotherPerson.Position) < 2.0)
            {
                anotherPerson.PersonTexture = ArtAssets.InfectedPersonTexture;

            }
            if (Vector2.Distance(aPerson.Position, anotherPerson.Position) <= 150 && Vector2.Distance(aPerson.Position, anotherPerson.Position) >= 50)
            {
                updateAnotherPerson();
                anotherPerson.PersonTexture = ArtAssets.InfectedPersonTextureMoving;
                MoveEntity(1.0f, anotherPerson);
            }

            updateAPerson();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                aPerson.PersonTexture = ArtAssets.PersonTextureMoving;
                MoveEntity(2.0f, aPerson);
            }
            else
            {
                aPerson.PersonTexture = ArtAssets.PersonTexture;
            }

            base.Update(gameTime);
        }

        public void updateAnotherPerson()
        {

                float deltaX = (aPerson.XPosition) - (anotherPerson.XPosition);
                float deltaY = (aPerson.YPosition) - (anotherPerson.YPosition);

                float angle = (float)Math.Atan2(deltaY, deltaX);
                anotherPerson.Rotation = angle;
                float offSet = (90.0f / 360) * MathHelper.Pi*2;
                anotherPerson.Rotation += offSet;

                //MathHelper.ToDegrees(f);
                //MathHelper.ToRadians(f);
        }

        //http://gamedev.stackexchange.com/questions/50793/moving-a-sprite-in-the-direction-its-facing-xna
        public void MoveEntity(float speed, Person person)
        {
            float offSet = (90.0f / 360) * MathHelper.Pi * 2;
            person.Rotation -= offSet;
            Vector2 direction = new Vector2((float)Math.Cos(person.Rotation),
                                            (float)Math.Sin(person.Rotation));
            
            direction.Normalize();
            person.Position += direction * speed;
        }

        public void updateAPerson()
        {
            MouseState mouseState;
            mouseState = Mouse.GetState();

            float deltaX = (mouseState.X) - (aPerson.XPosition);
            float deltaY = (mouseState.Y) - (aPerson.YPosition);

            float angle = (float)Math.Atan2(deltaY, deltaX);
            aPerson.Rotation = angle;
            float offSet = (90.0f / 360) * MathHelper.Pi * 2;
            aPerson.Rotation += offSet;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            Vector2 origin = new Vector2(aPerson.PersonTexture.Width/2, aPerson.PersonTexture.Height/2);
            spriteBatch.Draw(aPerson.PersonTexture, aPerson.Position, null, Color.White, aPerson.Rotation, origin, aPerson.Scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(anotherPerson.PersonTexture, anotherPerson.Position, null, Color.White, anotherPerson.Rotation, origin, anotherPerson.Scale, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
