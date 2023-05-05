using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlappyBird.Classes
{
    public class FireBall
    {
        private Rectangle rectangle;
        public static Texture2D texture2D;
        public static int width;
        public static int height;
        public static int speed = 10;

        public FireBall(int initX, int initY)
        {
            width = 35;
            height = 20;
            rectangle = new Rectangle(initX, initY, width, height);
        }

        public void Move()
        {
            rectangle.X += speed;
        }

        public Rectangle Rectangle
        { 
            get 
            { 
                return rectangle; 
            } 
            set 
            { 
                rectangle = value; 
            } 
        }
    }
}