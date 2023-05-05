using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//Регуляция высоты, ширины, расстояние между трубами (вертикальное и горизонтальное).
namespace FlappyBird.Classes
{
    public class Pipe 
    {
        public const int FrontState = 0;
        public const int BackState = 1;
        public static GraphicsDeviceManager graphics;
        public static Texture2D topPipeTexture;
        public static Texture2D bottomPipeTexture;
        private Rectangle topPipeRectangle;
        private Rectangle bottomPipeRectangle;
        private Random random;
        private readonly int pipeHeight;
        private readonly int pipeWidth;
        private readonly int verticalDistanceBetween;
        public static int horizontalDistanceBetween;
        private readonly int position;
        private readonly int position2;
        private int state;
        private bool destroyed;

        public Pipe(int state)
        {
            random = new Random();
            pipeHeight = 620;
            pipeWidth = 85;
            destroyed = false;
            verticalDistanceBetween = 250;
            if (state == GameController.GettingGiftState || state == GameController.TransitionState)
            {
                position = -460;
                horizontalDistanceBetween = 7;
            }
            else if (state == GameController.PlayingState || state == GameController.NoPointState)
            {
                position = DefinePipePosition();
                horizontalDistanceBetween = 50;
            }
            else if (state == GameController.LeaveDestroyerState || state == GameController.DestroyingState)
            {
                position = DefinePipePosition();
                horizontalDistanceBetween = 25;
            }
            position2 = position + pipeHeight + verticalDistanceBetween;
            topPipeRectangle = new Rectangle(graphics.PreferredBackBufferWidth, position, pipeWidth, pipeHeight);
            bottomPipeRectangle = new Rectangle(graphics.PreferredBackBufferWidth, position2, pipeWidth, pipeHeight);
        }

        public void Move(int gameState) 
        {
            if (gameState == GameController.PlayingState || gameState == GameController.NoPointState)
            {
                bottomPipeRectangle.X -= 5;
                topPipeRectangle.X -= 5;
            }
            if (gameState == GameController.LeaveDestroyerState || gameState == GameController.DestroyingState || gameState == GameController.GettingGiftState || gameState == GameController.TransitionState)
            {
                bottomPipeRectangle.X -= 10;
                topPipeRectangle.X -= 10;
            }
        }

        public void Destroy() 
        {
            if (bottomPipeRectangle.Top < graphics.PreferredBackBufferHeight)
            {
                bottomPipeRectangle.X += 10;
                bottomPipeRectangle.Y += 10;
            }

            if (topPipeRectangle.Bottom > 0)
            {
                topPipeRectangle.X += 10;
                topPipeRectangle.Y -= 10;
            }
            destroyed |= (bottomPipeRectangle.Top >= graphics.PreferredBackBufferHeight || topPipeRectangle.Bottom <= 0);
        }

        public int DefinePipePosition() => random.Next(-570, -350);

        public Rectangle TopPipeRectangle
        {
            get
            {
                return topPipeRectangle;
            }
            set
            {
                topPipeRectangle = value;
            }
        }

        public Rectangle BottomPipeRectangle
        {
            get
            {
                return bottomPipeRectangle;
            }
            set
            {
                bottomPipeRectangle = value;
            }
        }

        public int State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        public bool Destroyed
        {
            get
            {
                return destroyed;
            }
            set
            {
                destroyed = value;
            }
        }
    }
}
//конструктор:
//Определение высоты и ширины трубы Определение базовой вертикальной дистанции (250). Если состояние игры – gettinggift или transition, то меняем позицию трубы на -460 и сокращаем горизонтальную дистанцию (они как будто уменьшаются) 
//Если состояние игры – playing или NoPoint, то вызываем метод 
//definePosition, горизонтальная дистанция - 50
//Если состояние игры – LeaveDestroyer или Destroying, то также вызываем 
//definePosition, но горизонтальная дистанция – 25
//После этой проверки position2 (позиция верхней трубы) = позиция нижней
//плюс высоты этой трубы плюс вертикальная дистанция.
//Дальше идёт создание прямоугольников по координатам (пригодится для рисовки) 
//Остальное всё - свойства для получения или возврата каких-то значений.