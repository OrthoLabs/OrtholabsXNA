using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OrtholabsXNA
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 512;
            graphics.PreferredBackBufferHeight = 512;
            graphics.ApplyChanges();

            heroPos = new Vector2(graphics.PreferredBackBufferWidth/2f, graphics.PreferredBackBufferHeight/2f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            hero = this.Content.Load<Texture2D>("hero");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                heroPos.Y -= 1*(float)gameTime.ElapsedGameTime.TotalSeconds * speedMultiplier;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                heroPos.Y += 1 * (float)gameTime.ElapsedGameTime.TotalSeconds * speedMultiplier;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                heroPos.X -= 1 * (float)gameTime.ElapsedGameTime.TotalSeconds * speedMultiplier;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                heroPos.X += 1 * (float)gameTime.ElapsedGameTime.TotalSeconds * speedMultiplier;
            }

            base.Update(gameTime);
        }

        private Texture2D hero;
        private Vector2 heroPos;
        private const float speedMultiplier = 180f;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null);
            spriteBatch.Draw(hero, heroPos, null, Color.White, 0f, new Vector2(hero.Width/2f, hero.Height*.75f), new Vector2(4, 4), SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
