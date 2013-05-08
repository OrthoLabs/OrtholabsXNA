using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrtholabsXNA
{
    class Obj
    {
        public Animator graphics;
        public IntTwo position;

        public void Update(GameTime gameTime)
        {
            graphics.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphics.Draw(gameTime, spriteBatch, position, 0f);
        }
        //Position, animation, update/draw etc
    }
}
