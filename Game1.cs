using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FlappyBird.Classes;
using static System.Formats.Asn1.AsnWriter;
using System;
// Класс по умолчанию, здесь идёт инициализация объектов, загрузка спрайтов – фонов, изменение игры (как она себя ведет,
// если игрок что-то делает) и сама рисовка спрайтов и фонов.Важное действие здесь switch (как if, только с case). Там считываются состояния из GameController (потом)
namespace FlappyBird
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont scoreFont;
        private SpriteFont textFont;
        private GameController gameController;
        private MouseState mouseState;
        private Items items;
        private int gameStateTmp;
        private Scene scene;
        private Bird bird;
        private int indexFrame;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //graphics.PreferredBackBufferWidth = 1080;
            //graphics.PreferredBackBufferHeight = 650;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            GameController.graphics = graphics;
            Booster.graphics = graphics;
            Scene.graphics = graphics;
            Items.graphics = graphics;
            Enemy.graphics = graphics;
            Bird.graphics = graphics;
            Pipe.graphics = graphics;
            gameController = new GameController();
            scene = new Scene();
            items = new Items();
            bird = new Bird();
            indexFrame = 0;
            mouseState = Mouse.GetState();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scene.BackgroundTexture = Content.Load<Texture2D>("background");
            scene.FloorTexture = Content.Load<Texture2D>("floor");
            bird.Texture2D[0] = Content.Load<Texture2D>("bird1");
            bird.Texture2D[1] = Content.Load<Texture2D>("bird2");
            bird.Texture2D[2] = Content.Load<Texture2D>("bird3");
            Pipe.topPipeTexture = Content.Load<Texture2D>("toppipe");
            Pipe.bottomPipeTexture = Content.Load<Texture2D>("bottompipe");
            Enemy.texture2D = Content.Load<Texture2D>("enemy");
            GiftBooster.texture2D = Content.Load<Texture2D>("gift");
            NoPointsBooster.texture2D = Content.Load<Texture2D>("nopoints");
            FireBall.texture2D = Content.Load<Texture2D>("fireballTexture");
            Items.clickTexture = Content.Load<Texture2D>("click");
            Items.instructionsTexture = Content.Load<Texture2D>("instructions");
            Items.pauseTexture = Content.Load<Texture2D>("pause");
            Items.loseTexture = Content.Load<Texture2D>("lose");
            BigBirdBooster.texture2D = Content.Load<Texture2D>("bigbird");

            Bird.wingSound = Content.Load<SoundEffect>("wing");
            GameController.hitSound = Content.Load<SoundEffect>("hit");
            GameController.dieSound = Content.Load<SoundEffect>("die");
            GameController.pointSound = Content.Load<SoundEffect>("point");
            GameController.bulletSound = Content.Load<SoundEffect>("bullet");
            GameController.pauseSound = Content.Load<SoundEffect>("pauseSound");
            GameController.fireballSound = Content.Load<SoundEffect>("fireball");
            GameController.hitbulletSound = Content.Load<SoundEffect>("hitbullet");
            GameController.hoohooSound = Content.Load<SoundEffect>("hoohoo");
            GameController.boosterSound = Content.Load<SoundEffect>("booster");
            GameController.breakSound = Content.Load<SoundEffect>("break");

            scoreFont = Content.Load<SpriteFont>("score");
            textFont = Content.Load<SpriteFont>("text");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            switch (gameController.GameState)
            {
                case GameController.InitState:
                    gameController.ArrayBoosters.Clear();
                    gameController.ArrayPipes.Clear();
                    bird.SetInitPosition();
                    gameController.Score = 0;
                    gameController.ArrayBoosters.Clear();
                    gameController.ArrayEnemies.Clear();
                    bird.ArrayFireBalls.Clear();

                    scene.Move(gameController.GameState);

                    indexFrame = gameController.GetWingsBirdFrame(gameTime, bird);

                    if (mouseState.LeftButton == ButtonState.Released 
                        && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        gameController.GenerateInitBoosters();
                        gameController.GameState = GameController.SelectBoosterState;
                    }
                    mouseState = Mouse.GetState();
                    break;
                case GameController.PlayingState:
                    bird.SetNormalSize();
                    scene.Move(gameController.GameState);
                    gameController.AddPipes();
                    gameController.MovePipes();
                    gameController.EnemyAppear();
                    gameController.MoveEnemies();
                    gameController.BirdShoot(bird);
                    gameController.VerifyFireBallEnemyImpact(bird.ArrayFireBalls);
                    gameController.MoveFireBalls(bird.ArrayFireBalls);
                    indexFrame = gameController.GetWingsBirdFrame(gameTime, bird);
                    gameController.RaiseBirdOnClick(bird);
                    gameController.LoseForImpactPipe(bird);
                    gameController.LoseForImpactFloor(bird);
                    gameController.LoseForImpactEnemy(bird);
                    gameController.IncreaseScore(bird);
                    if (gameController.IsTimeToBigBird())
                        gameController.ArrayBoosters.Add(new BigBirdBooster());
                    foreach (Booster booster in gameController.ArrayBoosters.ToArray())
                    {
                        booster.Move();
                        if (booster.Rectangle.Right <= 0)
                        {
                            gameController.ArrayBoosters.Remove(booster);
                            gameController.NumberOfBigBirdBoosters = 0;
                        }
                    }
                    foreach (Booster booster in gameController.ArrayBoosters.ToArray())
                    {
                        if (bird.Rectangle.Intersects(booster.Rectangle))
                        {
                            gameController.ArrayBoosters.Remove(booster);
                            GameController.boosterSound.Play();
                            gameController.GameState = GameController.DestroyingState;
                        }
                    }
                    if (mouseState.RightButton == ButtonState.Released && Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        gameStateTmp = gameController.GameState;
                        gameController.GameState = GameController.PauseState;
                        GameController.pauseSound.Play();
                    }
                    mouseState = Mouse.GetState();
                    break;
                case GameController.LoseState:
                    gameController.MoveEnemies();
                    gameController.GetDownBirdAfterLose(bird);
                    if (mouseState.LeftButton == ButtonState.Released 
                        && Mouse.GetState().LeftButton == ButtonState.Pressed)
                        gameController.GameState = GameController.InitState;
                    mouseState = Mouse.GetState();
                    gameController.SetBestScore();
                    break;
                case GameController.GettingGiftState:
                    gameController.MoveBoosters();
                    bird.SetInitPosition();
                    scene.Move(gameController.GameState);
                    gameController.AddPipes();
                    gameController.MovePipes();
                    indexFrame = gameController.GetWingsBirdFrame(gameTime, bird);
                    gameController.IncreaseScore(bird);
                    if (gameController.Score == 8)
                        gameController.GameState = GameController.TransitionState;
                    if (mouseState.RightButton == ButtonState.Released && Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        gameStateTmp = gameController.GameState;
                        gameController.GameState = GameController.PauseState;
                        GameController.pauseSound.Play();
                    }
                    mouseState = Mouse.GetState();
                    break;
                case GameController.TransitionState:
                    scene.Move(gameController.GameState);
                    gameController.MovePipes();
                    indexFrame = gameController.GetWingsBirdFrame(gameTime, bird);
                    gameController.IncreaseScore(bird);
                    if (gameController.ArrayPipes.ToArray().Length == 0)
                        gameController.GameState = GameController.PlayingState;
                    if (mouseState.RightButton == ButtonState.Released 
                        && Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        gameStateTmp = gameController.GameState;
                        gameController.GameState = GameController.PauseState;
                        GameController.pauseSound.Play();
                    }
                    mouseState = Mouse.GetState();
                    break;
                case GameController.SelectBoosterState:
                    scene.Move(gameController.GameState);
                    indexFrame = gameController.GetWingsBirdFrame(gameTime, bird);
                    gameController.RaiseBirdOnClick(bird);
                    gameController.MoveBoosters();
                    gameController.LoseForImpactFloor(bird);
                    foreach (Booster booster in gameController.ArrayBoosters.ToArray())
                    {
                        if (bird.Rectangle.Intersects(booster.Rectangle))
                        {
                            gameController.ArrayBoosters.Remove(booster);
                            GameController.boosterSound.Play();
                            if (booster.Type == Booster.GiftBooster)
                                gameController.GameState = GameController.GettingGiftState;
                            else if (booster.Type == Booster.NoPointsBooster)
                                gameController.GameState = GameController.NoPointState;
                            break;
                        }
                    }
                    if (gameController.ArrayBoosters.ToArray().Length == 0)
                        gameController.GameState = GameController.PlayingState;
                    if (mouseState.RightButton == ButtonState.Released 
                        && Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        gameStateTmp = gameController.GameState;
                        gameController.GameState = GameController.PauseState;
                        GameController.pauseSound.Play();
                    }
                    mouseState = Mouse.GetState();
                    break;
                case GameController.PauseState:
                    if (mouseState.LeftButton == ButtonState.Released 
                        && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        gameController.GameState = gameStateTmp;
                        GameController.pauseSound.Play();
                    }
                    mouseState = Mouse.GetState();
                    break;
                case GameController.NoPointState:
                    gameController.MoveBoosters();
                    scene.Move(gameController.GameState);
                    gameController.AddPipes();
                    gameController.MovePipes();
                    gameController.EnemyAppear();
                    gameController.MoveEnemies();
                    gameController.BirdShoot(bird);
                    gameController.VerifyFireBallEnemyImpact(bird.ArrayFireBalls);
                    gameController.MoveFireBalls(bird.ArrayFireBalls);
                    indexFrame = gameController.GetWingsBirdFrame(gameTime, bird);
                    gameController.RaiseBirdOnClick(bird);
                    gameController.LoseForImpactPipe(bird);
                    gameController.LoseForImpactFloor(bird);
                    gameController.LoseForImpactEnemy(bird);
                    if (gameController.GetNumberOfPipesCrossed(bird) >= NoPointsBooster.penalty)
                    {
                        gameController.PipesCrossed = 0;
                        gameController.GameState = GameController.PlayingState;
                        NoPointsBooster.DefinePenalty();
                    }
                    if (mouseState.RightButton == ButtonState.Released 
                        && Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        gameStateTmp = gameController.GameState;
                        gameController.GameState = GameController.PauseState;
                        GameController.pauseSound.Play();
                    }
                    mouseState = Mouse.GetState();
                    break;
                case GameController.DestroyingState:
                    bird.SetInitPosition();
                    bird.SetBigSize();
                    scene.Move(gameController.GameState);
                    gameController.AddPipes();
                    gameController.MovePipes();
                    gameController.DestoyPipe(bird);
                    indexFrame = gameController.GetWingsBirdFrame(gameTime, bird);
                    if (gameController.PipesDestroyed >= 10)
                    {
                        gameController.PipesDestroyed = 0;
                        gameController.GameState = GameController.LeaveDestroyerState;
                    }
                    if (mouseState.RightButton == ButtonState.Released 
                        && Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        gameStateTmp = gameController.GameState;
                        gameController.GameState = GameController.PauseState;
                        GameController.pauseSound.Play();
                    }
                    mouseState = Mouse.GetState();
                    break;
                case GameController.LeaveDestroyerState:
                    bird.SetInitPosition();
                    bird.SetBigSize();
                    scene.Move(gameController.GameState);
                    gameController.MovePipes();
                    gameController.DestoyPipe(bird);
                    indexFrame = gameController.GetWingsBirdFrame(gameTime, bird);
                    if (gameController.ArrayPipes.ToArray().Length <= 0)
                    {
                        gameController.NumberOfBigBirdBoosters = 0;
                        gameController.GameState = GameController.PlayingState;
                    }
                    if (mouseState.RightButton == ButtonState.Released && Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        gameStateTmp = gameController.GameState;
                        gameController.GameState = GameController.PauseState;
                        GameController.pauseSound.Play();
                    }
                    mouseState = Mouse.GetState();
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(scene.BackgroundTexture, scene.BackgroundRectangle, Color.White);
            spriteBatch.Draw(scene.BackgroundTexture, scene.BackgroundRectangle2, Color.White);
            foreach (Pipe pipe in gameController.ArrayPipes)
            {
                spriteBatch.Draw(Pipe.topPipeTexture, pipe.TopPipeRectangle, Color.White);
                spriteBatch.Draw(Pipe.bottomPipeTexture, pipe.BottomPipeRectangle, Color.White);
            }
            spriteBatch.Draw(scene.FloorTexture, scene.FloorRectangle, Color.White);
            spriteBatch.Draw(scene.FloorTexture, scene.FloorRectangle2, Color.White);
            foreach (Booster booster in gameController.ArrayBoosters)
            {
                if (booster.Type == Booster.GiftBooster)
                    spriteBatch.Draw(GiftBooster.texture2D, booster.Rectangle, Color.White);
                else if (booster.Type == Booster.NoPointsBooster)
                    spriteBatch.Draw(NoPointsBooster.texture2D, booster.Rectangle, Color.White);
                else if (booster.Type == Booster.BigBirdBooster)
                    spriteBatch.Draw(BigBirdBooster.texture2D, booster.Rectangle, Color.White);
            }
            foreach (Enemy enemy in gameController.ArrayEnemies)
            {
                spriteBatch.Draw(Enemy.texture2D, enemy.Rectangle, Color.White);
            }
            spriteBatch.Draw(bird.Texture2D[indexFrame], bird.Rectangle, Color.White);
            if (gameController.GameState == GameController.InitState)
            {
                spriteBatch.Draw(Items.clickTexture, items.ClickRectangle, Color.White);
                spriteBatch.Draw(Items.instructionsTexture, items.InstructionsRectangle, Color.White);
            }
            if (gameController.GameState == GameController.PauseState)
            {
                spriteBatch.Draw(Items.pauseTexture, items.PauseRectangle, Color.White);
            }
            if (gameController.GameState == GameController.NoPointState)
            {
                spriteBatch.DrawString(textFont, "Your first " + NoPointsBooster.penalty + " points are worth nothing", new Vector2((graphics.PreferredBackBufferWidth / 2) - (textFont.MeasureString("Your first points are worth nothing").X / 2), 80), Color.White);
            }
            if (gameController.GameState == GameController.GettingGiftState)
            {
                spriteBatch.DrawString(textFont, "Easy points", new Vector2((graphics.PreferredBackBufferWidth / 2) - (textFont.MeasureString("Easy points").X / 2), 80), Color.White);
            }
            foreach (FireBall fireBall in bird.ArrayFireBalls)
            {
                spriteBatch.Draw(FireBall.texture2D, fireBall.Rectangle, Color.White);
            }
            if (gameController.GameState == GameController.LoseState)
            {
                spriteBatch.Draw(Items.loseTexture, items.LoseRectangle, Color.White);
                spriteBatch.DrawString(scoreFont, gameController.Score.ToString(), new Vector2((graphics.PreferredBackBufferWidth / 2) - (scoreFont.MeasureString(gameController.Score.ToString()).X / 2), 260), Color.GhostWhite);
                spriteBatch.DrawString(scoreFont, gameController.BestScore.ToString(), new Vector2((graphics.PreferredBackBufferWidth / 2) - (scoreFont.MeasureString(gameController.BestScore.ToString()).X / 2), 370), Color.GhostWhite);
            }
            spriteBatch.DrawString(scoreFont, gameController.Score.ToString(), new Vector2((graphics.PreferredBackBufferWidth / 2) - (scoreFont.MeasureString(gameController.Score.ToString()).X / 2), 10), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}