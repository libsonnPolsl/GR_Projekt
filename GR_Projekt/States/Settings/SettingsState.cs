using System;
using System.Collections.Generic;
using System.Diagnostics;
using GR_Projekt.Core;
using GR_Projekt.Core.Controls;
using GR_Projekt.States.Settings.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GR_Projekt.States.Settings
{
    public class SettingsState : State
    {
        private List<Component> _components;

        Vector2 _screenCenter;
        Vector2 _componentsStartPosition;
        Vector2 _musicVolumePickerPosition;
        Vector2 _soundVolumePickerPosition;
        Vector2 _resolutionPickerPosition;
        Vector2 _fullScreenCheckboxPosition;
        Vector2 _backButtonPosition;

        MenuBackground _menuBackground;
        MusicVolumePicker _musicVolumePicker;
        SoundVolumePicker _soundVolumePicker;
        ResolutionPicker _resolutionPicker;
        FullScreenCheckbox _fullScreenCheckbox;
        Button _backButton;


        public SettingsState(ContentManager contentManager, GraphicsDevice graphicsDevice, Game1 game) : base(contentManager, graphicsDevice, game, StateTypeEnumeration.Settings)
        {
            //Components positions
            setComponentsPositions();

            //Components declarations
            addComponents();

        }

        private void onGraphicsChanged(object sender, EventArgs e)
        {
            setComponentsPositions();
            addComponents();
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


        private void setComponentsPositions()
        {
            _screenCenter = new Vector2(_graphicsDevice.Viewport.Width / 2, _graphicsDevice.Viewport.Height / 2);
            _componentsStartPosition = new Vector2(_screenCenter.X - Dimens.buttonWidth / 2, Paddings.componentVerticalPadding);
            _musicVolumePickerPosition = _componentsStartPosition;
            _soundVolumePickerPosition = new Vector2(_musicVolumePickerPosition.X, _musicVolumePickerPosition.Y + Dimens.plusMinusButtonHeight + Paddings.componentVerticalPadding);
            _resolutionPickerPosition = new Vector2(_soundVolumePickerPosition.X, _soundVolumePickerPosition.Y + Dimens.plusMinusButtonHeight + Paddings.componentVerticalPadding);
            _fullScreenCheckboxPosition = new Vector2(_resolutionPickerPosition.X, _resolutionPickerPosition.Y + Dimens.plusMinusButtonHeight + Paddings.componentVerticalPadding);

            _backButtonPosition = new Vector2(_fullScreenCheckboxPosition.X, _fullScreenCheckboxPosition.Y + Dimens.checkboxHeight + Paddings.componentVerticalPadding * 2);
        }

        private void addComponents()
        {
            _components = new List<Component>();


            _menuBackground = new MenuBackground(contentManager: _contentManager, graphicsDevice: _graphicsDevice);
            _musicVolumePicker = new MusicVolumePicker(_contentManager, _musicVolumePickerPosition);
            _soundVolumePicker = new SoundVolumePicker(_contentManager, _soundVolumePickerPosition);
            _resolutionPicker = new ResolutionPicker(_contentManager, _game.getGraphicsDeviceManager, _resolutionPickerPosition, onGraphicsChanged);
            _fullScreenCheckbox = new FullScreenCheckbox(_contentManager, _game.getGraphicsDeviceManager, _fullScreenCheckboxPosition, onGraphicsChanged);
            _backButton = new Button(_contentManager, "Back", _backButtonPosition, onBackButtonPressed);


            _components.Add(_menuBackground);

            _components.Add(_musicVolumePicker);
            _components.Add(_soundVolumePicker);
            _components.Add(_resolutionPicker);
            _components.Add(_fullScreenCheckbox);

            _components.Add(_backButton);

        }
    }
}
