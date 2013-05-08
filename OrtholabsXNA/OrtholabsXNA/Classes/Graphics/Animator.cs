using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrtholabsXNA
{
    public struct IntTwo
    {
        public int X;
        public int Y;

        public IntTwo(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }

    public struct AnimationTiles
    {
        public string Name;
        public List<IntTwo> Tiles;

        public float ChangeSpeed;

        public AnimationTiles(string name, List<IntTwo> tiles, float changeSpeed )
        {
            Name = name;
            Tiles = tiles;
            ChangeSpeed = changeSpeed;
        }
    }

    class Animator
    {
        public List<AnimationTiles> animations = new List<AnimationTiles>();
        public Texture2D tilesheet;
        public int tileWidth, tileHeight;

        public int animationIndex = 0;
        public int animationFrame = 0;
        public double timeToNextAnim = 0.1f;

        public Animator(Texture2D tilesheet, IntTwo dimensions)
        {
            this.tilesheet = tilesheet;
            tileWidth = dimensions.X;
            tileHeight = dimensions.Y;
        }

        public AnimationTiles FindAnimation(string name, bool set = false)
        {
            var loc = -1;
            for (int index = 0; index < animations.Count; index++)
            {
                var animation = animations[index];
                if (animation.Name.ToLower() == name.ToLower())
                    loc = index;
            }

            if (loc != -1)
            {
                if (set)
                {
                    animationIndex = loc;
                }

                return animations[loc];
            }

            return animations[0];
        }

        public void Update(GameTime gameTime)
        {
            if (animations[animationIndex].ChangeSpeed > 0)
                timeToNextAnim -= gameTime.ElapsedGameTime.TotalSeconds;

            if(timeToNextAnim <= 0)
            {
                animationFrame++;

                if (animations[animationIndex].Tiles.Count < animationFrame + 1)
                    animationFrame = 0;

                timeToNextAnim = animations[animationIndex].ChangeSpeed;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IntTwo position, float layer)
        {
            if (animations[animationIndex].Tiles.Count < animationFrame + 1)
                animationFrame = 0;

            var test = animations[animationIndex].Tiles[animationFrame];
            spriteBatch.Draw(tilesheet, new Rectangle(position.X, position.Y, (int)(tileWidth * Game1.GameScale), (int)(tileHeight * Game1.GameScale)), new Rectangle(test.X * tileWidth, test.Y * tileHeight, tileWidth, tileHeight), Color.White, 0f, new Vector2(tileWidth / 2f, tileHeight / 2f), SpriteEffects.None, layer);
        }
    }
}
