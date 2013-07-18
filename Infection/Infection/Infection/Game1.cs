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
using Infection.StrategyDesignPattern;

namespace Infection
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        StrategyPattern aPerson;
        
        GameArt ArtAssets;

        //TextDisplay debugText;

        public List<StrategyPattern> AIPeople;

        Random randomNumber;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {

            ArtAssets = new GameArt(this.Content);

            aPerson = new StrategyPattern(new Vector2(400, 200), new Vector2(0, 0));
            aPerson.AddPlayerMovement();
            aPerson.PersonTexture = ArtAssets.PersonTexture;

            randomNumber = new Random();

            AIPeople = new List<StrategyPattern>();
            AddAI();
            AddAI();
            AddAI();
            AddAI();
            AddAI();
            //debugText = new TextDisplay(this.Content, "test", new Vector2(0,350));
            
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
            //debugText.stringValue = Convert.ToString(Vector2.Distance(aPerson.Position, AIPerson.Position));
            UpdatePlayer();
            for (int i = AIPeople.Count - 1; i >= 0; i--)
            {
                UpdateAI(i);
            }
            base.Update(gameTime);
        }

        public void UpdateAI(int i)
        {
            //ai
            if (Vector2.Distance(AIPeople[i].Position, aPerson.Position) >= 150)
            {

                AIPeople[i].UpdateRotation(aPerson.Position.X, aPerson.Position.Y);
                AIPeople[i].PersonTexture = ArtAssets.InfectedPersonTexture;

            }
            if (Vector2.Distance(aPerson.Position, AIPeople[i].Position) <= 50 && Vector2.Distance(aPerson.Position, AIPeople[i].Position) > 2.0)
            {
                AIPeople[i].UpdateRotation(aPerson.Position.X, aPerson.Position.Y);
                AIPeople[i].PersonTexture = ArtAssets.InfectedPersonTextureMoving;
                AIPeople[i].UpdateMovement();

            }
            if (Vector2.Distance(aPerson.Position, AIPeople[i].Position) < 2.0)
            {
                AIPeople[i].PersonTexture = ArtAssets.InfectedPersonTexture;

            }
            if (Vector2.Distance(aPerson.Position, AIPeople[i].Position) <= 150 && Vector2.Distance(aPerson.Position, AIPeople[i].Position) >= 50)
            {
                AIPeople[i].UpdateRotation(aPerson.Position.X, aPerson.Position.Y);
                AIPeople[i].PersonTexture = ArtAssets.InfectedPersonTextureMoving;
                AIPeople[i].UpdateMovement();
            }
        }

        public void UpdatePlayer()
        {
            MouseState mouseState;
            mouseState = Mouse.GetState();

            aPerson.UpdateRotation(mouseState.X, mouseState.Y);

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                aPerson.PersonTexture = ArtAssets.PersonTextureMoving;
                aPerson.UpdateMovement();
            }
            else
            {
                aPerson.PersonTexture = ArtAssets.PersonTexture;
            }
        }

        public void AddAI()
        {
            StrategyPattern aiPerson = new StrategyPattern(new Vector2(randomNumberGenerator(GraphicsDevice.Viewport.Width), randomNumberGenerator(GraphicsDevice.Viewport.Height)), new Vector2(0, 0));
            aiPerson.AddAIMovement();
            aiPerson.PersonTexture = ArtAssets.InfectedPersonTexture;
            AIPeople.Add(aiPerson);
        }

        public int randomNumberGenerator(int maxNumber)
        {
            return randomNumber.Next(0, maxNumber);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            Vector2 origin = new Vector2(aPerson.PersonTexture.Width/2, aPerson.PersonTexture.Height/2);
            spriteBatch.Draw(aPerson.PersonTexture, aPerson.Position, null, Color.White, aPerson.Rotation, origin, aPerson.Scale, SpriteEffects.None, 0f);
            for (int i = AIPeople.Count - 1; i >= 0; i--)
            {
                spriteBatch.Draw(AIPeople[i].PersonTexture, AIPeople[i].Position, null, Color.White, AIPeople[i].Rotation, origin, AIPeople[i].Scale, SpriteEffects.None, 0f);
            }
            spriteBatch.End();

            //SpriteBatch fontBatch = new SpriteBatch(GraphicsDevice);

            //debugText.DrawFont(fontBatch);


            base.Draw(gameTime);
        }
    }
}
