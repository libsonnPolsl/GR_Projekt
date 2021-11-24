using System;
using System.Collections.Generic;
using System.Diagnostics;
using GR_Projekt.Content.Images;
using GR_Projekt.Content.Images.Controls;
using GR_Projekt.Core;
using GR_Projekt.Core.Controls;
using GR_Projekt.States.Settings;
using GR_Projekt.States.Settings.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GR_Projekt.States
{
    public class MenuState : State
    {
        private List<Component> _components;
        private MenuBackground _menuBackground;
        private Button _newGameButton;
        private Button _settingsButton;
        private Button _quitButton;
        private ImageComponent _menuDoomFace;

        Vector2 _screenCenter;
        Vector2 _settingsButtonPosition;
        Vector2 _newGameButtonPosition;
        Vector2 _quitGameButtonPosition;
        Vector2 _menuDoomFaceCenter;



        public MenuState(ContentManager contentManager, GraphicsDevice graphicsDevice, Game1 game, SettingsModel settingsModel) : base(contentManager, graphicsDevice, game, settingsModel, StateTypeEnumeration.MainMenu)
        {
            setComponentsPositions();
            addComponents();
        }

        private void onNewGameButtonClick(object sender, EventArgs e)
        {
            _game.ChangeState(newState: new GameState(_contentManager, _graphicsDevice, _game, _settingsModel));
        }

        private void onSettingsButtonClick(object sender, EventArgs e)
        {
            _game.ChangeState(newState: new SettingsState(_contentManager, _graphicsDevice, _game, _settingsModel));
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

        public override void repositionComponents()
        {
            setComponentsPositions();
            addComponents();
        }

        private void setComponentsPositions()
        {
            _screenCenter = new Vector2(_graphicsDevice.Viewport.Width / 2, _graphicsDevice.Viewport.Height / 2);
            _settingsButtonPosition = new Vector2(_graphicsDevice.Viewport.Width - Dimens.buttonWidth - Paddings.screenHorizontalPadding, _screenCenter.Y - Dimens.buttonHeight / 2);
            _newGameButtonPosition = new Vector2(_settingsButtonPosition.X, _settingsButtonPosition.Y - Dimens.buttonHeight - Paddings.componentVerticalPadding);
            _quitGameButtonPosition = new Vector2(_settingsButtonPosition.X, _settingsButtonPosition.Y + Dimens.buttonHeight + Paddings.componentVerticalPadding);
            _menuDoomFaceCenter = new Vector2(_graphicsDevice.Viewport.Width / 4, _screenCenter.Y);

        }

        private void addComponents()
        {

            _components = new List<Component>();
            _menuBackground = new MenuBackground(contentManager: _contentManager, graphicsDevice: _graphicsDevice);
            _newGameButton = new Button(contentManager: _contentManager, buttonText: "New game", position: _newGameButtonPosition, onClick: onNewGameButtonClick);
            _settingsButton = new Button(contentManager: _contentManager, buttonText: "Settings", position: _settingsButtonPosition, onClick: onSettingsButtonClick);
            _quitButton = new Button(contentManager: _contentManager, buttonText: "Quit", position: _quitGameButtonPosition, onClick: onQuitButtonClick);
            _menuDoomFace = new ImageComponent(contentManager: _contentManager, texturePath: MainMenuImages.menuDoomFace, imageCenter: _menuDoomFaceCenter, _graphicsDevice.Viewport.Width / 6, _graphicsDevice.Viewport.Height / 3);


            _components.Add(_menuBackground);
            _components.Add(_newGameButton);
            _components.Add(_settingsButton);
            _components.Add(_quitButton);
            _components.Add(_menuDoomFace);
        }
    }
}
