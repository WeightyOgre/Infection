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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            aPerson = new Person(new Vector2(0,0),new Vector2(0,0), Content.Load<Texture2D>("Person27x27"));

            anotherPerson = new Person(new Vector2(100, 100), new Vector2(0, 0), Content.Load<Texture2D>("InfectedPerson27x27"));

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

            //aPerson Input
            if (currentKeyboardState.IsKeyDown(Keys.Left) == true)
            {
                aPerson.XPosition--;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) == true)
            {
                aPerson.XPosition++;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) == true)
            {
                aPerson.YPosition--;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) == true)
            {
                aPerson.YPosition++;
            }
            if (currentKeyboardState.IsKeyDown(Keys.N) == true)
            {
                aPerson.Rotation -= (float)(1 * 1.0f * gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (currentKeyboardState.IsKeyDown(Keys.M) == true)
            {
                aPerson.Rotation += (float)(1 * 1.0f * gameTime.ElapsedGameTime.TotalSeconds);
            }

            //anotherPerson Input
            if (currentKeyboardState.IsKeyDown(Keys.A) == true)
            {
                anotherPerson.XPosition--;
            }
            if (currentKeyboardState.IsKeyDown(Keys.D) == true)
            {
                anotherPerson.XPosition++;
            }
            if (currentKeyboardState.IsKeyDown(Keys.W) == true)
            {
                anotherPerson.YPosition--;
            }
            if (currentKeyboardState.IsKeyDown(Keys.S) == true)
            {
                anotherPerson.YPosition++;
            }

            updateRotation();

            updateAnotherPerson();

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
        }

        public void updateRotation()
        {
            if (aPerson.Rotation >= 6.3f)
            {
                aPerson.Rotation = 0f;
                
            }
            if (aPerson.Rotation < 0f)
            {
                aPerson.Rotation = 6.3f;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            Vector2 origin = new Vector2(aPerson.Texture.Width/2, aPerson.Texture.Height/2);
            spriteBatch.Draw(aPerson.Texture, aPerson.Position, null, Color.White, aPerson.Rotation, origin, aPerson.Scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(anotherPerson.Texture, anotherPerson.Position, null, Color.White, anotherPerson.Rotation, origin, anotherPerson.Scale, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
