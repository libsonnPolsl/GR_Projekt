using System;
using System.Collections.Generic;
using GR_Projekt.Content.Fonts;
using GR_Projekt.Content.Images;
using GR_Projekt.Content.Images.Controls;
using GR_Projekt.Content.Sounds;
using GR_Projekt.Core;
using GR_Projekt.Core.Controls;
using GR_Projekt.States.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GR_Projekt.States
{
    public class MenuState : State
    {
        private List<Component> _components;


        public MenuState(ContentManager contentManager, GraphicsDevice graphicsDevice, Game1 game) : base(contentManager, graphicsDevice, game, StateTypeEnumeration.MainMenu)
        {
            _components = new List<Component>();
            MenuBackground _menuBackground = new MenuBackground(contentManager: contentManager, graphicsDevice: graphicsDevice);

            Vector2 _screenCenter = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
            Vector2 _settingsButtonPosition = new Vector2(graphicsDevice.Viewport.Width - Dimens.buttonWidth - Paddings.screenHorizontalPadding, _screenCenter.Y - Dimens.buttonHeight / 2);
            Vector2 _newGameButtonPosition = new Vector2(_settingsButtonPosition.X, _settingsButtonPosition.Y - Dimens.buttonHeight - Paddings.componentVerticalPadding);
            Vector2 _quitGameButtonPosition = new Vector2(_settingsButtonPosition.X, _settingsButtonPosition.Y + Dimens.buttonHeight + Paddings.componentVerticalPadding);

            Button _newGameButton = new Button(contentManager: contentManager, buttonText: "New game", position: _newGameButtonPosition, onClick: onNewGameButtonClick);
            Button _settingsButton = new Button(contentManager: contentManager, buttonText: "Settings", position: _settingsButtonPosition, onClick: onSettingsButtonClick);
            Button _quitButton = new Button(contentManager: contentManager, buttonText: "Quit", position: _quitGameButtonPosition, onClick: onQuitButtonClick);

            Vector2 _menuDoomFaceCenter = new Vector2(graphicsDevice.Viewport.Width / 4, _screenCenter.Y);

            ImageComponent _menuDoomFace = new ImageComponent(contentManager: contentManager, texturePath: MainMenuImages.menuDoomFace, imageCenter: _menuDoomFaceCenter, scale: 0.5);

            _components.Add(_menuBackground);
            _components.Add(_newGameButton);
            _components.Add(_settingsButton);
            _components.Add(_quitButton);
            _components.Add(_menuDoomFace);
        }

        private void onNewGameButtonClick(object sender, EventArgs e)
        {
            _game.ChangeState(newState: new GameState(_contentManager, _graphicsDevice, _game));
        }

        private void onSettingsButtonClick(object sender, EventArgs e)
        {
            _game.ChangeState(newState: new SettingsState(_contentManager, _graphicsDevice, _game));
        }

        private void onQuitButtonClick(object sender, EventArgs e)
        {
            this.Dispose();
            _game.quitGame();
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Component component in _components)
            {
                component.Draw(gameTime: gameTime, spriteBatch: spriteBatch);
            }
        }

        public override void Dispose()
        {
            _components.Clear();
        }

        public override void Update(GameTime gameTime, KeyboardState previousState, KeyboardState currentState)
        {
            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].Update(gameTime: gameTime);
            }
        }
    }
}
