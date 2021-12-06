
using System;
using System.Diagnostics;
using GR_Projekt.Core;
using GR_Projekt.Core.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.States.Settings.Components
{
    public class FullScreenCheckbox : Component
    {
        private bool _isFullScreen;
        private GraphicsDeviceManager _graphicsDeviceManager;
        private Checkbox _checkbox;
        private Action _onGraphicsChanged;

        public FullScreenCheckbox(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager, Vector2 position, Action onGraphicsChanged)
        {
            this._graphicsDeviceManager = graphicsDeviceManager;
            this._isFullScreen = graphicsDeviceManager.IsFullScreen;
            this._onGraphicsChanged = onGraphicsChanged;

            this._checkbox = new Checkbox(contentManager, _isFullScreen, "Pelny ekran", position, onFullScreenPressed);

        }

        private void onFullScreenPressed(object sender, EventArgs e)
        {


            if (_graphicsDeviceManager != null)
            {
                _isFullScreen = !_isFullScreen;

                _graphicsDeviceManager.IsFullScreen = _isFullScreen;

                _graphicsDeviceManager.ApplyChanges();

                _onGraphicsChanged();
            }


        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _checkbox.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            _checkbox.Update(gameTime);
        }

        public bool getFullScreen => _isFullScreen;

        public void updateFullScreen(bool fullScreen) => _isFullScreen = fullScreen;
    }
}
