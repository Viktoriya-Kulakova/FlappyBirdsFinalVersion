using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Classes
{
    public class BigBirdBooster : Booster
    {
        public static Texture2D texture2D;
        private int initY;
        private Random random;

        public BigBirdBooster()
        {
            random = new Random();
            initY = DefineInitY();
            type = BigBirdBooster;
            rectangle = new Rectangle(graphics.PreferredBackBufferWidth, initY, width, height);
        }

        public int DefineInitY() => random.Next(0, graphics.PreferredBackBufferHeight - 200);
    }
}