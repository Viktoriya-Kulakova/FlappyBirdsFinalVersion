using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Classes
{
    public class Items
    {
        public static GraphicsDeviceManager graphics;
        public static Texture2D clickTexture;
        private Rectangle clickRectangle;
        public static Texture2D instructionsTexture;
        private Rectangle instructionsRectangle;
        public static Texture2D pauseTexture;
        private Rectangle pauseRectangle;
        public static Texture2D loseTexture;
        private Rectangle loseRectangle;

        public Items()
        {
            clickRectangle = new Rectangle(270, 180, 200, 180);
            loseRectangle = new Rectangle((graphics.PreferredBackBufferWidth / 2) - 115, (graphics.PreferredBackBufferHeight / 2) - 150, 230, 300);
            pauseRectangle = new Rectangle((graphics.PreferredBackBufferWidth / 2) - 115, (graphics.PreferredBackBufferHeight / 2) - 100, 230, 200);
            instructionsRectangle = new Rectangle((3 * (graphics.PreferredBackBufferWidth / 4)) - 230, (graphics.PreferredBackBufferHeight / 2) - 220, 230, 400);
        }

        public Rectangle ClickRectangle
        { get { return clickRectangle; } set { clickRectangle = value; } }

        public Rectangle InstructionsRectangle
        {
            get
            {
                return instructionsRectangle;
            }
            set
            {
                instructionsRectangle = value;
            }
        }

        public Rectangle PauseRectangle
        {
            get
            {
                return pauseRectangle;
            }
            set
            {
                pauseRectangle = value;
            }
        }

        public Rectangle LoseRectangle
        {
            get
            {
                return loseRectangle;
            }
            set
            {
                loseRectangle = value;
            }
        }
    }
}