using System;
using System.Collections.Generic;
using System.Diagnostics;
using GR_Projekt.Core;
using GR_Projekt.Core.Controls;
using GR_Projekt.States.Settings.Components;
using GR_Projekt.States.Settings.Entities;
using GR_Projekt.States.Settings.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GR_Projekt.States.Settings
{
    public class SettingsState : State
    {
        private SettingsModel _modifiedSettings;

        private List<Component> _components;

        Vector2 _screenCenter;
        Vector2 _componentsStartPosition;
        Vector2 _musicVolumePickerPosition;
        Vector2 _soundVolumePickerPosition;
        Vector2 _resolutionPickerPosition;
        Vector2 _fullScreenCheckboxPosition;
        Vector2 _defaultSettingsButtonPosition;
        Vector2 _backButtonPosition;

        MenuBackground _menuBackground;
        MusicVolumePicker _musicVolumePicker;
        SoundVolumePicker _soundVolumePicker;
        ResolutionPicker _resolutionPicker;
        FullScreenCheckbox _fullScreenCheckbox;
        Button _defaultSettingsButton;
        Button _backButton;


        public SettingsState(ContentManager contentManager, GraphicsDevice graphicsDevice, Game1 game, SettingsModel settingsModel) : base(contentManager, graphicsDevice, game, settingsModel, StateTypeEnumeration.Settings)
        {
            setComponentsPositions();
            addComponents();

        }

        public override void repositionComponents()
        {
            bool fullScreen = _fullScreenCheckbox.getFullScreen;
            Dictionary<string, int> _resolution = ResolutionEnumerationParser.toMap(_resolutionPicker.getResolution);
            float musicVolume = _musicVolumePicker.getMusicVolume;
            float soundsVolume = _soundVolumePicker.getSoundsVolume;


            _modifiedSettings = new SettingsModel
            {
                musicVolume = musicVolume,
                soundsVolume = soundsVolume,
                fullscreen = fullScreen,
                width = _resolution["width"],
                height = _resolution["height"],
            };

            _settingsModel = _modifiedSettings;

            setComponentsPositions();
            addComponents();


        }

        private void onBackButtonPressed(object sender, EventArgs e)
        {
            bool fullScreen = _fullScreenCheckbox.getFullScreen;
            Dictionary<string, int> _resolution = ResolutionEnumerationParser.toMap(_resolutionPicker.getResolution);
            float musicVolume = _musicVolumePicker.getMusicVolume;
            float soundsVolume = _soundVolumePicker.getSoundsVolume;


            _modifiedSettings = new SettingsModel
            {
                musicVolume = musicVolume,
                soundsVolume = soundsVolume,
                fullscreen = fullScreen,
                width = _resolution["width"],
                height = _resolution["height"],
            };


            _modifiedSettings.toMemory();

            _game.ChangeState(new MenuState(_contentManager, _graphicsDevice, _game, _settingsModel));
        }

        private void onDefaultSettingsButtonPressed(object sender, EventArgs e)
        {
            _modifiedSettings = DefaultGameSetting.getDefaultSettings;

            _modifiedSettings.toMemory();

            SettingsHandler.setSettings(_modifiedSettings, _game.getGraphicsDeviceManager);

            _settingsModel = _modifiedSettings;

            setComponentsPositions();
            addComponents();

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


            _defaultSettingsButtonPosition = new Vector2(_fullScreenCheckboxPosition.X, _fullScreenCheckboxPosition.Y + Dimens.checkboxHeight + Paddings.componentVerticalPadding * 2);
            _backButtonPosition = new Vector2(_defaultSettingsButtonPosition.X, _defaultSettingsButtonPosition.Y + Dimens.checkboxHeight + Paddings.componentVerticalPadding);
        }

        private void addComponents()
        {
            _components = new List<Component>();


            _menuBackground = new MenuBackground(contentManager: _contentManager, graphicsDevice: _graphicsDevice);
            _musicVolumePicker = new MusicVolumePicker(_contentManager, _musicVolumePickerPosition);
            _soundVolumePicker = new SoundVolumePicker(_contentManager, _soundVolumePickerPosition);
            _resolutionPicker = new ResolutionPicker(_contentManager, _game.getGraphicsDeviceManager, _resolutionPickerPosition, repositionComponents, _settingsModel);
            _fullScreenCheckbox = new FullScreenCheckbox(_contentManager, _game.getGraphicsDeviceManager, _fullScreenCheckboxPosition, repositionComponents);

            _defaultSettingsButton = new Button(_contentManager, "Ustawienia domyslne", _defaultSettingsButtonPosition, onDefaultSettingsButtonPressed);
            _backButton = new Button(_contentManager, "Zapisz i wroc do menu", _backButtonPosition, onBackButtonPressed);


            _components.Add(_menuBackground);

            _components.Add(_musicVolumePicker);
            _components.Add(_soundVolumePicker);
            _components.Add(_resolutionPicker);
            _components.Add(_fullScreenCheckbox);
            _components.Add(_defaultSettingsButton);

            _components.Add(_backButton);

        }
    }
}
