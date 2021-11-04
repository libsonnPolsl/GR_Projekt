using System;
using GR_Projekt.Content.Images;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GR_Projekt.Core.Controls
{
    public class PlusMinusButton : Component
    {
        private Texture2D _buttonTexture;
        private Rectangle _buttonRectangle;
        private event EventHandler _onClick;
        private Color _buttonColor;
        private MouseState _previousMouseState;
        private MouseState _mouseState;
        private bool _isOnButton;

        public PlusMinusButton(ContentManager contentManager, string buttonTexture, Rectangle buttonRectangle, EventHandler onClick)
        {
            this._buttonTexture = contentManager.Load<Texture2D>(buttonTexture);
            this._buttonRectangle = buttonRectangle;
            this._onClick = onClick;
            this._isOnButton = false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            if (_isOnButton)
            {
                _buttonColor = Colors.onButtonColor;
            }
            else
            {
                _buttonColor = Colors.defaultButtonColor;
            }

            spriteBatch.Draw(_buttonTexture, _buttonRectangle, _buttonColor);
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouseState = _mouseState;
            _mouseState = Mouse.GetState();
            _isOnButton = false;

            Rectangle _mouseRectangle = new Rectangle(x: _mouseState.X, y: _mouseState.Y, width: 1, height: 1);

            if (_mouseRectangle.Intersects(_buttonRectangle))
            {
                _isOnButton = true;

                if (_mouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed)
                {
                    _onClick?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
