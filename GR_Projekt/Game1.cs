﻿using System.Collections.Generic;
using GR_Projekt.Content.Sounds;
using GR_Projekt.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GR_Projekt
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardState _previousKeyboardState;
        private KeyboardState _currentKeyboardState;
        private Song song;
        private GameTime gameTime;

        List<State> _currentStates = new List<State>();
        private State _nextGameState;

        public void ChangeState(State newState)
        {
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

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentStates.Add(new MenuState(game: this, graphicsDevice: _graphics.GraphicsDevice, contentManager: Content));

            song = Content.Load<Song>(Songs.menuSong);
            MediaPlayer.Play(song);
        }

        protected override void Update(GameTime gameTime)
        {
            _currentKeyboardState = Keyboard.GetState();

            if (_nextGameState != null)
            {

                _currentStates = StateHandler.handleNewState(_currentStates, _nextGameState);
                _nextGameState = null;
            }

            _currentStates[_currentStates.Count - 1].Update(gameTime: gameTime, _previousKeyboardState, _currentKeyboardState);

            _previousKeyboardState = _currentKeyboardState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.gameTime = gameTime;

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _currentStates[_currentStates.Count - 1].Draw(gameTime: gameTime, spriteBatch: _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void RedrawState()
        {
            this.Draw(this.gameTime);
        }

        public void quitGame()
        {
            _graphics = null;
            _spriteBatch = null;
            _currentStates.Clear();
            _currentStates = null;

            this.Exit();
        }

        public GraphicsDeviceManager getGraphicsDeviceManager => _graphics;
    }
}
