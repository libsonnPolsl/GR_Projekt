using GR_Projekt.Content.Fonts;
using GR_Projekt.Content.Images;
using GR_Projekt.Core;
using GR_Projekt.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GR_Projekt
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private State _currentGameState;
        private State _nextGameState;

        public void ChangeState(State newState) {
            _nextGameState = newState;
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentGameState = new MenuState(game: this, graphicsDevice: _graphics.GraphicsDevice, contentMenager: Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (_nextGameState != null) {
                _currentGameState = _nextGameState;
                _nextGameState = null;
            }

            _currentGameState.Update(gameTime: gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _currentGameState.Draw(gameTime: gameTime, spriteBatch: _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
