using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // The ball's information
        private Vector2 _ballPosition;
        private Vector2 _ballVelocity;
        private Texture2D _ballTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.Title = "My Cooler Game Title";
            _graphics.PreferredBackBufferWidth = 760;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.ApplyChanges();

            // Position the ball in the center of the screen
            _ballPosition.X = GraphicsDevice.Viewport.Width / 2;
            _ballPosition.Y = GraphicsDevice.Viewport.Height / 2;

            // Give the ball a random velocity
            System.Random rand = new System.Random();
            _ballVelocity.X = (float)rand.NextDouble();
            _ballVelocity.Y = (float)rand.NextDouble();
            _ballVelocity.Normalize();
            _ballVelocity *= 150;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _ballTexture = Content.Load<Texture2D>("Graphics/ball");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _ballPosition += _ballVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Moving offscreen horizontally
            if (_ballPosition.X < GraphicsDevice.Viewport.X || _ballPosition.X > GraphicsDevice.Viewport.Width - 64)
            {
                _ballVelocity.X *= -1;
            }

            // Moving offscreen vertically
            if (_ballPosition.Y < GraphicsDevice.Viewport.Y || _ballPosition.Y > GraphicsDevice.Viewport.Height - 64)
            {
                _ballVelocity.Y *= -1;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_ballTexture, _ballPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
