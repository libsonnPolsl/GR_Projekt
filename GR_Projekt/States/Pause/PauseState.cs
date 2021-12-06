using System;
using System.Collections.Generic;

using GR_Projekt.Core;
using GR_Projekt.Core.Controls;
using GR_Projekt.States.Settings.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GR_Projekt.States
{
    public class PauseState : State
    {
        private List<Component> _components;

        public PauseState(ContentManager contentManager, GraphicsDevice graphicsDevice, Game1 game, SettingsModel settingsModel) : base(contentManager, graphicsDevice, game, settingsModel, StateTypeEnumeration.Pause)
        {
            _components = new List<Component>();

            MenuBackground _menuBackround = new MenuBackground(contentManager, graphicsDevice);

            Vector2 _screenCenter = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
            Vector2 _resumeButtonPosition = new Vector2(_screenCenter.X - Dimens.buttonWidth / 2, _screenCenter.Y - Dimens.buttonHeight - Paddings.componentVerticalPadding);
            Vector2 _mainMenuButtonPosition = new Vector2(_resumeButtonPosition.X, _screenCenter.Y + Paddings.componentVerticalPadding);

            Button _resumeGameButton = new Button(contentManager: contentManager, buttonText: "Wznow gre", position: _resumeButtonPosition, onClick: onResumeGameClick);
            Button _mainMenuButton = new Button(contentManager: contentManager, buttonText: "Menu glowne", position: _mainMenuButtonPosition, onClick: onGoToMainMenuClick);

            _components.Add(_menuBackround);
            _components.Add(_resumeGameButton);
            _components.Add(_mainMenuButton);

        }

        public override void repositionComponents()
        {
        }

        private void onResumeGameClick(object sender, EventArgs e)
        {

            _game.ChangeState(newState: new GameState(_contentManager, _graphicsDevice, _game, _settingsModel));

        }

        private void onGoToMainMenuClick(object sender, EventArgs e)
        {
            _game.ChangeState(newState: new MenuState(_contentManager, _graphicsDevice, _game, _settingsModel));

        }


        public override void Dispose()
        {
            _components.Clear();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Component component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime, KeyboardState previousState, KeyboardState currentState)
        {
            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].Update(gameTime);
            }
        }
    }
}
