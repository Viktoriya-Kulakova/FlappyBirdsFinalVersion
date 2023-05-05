using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlappyBird.Classes
{
    public class Enemy
    {
        private Rectangle rectangle;
        private readonly int initY;
        private static Random random = new Random();
        public static int delay;
        public static Texture2D texture2D;
        public static GraphicsDeviceManager graphics;
        public static int size;
        public static int speed = 10;
        public static int inferiorLimitDelay = 50;
        private int numberImpacts;
        private bool isDestroyed;

        public Enemy()
        {
            size = 95;
            delay = 600;
            numberImpacts = 0;
            isDestroyed = false;
            initY = DefineInitY();
            rectangle = new Rectangle(graphics.PreferredBackBufferWidth, initY, size, size);
        }

        public void GoDown()
        {
            rectangle.Y += speed;
            rectangle.X += speed;
        }

        public void Move()
        {
            rectangle.X -= speed;
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

        public static void DefineDelay()
        {
            delay = random.Next(200, 2000);
        }

        private int DefineInitY()
        {
            return random.Next(0, 480 - (size / 2));
        }

        public static int Delay => delay;

        public int NumberImpacts
        {
            get
            {
                return numberImpacts;
            }
            set
            {
                numberImpacts = value;
            }
        }

        public bool IsDestroyed
        {
            get
            {
                return isDestroyed;
            }
            set
            {
                isDestroyed = value;
            }
        }
    }
}
