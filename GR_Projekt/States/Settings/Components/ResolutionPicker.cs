using System;
using GR_Projekt.Core;
using GR_Projekt.Core.Controls;
using GR_Projekt.States.Settings.Entities;
using GR_Projekt.States.Settings.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.States.Settings.Components
{
    public class ResolutionPicker : Component
    {
        private int _resolutionIndex;
        private PlusMinusPicker _resolutionPicker;
        private ResolutionEnumeration _resolutionEnumeration;
        private Action _onResolutionChanged;
        private GraphicsDeviceManager _graphicsDeviceManager;

        public ResolutionPicker(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager, Vector2 position, Action onResolutionChanged, SettingsModel settingsModel)
        {
            this._graphicsDeviceManager = graphicsDeviceManager;
            this._resolutionEnumeration = ResolutionEnumerationParser.fromValues(settingsModel.width, settingsModel.height);
            this._resolutionIndex = ResolutionEnumerationParser.indexOfValue(_resolutionEnumeration);
            this._onResolutionChanged = onResolutionChanged;
            this._resolutionPicker = new PlusMinusPicker(contentManager: contentManager, position: position, label: "Resolution", valueToShow: ResolutionEnumerationParser.toString(_resolutionEnumeration), onPlusClick: onPlusClick, onMinusClick: onMinusClick);
        }

        private void onMinusClick(object sender, EventArgs e)

        {
            if (_resolutionIndex > 0)
            {
                _resolutionIndex--;
            }
            else
            {
                _resolutionIndex = Enum.GetValues(typeof(ResolutionEnumeration)).Length - 1;
            }

            _resolutionEnumeration = (ResolutionEnumeration)Enum.GetValues(typeof(ResolutionEnumeration)).GetValue(_resolutionIndex);
            changeResolution();
        }

        private void onPlusClick(object sender, EventArgs e)
        {
            if (_resolutionIndex < Enum.GetValues(typeof(ResolutionEnumeration)).Length - 1)
            {
                _resolutionIndex++;
            }
            else
            {
                _resolutionIndex = 0;
            }

            _resolutionEnumeration = (ResolutionEnumeration)Enum.GetValues(typeof(ResolutionEnumeration)).GetValue(_resolutionIndex);
            changeResolution();
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _resolutionPicker.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            _resolutionPicker.Update(gameTime);
            _resolutionPicker.UpdateValue(gameTime, ResolutionEnumerationParser.toString(_resolutionEnumeration));

        }

        private void changeResolution()
        {
            if (_graphicsDeviceManager != null)
            {

                _graphicsDeviceManager.PreferredBackBufferWidth = ResolutionEnumerationParser.toMap(_resolutionEnumeration)["width"];
                _graphicsDeviceManager.PreferredBackBufferHeight = ResolutionEnumerationParser.toMap(_resolutionEnumeration)["height"];

                _graphicsDeviceManager.ApplyChanges();

                _onResolutionChanged();
            }
        }

        public ResolutionEnumeration getResolution => _resolutionEnumeration;
    }
}
