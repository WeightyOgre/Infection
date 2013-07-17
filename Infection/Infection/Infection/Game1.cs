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

        TextDisplay debugText;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            aPerson = new Person(new Vector2(300,300),new Vector2(0,0), Content.Load<Texture2D>("Person27x27"));

            anotherPerson = new Person(new Vector2(100, 100), new Vector2(0, 0), Content.Load<Texture2D>("InfectedPerson27x27"));

            debugText = new TextDisplay(this.Content, "test", new Vector2(0,0));

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

            updateDistanceText();

            if (Vector2.Distance(aPerson.Position, anotherPerson.Position) >= 150)
            {
                updateAnotherPerson();
                anotherPerson.Texture = Content.Load<Texture2D>("InfectedPerson27x27");
                debugText.stringValue += " Watching!";
            }
            if (Vector2.Distance(aPerson.Position, anotherPerson.Position) <= 50 && Vector2.Distance(aPerson.Position, anotherPerson.Position) >2.0)
            {
                updateAnotherPerson();
                anotherPerson.Texture = Content.Load<Texture2D>("InfectedPerson27x27Moving");
                MoveAnotherPerson(2.5f);
                debugText.stringValue += " Frenzied!";
            }
            if (Vector2.Distance(aPerson.Position, anotherPerson.Position) < 2.0)
            {
                anotherPerson.Texture = Content.Load<Texture2D>("InfectedPerson27x27");
                debugText.stringValue += " caught!";
            }
            if (Vector2.Distance(aPerson.Position, anotherPerson.Position) <= 150 && Vector2.Distance(aPerson.Position, anotherPerson.Position) >= 50)
            {
                updateAnotherPerson();
                anotherPerson.Texture = Content.Load<Texture2D>("InfectedPerson27x27Moving");
                MoveAnotherPerson(1.0f);
                debugText.stringValue += " Running!";
            }

            updateAPerson();

                MouseState mouseState;
                mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    aPerson.Texture = Content.Load<Texture2D>("Person27x27Moving");
                    MoveEntity(2.0f);
                }
                else
                {
                    aPerson.Texture = Content.Load<Texture2D>("Person27x27");
                }

            base.Update(gameTime);
        }

        public void updateDistanceText()
        {
            debugText.stringValue = Convert.ToString(Vector2.Distance(aPerson.Position, anotherPerson.Position));
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
        public void MoveEntity(float speed)
        {
            float offSet = (90.0f / 360) * MathHelper.Pi * 2;
            aPerson.Rotation -= offSet;
            Vector2 direction = new Vector2((float)Math.Cos(aPerson.Rotation),
                                            (float)Math.Sin(aPerson.Rotation));
            
            direction.Normalize();
            aPerson.Position += direction * speed;
            //float offSet = (90.0f / 360) * MathHelper.Pi * 2;
            //aPerson.Rotation += offSet;
        }

        public void MoveAnotherPerson(float speed)
        {
            float offSet = (90.0f / 360) * MathHelper.Pi * 2;
            anotherPerson.Rotation -= offSet;
            Vector2 direction = new Vector2((float)Math.Cos(anotherPerson.Rotation),
                                            (float)Math.Sin(anotherPerson.Rotation));

            direction.Normalize();
            anotherPerson.Position += direction * speed;
            //float offSet = (90.0f / 360) * MathHelper.Pi * 2;
            //aPerson.Rotation += offSet;
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

            SpriteBatch fontBatch = new SpriteBatch(GraphicsDevice);

            debugText.DrawFont(fontBatch);

            base.Draw(gameTime);
        }
    }
}
