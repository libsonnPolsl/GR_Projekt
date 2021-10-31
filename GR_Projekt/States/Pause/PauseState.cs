using System;
using System.Collections.Generic;

using GR_Projekt.Content.Fonts;
using GR_Projekt.Content.Images;
using GR_Projekt.Core;
using GR_Projekt.Core.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GR_Projekt.States
{
    public class PauseState : State
    {
        List<Component> _components;

        public PauseState(ContentManager contentManager, GraphicsDevice graphicsDevice, Game1 game) : base(contentManager, graphicsDevice, game, StateTypeEnumeration.Pause) {

            _components = new List<Component>();

            Texture2D _buttonTexture = _contentManager.Load<Texture2D>(ControlsImages.wideButtonImage);
            SpriteFont _textFont = _contentManager.Load<SpriteFont>(Fonts.Arial(fontSize: 12));

            string _mainMenuButtonText = "MAIN MENU";
            Vector2 _buttonPosition = new Vector2(x: 100, y: 200);
            Rectangle _buttonRectangle = new Rectangle(x: (int)_buttonPosition.X, y: (int)_buttonPosition.Y, width: 300, height: 100);

            Button _goToMainMenuButton = new Button(texture: _buttonTexture, font: _textFont, buttonText: _mainMenuButtonText, position: _buttonPosition, buttonRectangle: _buttonRectangle, onClick: onGoToMainMenuClick);

            string _resumeButtonText = "RESUME";
            Vector2 _resumeButtonPosition = new Vector2(x: 100, y: 100);
            Rectangle _resumeButtonRectangle = new Rectangle(x: (int)_resumeButtonPosition.X, y: (int)_resumeButtonPosition.Y, width: 300, height: 100);

            Button _resumeButton = new Button(texture: _buttonTexture, font: _textFont, buttonText: _resumeButtonText, position: _resumeButtonPosition, buttonRectangle: _resumeButtonRectangle, onClick: onResumeGameClick);

            _components.Add(_resumeButton);
            _components.Add(_goToMainMenuButton);

        }

        private void onResumeGameClick(object sender, EventArgs e) {
            _game.ChangeState(newState:new GameState(_contentManager, _graphicsDevice, _game));
        }

        private void onGoToMainMenuClick(object sender, EventArgs e) {
            _game.ChangeState(newState: new MenuState(_contentManager, _graphicsDevice, _game));

        }


        public override void Dispose()
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Component component in _components) {
                component.Draw(gameTime, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime, KeyboardState previousState, KeyboardState currentState)
        {
            foreach (Component component in _components)
            {
                component.Update(gameTime);
            }
        }
    }
}
