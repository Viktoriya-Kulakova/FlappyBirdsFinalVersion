using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Classes
{
    public class NoPointsBooster : Booster
    {
        public const int UpPosition = 0;
        public const int DownPosition = 1;
        public static Texture2D texture2D;
        public static int penalty;
        private static Random random;

        public NoPointsBooster(int position)
        {
            type = NoPointsBooster;
            random = new Random();
            width = 100;
            height = 120;
            DefinePenalty();
            if (position == UpPosition)
            {
                rectangle = new Rectangle(graphics.PreferredBackBufferWidth - (width / 2), (4 * (graphics.PreferredBackBufferHeight / 10)) - (height / 2) - height, width, height);
            }
            else
            {
                rectangle = new Rectangle(graphics.PreferredBackBufferWidth - (width / 2), (4 * (graphics.PreferredBackBufferHeight / 10)) - (height / 2) + height, width, height);
            }
        }

        public static void DefinePenalty()
        {
            penalty = random.Next(1, 15);
        }
    }
}