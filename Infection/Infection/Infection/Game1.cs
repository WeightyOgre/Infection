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

        TextDisplay debugText;

        public List<StrategyPattern> AIPeople;

        Random randomNumber;

        Color aColor;

        int minTime;

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
            AddAI();
            AddAI();
            AddAI();
            AddAI();
            AddAI();
            AddAI();
            debugText = new TextDisplay(this.Content, "test", new Vector2(0,350));

            aColor = new Color();
            minTime = 30;

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
            debugText.stringValue = Convert.ToString(gameTime.TotalGameTime);

            UpdatePlayer();

            for (int i = AIPeople.Count - 1; i >= 0; i--)
            {
                UpdateAI(i, gameTime);
            }

            base.Update(gameTime);
        }

        public void UpdateAI(int i, GameTime gameTime)
        {
            if ((int)gameTime.TotalGameTime.TotalSeconds < minTime)
            {
                AIPeople[i].IsInfected = false;
            }
            else
            {
                AIPeople[i].IsInfected = true;
                AIPeople[i].AddAIMovement();

            }
            if (AIPeople[i].IsInfected)
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
                    AIPeople[i].UpdateInfectedMovement();

                }
                if (Vector2.Distance(aPerson.Position, AIPeople[i].Position) < 2.0)
                {
                    AIPeople[i].PersonTexture = ArtAssets.InfectedPersonTexture;

                }
                if (Vector2.Distance(aPerson.Position, AIPeople[i].Position) <= 150 && Vector2.Distance(aPerson.Position, AIPeople[i].Position) >= 50)
                {
                    AIPeople[i].UpdateRotation(aPerson.Position.X, aPerson.Position.Y);
                    AIPeople[i].PersonTexture = ArtAssets.InfectedPersonTextureMoving;
                    AIPeople[i].UpdateInfectedMovement();
                }
            }

            if (!AIPeople[i].IsInfected)
            {
                AIPeople[i].PersonTexture = ArtAssets.PersonTextureMoving;
                if (!AIPeople[i].IsDestinationSet)
                {
                    AIPeople[i].setDestination(randomNumberGenerator(GraphicsDevice.Viewport.Width), randomNumberGenerator(GraphicsDevice.Viewport.Height));
                    AIPeople[i].IsDestinationSet = true;
                }
                if (Vector2.Distance(AIPeople[i].Position,AIPeople[i].Destination) <2.0)
                {
                    AIPeople[i].IsDestinationSet = false;
                }
                if (AIPeople[i].IsDestinationSet)
                {
                    AIPeople[i].UpdateRotation(AIPeople[i].Destination.X, AIPeople[i].Destination.Y);
                    AIPeople[i].UpdateInfectedMovement();
                }
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
                aPerson.UpdateInfectedMovement();
            }
            else
            {
                aPerson.PersonTexture = ArtAssets.PersonTexture;
            }
        }

        public void AddAI()
        {
            StrategyPattern aiPerson = new StrategyPattern(new Vector2(randomNumberGenerator(GraphicsDevice.Viewport.Width), randomNumberGenerator(GraphicsDevice.Viewport.Height)), new Vector2(0, 0));
            aiPerson.AddAINotInfectedMovement();
            aiPerson.PersonTexture = ArtAssets.InfectedPersonTexture;
            AIPeople.Add(aiPerson);
        }

        public int randomNumberGenerator(int maxNumber)
        {
            return randomNumber.Next(0, maxNumber);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(aColor);

            spriteBatch.Begin();
            Vector2 origin = new Vector2(aPerson.PersonTexture.Width/2, aPerson.PersonTexture.Height/2);
            spriteBatch.Draw(aPerson.PersonTexture, aPerson.Position, null, Color.White, aPerson.Rotation, origin, aPerson.Scale, SpriteEffects.None, 0f);
            for (int i = AIPeople.Count - 1; i >= 0; i--)
            {
                spriteBatch.Draw(AIPeople[i].PersonTexture, AIPeople[i].Position, null, Color.White, AIPeople[i].Rotation, origin, AIPeople[i].Scale, SpriteEffects.None, 0f);
            }
            spriteBatch.End();

            SpriteBatch fontBatch = new SpriteBatch(GraphicsDevice);

            debugText.DrawFont(fontBatch);


            base.Draw(gameTime);
        }
    }
}
