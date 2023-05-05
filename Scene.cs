using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//Здесь просто движение фонов и также возврат некоторых позиций от этих фонов
namespace FlappyBird.Classes
{
    public class Scene
    {
        private Texture2D backgroundTexture;
        private Texture2D floorTexture;
        private Rectangle backgroundRectangle;
        private Rectangle floorRectangle;
        private Rectangle backgroundRectangle2;
        private Rectangle floorRectangle2;
        public static GraphicsDeviceManager graphics;

        public Scene()
        {
            floorRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            backgroundRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            floorRectangle2 = new Rectangle(graphics.PreferredBackBufferWidth, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            backgroundRectangle2 = new Rectangle(graphics.PreferredBackBufferWidth, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        public void Move(int gameState)
        {
            if (gameState == GameController.PlayingState 
                || gameState == GameController.InitState 
                || gameState == GameController.NoPointState)
            {
                MoveBackground(1);
                MoveFloor(5);
            }
            else if (gameState == GameController.LeaveDestroyerState 
                || gameState == GameController.DestroyingState 
                || gameState == GameController.GettingGiftState 
                || gameState == GameController.TransitionState 
                || gameState == GameController.SelectBoosterState)
            {
                MoveBackground(6);
                MoveFloor(10);
            }
        }

        private void MoveBackground(int speed)
        {
            if (backgroundRectangle.X <= -graphics.PreferredBackBufferWidth)
            {
                backgroundRectangle.X = backgroundRectangle2.Right;
            }
            if (backgroundRectangle2.X <= -graphics.PreferredBackBufferWidth)
            {
                backgroundRectangle2.X = backgroundRectangle.Right;
            }
            backgroundRectangle.X -= speed;
            backgroundRectangle2.X -= speed;
        }

        private void MoveFloor(int speed)
        {
            if (floorRectangle.X <= -graphics.PreferredBackBufferWidth)
            {
                floorRectangle.X = floorRectangle2.Right;
            }
            if (floorRectangle2.X <= -graphics.PreferredBackBufferWidth)
            {
                floorRectangle2.X = floorRectangle.Right;
            }
            floorRectangle.X -= speed;
            floorRectangle2.X -= speed;
        }

        public Texture2D BackgroundTexture 
        { get { return backgroundTexture; }  set { backgroundTexture = value; } }

        public Texture2D FloorTexture
        { get {  return floorTexture; } set { floorTexture = value; } }

        public Rectangle BackgroundRectangle
        { get { return backgroundRectangle; } set { backgroundRectangle = value; } }

        public Rectangle FloorRectangle
        { get { return floorRectangle; } set { floorRectangle = value; } }

        public Rectangle BackgroundRectangle2
        { get { return backgroundRectangle2; } set { backgroundRectangle2 = value; } }

        public Rectangle FloorRectangle2
        {
            get
            {
                return floorRectangle2;
            }
            set
            {
                floorRectangle2 = value;
            }
        }
    }
}
