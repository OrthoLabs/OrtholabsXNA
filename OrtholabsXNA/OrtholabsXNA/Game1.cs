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
        public static float GameScale = 4f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = (int)Camera2D.ScreenSize.X;
            graphics.PreferredBackBufferHeight = (int)Camera2D.ScreenSize.Y;
            graphics.ApplyChanges();

            GameCamera = new Camera2D {Pos = new Vector2(Camera2D.ScreenSize.X/2, Camera2D.ScreenSize.Y/2)};

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

            player = new Obj {graphics = new Animator(hero, new IntTwo(24, 32))};
            player.graphics.animations.Add(new AnimationTiles("Down", new List<IntTwo>() { new IntTwo(0, 0), new IntTwo(1, 0), new IntTwo(2, 0), new IntTwo(3, 0), new IntTwo(4, 0), new IntTwo(5, 0), new IntTwo(6, 0), new IntTwo(7, 0) }, .075f));
            player.graphics.animations.Add(new AnimationTiles("Left", new List<IntTwo>() { new IntTwo(0, 1), new IntTwo(1, 1), new IntTwo(2, 1), new IntTwo(3, 1), new IntTwo(4, 1), new IntTwo(5, 1) }, .1f));
            player.graphics.animations.Add(new AnimationTiles("Up", new List<IntTwo>() { new IntTwo(0, 2), new IntTwo(1, 2), new IntTwo(2, 2), new IntTwo(3, 2), new IntTwo(4, 2), new IntTwo(5, 2), new IntTwo(6, 2), new IntTwo(7, 2) }, .1f));
            player.graphics.animations.Add(new AnimationTiles("Right", new List<IntTwo>() { new IntTwo(0, 3), new IntTwo(1, 3), new IntTwo(2, 3), new IntTwo(3, 3), new IntTwo(4, 3), new IntTwo(5, 3) }, .1f));

            smallSquare = new Texture2D(GraphicsDevice, 1, 1);
            smallSquare.SetData(new Color[]{Color.White});
            //END OF TEMPORARY CODE
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            //TEMPORARY CODE
            player.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                heroPos.Y = graphics.PreferredBackBufferHeight/2f;
                heroPos.X = graphics.PreferredBackBufferWidth/2f;
            }

            var moveVector = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                moveVector.Y -= 1;
                player.graphics.FindAnimation("Up", true);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                moveVector.Y += 1;
                player.graphics.FindAnimation("Down", true);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                moveVector.X -= 1;
                player.graphics.FindAnimation("Left", true);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                moveVector.X += 1;
                player.graphics.FindAnimation("Right", true);
            }

            moveVector.Normalize();
            if(moveVector.ToString().Contains("NaN")) moveVector = Vector2.Zero;
            heroPos += moveVector * (float)gameTime.ElapsedGameTime.TotalSeconds * speedMultiplier;

            //Default is 4
            if (moveVector == Vector2.Zero)
            {
                var name = player.graphics.animations[player.graphics.animationIndex].Name;
                player.graphics.animationFrame = (name != "Down") ? 3 : 4;
            }

            player.position = new IntTwo((int)heroPos.X, (int)heroPos.Y);

            if (player.position.X > 512 && (int)GameCamera.Pos.X != 512 + 256) GameCamera.Move(new Vector2(512 / 32, 0));
            if (player.position.X < 512 && (int)GameCamera.Pos.X != 0 + 256) GameCamera.Move(new Vector2(-512 / 32, 0));
            //END OF TEMPORARY CODE

            base.Update(gameTime);
        }

        //TEMPORARY CODE
        private Texture2D hero;
        private Vector2 heroPos;
        private const float speedMultiplier = 180f;
        private Obj player;
        private Texture2D smallSquare;

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

        public static Camera2D GameCamera;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //TEMPORARY CODE
            //Draw Game
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, GameCamera.get_transformation(GraphicsDevice));
            player.Draw(gameTime, spriteBatch);
            
            spriteBatch.Draw(smallSquare, new Rectangle(0, 0, 512, 512), Color.Red);
            spriteBatch.Draw(smallSquare, new Rectangle(512, 0, 512, 512), Color.Blue);
            spriteBatch.End();

            //DrawGUI
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            spriteBatch.End();

            //spriteBatch.Draw(hero, heroPos, new Rectangle(0, 0, 24, 31), Color.White, 0f, new Vector2(24/2f, 31*.75f), new Vector2(4, 4), SpriteEffects.None, 0f);
            //END OF TEMPORARY CODE

            base.Draw(gameTime);
        }
    }
}
