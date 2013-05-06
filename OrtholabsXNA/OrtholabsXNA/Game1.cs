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

            //TEMPORARY CODE
            heroPos = new Vector2(graphics.PreferredBackBufferWidth/2f, graphics.PreferredBackBufferHeight/2f);
            //END OF TEMPORARY CODE

            base.Initialize();
        }

        public static Color[] textureBackgroundColors = new Color[] { new Color(176, 97, 255), new Color(176, 153, 255) };
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TEMPORARY CODE
            hero = this.Content.Load<Texture2D>("Textures/Mob/Player/heroTilesheet");

            RemoveColors(ref hero, textureBackgroundColors);
            //END OF TEMPORARY CODE
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            //TEMPORARY CODE
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
            //END OF TEMPORARY CODE

            base.Update(gameTime);
        }

        //TEMPORARY CODE
        private Texture2D hero;
        private Vector2 heroPos;
        private const float speedMultiplier = 180f;

        private void RemoveColors(ref Texture2D texture, Color[] colors)
        {
            var data = new Color[texture.Width * texture.Height];
            texture.GetData(data);

            for (int i = 0; i < data.Length; i++)
            {
                foreach (var color in colors)
                {
                    if (ColorMatch(data[i], color))
                    {
                        data[i] = new Color(0, 0, 0, 0);
                    }
                }
            }

            texture.SetData(data);
        }
        private void ReplaceColor(ref Texture2D texture, Color find, Color replace)
        {
            var data = new Color[texture.Width * texture.Height];
            texture.GetData(data);

            for (int i = 0; i < data.Length; i++)
            {
                if (ColorMatch(data[i], find))
                {
                    data[i] = replace;
                }
            }

            texture.SetData(data);
        }
        private bool ColorMatch(Color a, Color b, bool checkTransparent = false)
        {
            if (a.R == b.R && a.G == b.G && a.B == b.B)
            {
                if(checkTransparent)
                {
                    return a.A == b.A;
                }
                return true;
            }
            return false;
        }

        //END OF TEMPORARY CODE

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null);
            //TEMPORARY CODE
            spriteBatch.Draw(hero, heroPos, new Rectangle(0, 0, 24, 31), Color.White, 0f, new Vector2(24/2f, 31*.75f), new Vector2(4, 4), SpriteEffects.None, 0f);
            //END OF TEMPORARY CODE

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
