using System;
using System.Diagnostics;
using GR_Projekt.Core;
using GR_Projekt.Core.Controls;
using GR_Projekt.States.Settings.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.States.Settings.Components
{
    public class ResolutionPicker : Component
    {
        private GraphicsDeviceManager _graphicsDeviceManager;
        private PlusMinusPicker _resolutionPicker;
        private ResolutionEnumeration _resolutionEnumeration;
        private event EventHandler _onResolutionChanged;

        public ResolutionPicker(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager, Vector2 position, EventHandler onResolutionChanged)
        {
            _resolutionEnumeration = ResolutionEnumeration.Res800x600;
            this._graphicsDeviceManager = graphicsDeviceManager;
            this._onResolutionChanged = onResolutionChanged;
            this._resolutionPicker = new PlusMinusPicker(contentManager: contentManager, position: position, label: "Resolution", valueToShow: _resolutionEnumeration.ToString(), onPlusClick: onPlusClick, onMinusClick: onMinusClick);

        }

        private void onMinusClick(object sender, EventArgs e)

        {

            if (_graphicsDeviceManager != null)
            {
                _resolutionEnumeration = ResolutionEnumeration.Res800x600;

                _graphicsDeviceManager.PreferredBackBufferWidth = 800;
                _graphicsDeviceManager.PreferredBackBufferHeight = 600;

                _graphicsDeviceManager.ApplyChanges();

                _onResolutionChanged(sender, e);
            }

        }

        private void onPlusClick(object sender, EventArgs e)
        {
            if (_graphicsDeviceManager != null)
            {
                _resolutionEnumeration = ResolutionEnumeration.Res1280x720;

                _graphicsDeviceManager.PreferredBackBufferWidth = 1280;
                _graphicsDeviceManager.PreferredBackBufferHeight = 720;

                _graphicsDeviceManager.ApplyChanges();

                _onResolutionChanged(sender, e);
            }
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _resolutionPicker.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            _resolutionPicker.Update(gameTime);
            _resolutionPicker.UpdateValue(gameTime, _resolutionEnumeration.ToString());
        }
    }
}
