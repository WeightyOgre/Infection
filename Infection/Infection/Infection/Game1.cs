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

        StrategyPattern AIPerson;
        
        GameArt ArtAssets;

        //TextDisplay debugText;

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

            AIPerson = new StrategyPattern(new Vector2(200, 300), new Vector2(0, 0));
            AIPerson.AddAIMovement();
            AIPerson.PersonTexture = ArtAssets.InfectedPersonTexture;

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
            UpdateAI();
            
            base.Update(gameTime);
        }

        public void UpdateAI()
        {
            //ai
            if (Vector2.Distance(aPerson.Position, AIPerson.Position) >= 150)
            {
                
                AIPerson.UpdateRotation(aPerson.Position.X, aPerson.Position.Y);
                AIPerson.PersonTexture = ArtAssets.InfectedPersonTexture;

            }
            if (Vector2.Distance(aPerson.Position, AIPerson.Position) <= 50 && Vector2.Distance(aPerson.Position, AIPerson.Position) > 2.0)
            {
                AIPerson.UpdateRotation(aPerson.Position.X, aPerson.Position.Y);
                AIPerson.PersonTexture = ArtAssets.InfectedPersonTextureMoving;
                AIPerson.UpdateMovement();

            }
            if (Vector2.Distance(aPerson.Position, AIPerson.Position) < 2.0)
            {
                AIPerson.PersonTexture = ArtAssets.InfectedPersonTexture;

            }
            if (Vector2.Distance(aPerson.Position, AIPerson.Position) <= 150 && Vector2.Distance(aPerson.Position, AIPerson.Position) >= 50)
            {
                AIPerson.UpdateRotation(aPerson.Position.X, aPerson.Position.Y);
                AIPerson.PersonTexture = ArtAssets.InfectedPersonTextureMoving;
                AIPerson.UpdateMovement();
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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            Vector2 origin = new Vector2(aPerson.PersonTexture.Width/2, aPerson.PersonTexture.Height/2);
            spriteBatch.Draw(aPerson.PersonTexture, aPerson.Position, null, Color.White, aPerson.Rotation, origin, aPerson.Scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(AIPerson.PersonTexture, AIPerson.Position, null, Color.White, AIPerson.Rotation, origin, AIPerson.Scale, SpriteEffects.None, 0f);

            spriteBatch.End();

            //SpriteBatch fontBatch = new SpriteBatch(GraphicsDevice);

            //debugText.DrawFont(fontBatch);


            base.Draw(gameTime);
        }
    }
}
