using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
//Птица. Установление размера, изменение под BigBird, определение, на полу птица или нет, физика вверх-вниз
namespace FlappyBird.Classes
{
    public class Bird
    {
        private Texture2D[] texture2D;
        private Rectangle rectangle;
        public static SoundEffect wingSound;
        private readonly int initX;
        private int delayShoot;
        private readonly int initY;
        private readonly int initWidth;
        private readonly int initHeight;
        private bool isUp;
        private ArrayList arrayFireBalls;
        public static GraphicsDeviceManager graphics;

        public Bird()
        {
            texture2D = new Texture2D[3];
            arrayFireBalls = new ArrayList();
            initWidth = 80;
            initHeight = 65;
            delayShoot = 0;
            isUp = false;
            initX = (graphics.PreferredBackBufferWidth / 4) - (initWidth / 2);
            initY = (4 * (graphics.PreferredBackBufferHeight / 10)) - (initHeight / 2);
            rectangle = new Rectangle(initX, initY, initWidth, initHeight);
        }

        public void SetNormalSize()
        {
            rectangle.Width = initWidth;
            rectangle.Height = initHeight;
        }

        public void SetBigSize()
        {
            rectangle.Y = 50;
            rectangle.Width = 10 * initWidth;
            rectangle.Height = 10 * initHeight;
        }

        public void SetInitPosition()
        {
            rectangle.X = initX;
            rectangle.Y = initY;
        }

        public bool IsOnFloor()
        {
            bool isOnFloor = false || rectangle.Y > graphics.PreferredBackBufferHeight - 250;
            return isOnFloor;
        }

        public void GoUp()
        {
            rectangle.Y -= 10;
        }

        public void GoDown()
        {
            rectangle.Y += 5;
        }

        public void Shoot()
        {
            arrayFireBalls.Add(new FireBall(rectangle.Right, rectangle.Y + (2 * (rectangle.Width / 3)) - (FireBall.height / 2)));
        }

        public Texture2D[] Texture2D
        { get {  return texture2D; } set { texture2D = value; } }

        public Rectangle Rectangle
        { get { return rectangle; } set { rectangle = value; } }

        public bool IsUp
        { get { return isUp; } set { isUp = value; } }

        public int DelayShoot
        { get { return delayShoot; } set { delayShoot = value; } }

        public ArrayList ArrayFireBalls
        { get {  return arrayFireBalls; } }
    }
}
