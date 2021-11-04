﻿using System;
using System.Collections.Generic;
using GR_Projekt.Core;
using GR_Projekt.Core.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GR_Projekt.States.Settings
{
    public class SettingsState : State
    {
        private List<Component> _components;

        public SettingsState(ContentManager contentManager, GraphicsDevice graphicsDevice, Game1 game) : base(contentManager, graphicsDevice, game, StateTypeEnumeration.Settings)
        {
            _components = new List<Component>();
            MenuBackground _menuBackground = new MenuBackground(contentManager: contentManager, graphicsDevice: graphicsDevice);

            Vector2 _screenCenter = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);

            Vector2 _componentsStartPosition = new Vector2(_screenCenter.X - Dimens.buttonWidth / 2, graphicsDevice.Viewport.Height / 5);

            PlusMinusPicker _plusMinusPicker = new PlusMinusPicker(contentManager, _componentsStartPosition, label: "Music volume");

            Vector2 _cancelButtonPosition = new Vector2(_componentsStartPosition.X, _componentsStartPosition.Y + Dimens.plusMinusButtonHeight + Paddings.componentVerticalPadding);

            Button _backButton = new Button(contentManager, "Back", _cancelButtonPosition, onBackButtonPressed);


            _components.Add(_menuBackground);
            _components.Add(_backButton);
            _components.Add(_plusMinusPicker);
        }

        private void onBackButtonPressed(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_contentManager, _graphicsDevice, _game));
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