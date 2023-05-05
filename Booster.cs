using Microsoft.Xna.Framework;
//Движение бустеров
namespace FlappyBird.Classes
{
    public class Booster
    {
        public const int GiftBooster = 0;
        public const int NoPointsBooster = 1;
        public const int BigBirdBooster = 2;
        public static GraphicsDeviceManager graphics;

        protected Rectangle rectangle;
        protected int width;
        protected int height;
        protected int type;

        public Booster()
        {
            width = 95;
            height = 95;
        }

        public void Move()
        {
            rectangle.X -= 10;
        }

        public Rectangle Rectangle
        { get { return rectangle; } set { rectangle = value; } }

        public int Width
        { get { return width; } set { width = value; } }

        public int Height
        { get { return height; } set { height = value; } }

        public int Type
        { get { return type; } set { type = value; } }
    }
}