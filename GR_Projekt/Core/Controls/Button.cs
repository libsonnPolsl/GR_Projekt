using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GR_Projekt.Core.Controls
{
    public class Button : Component
    {
        private MouseState _mouseState;
        private SpriteFont _font;
        private MouseState _prevoiusMouseState;
        private Texture2D _buttonTexture;
        private string _buttonText;
        private Color _textColor;
        private bool _isOnButton;
        private event EventHandler _onClick;
        private Color _buttonColor;

        public Vector2 _position;
        public Rectangle _buttonRectangle;

        public Button(Texture2D texture, SpriteFont font, string buttonText, Vector2 position, Rectangle buttonRectangle, EventHandler onClick){
            this._buttonTexture = texture;
            this._font = font;
            this._position = position;
            this._buttonRectangle = buttonRectangle;
            this._buttonText = buttonText;
            this._onClick = onClick;

            _buttonColor = Colors.defaultButtonColor;
            _textColor = Colors.textButtonColor;
            _isOnButton = false;
         }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_isOnButton)
            {
                _buttonColor = Colors.onButtonColor;
            }
            else {
                _buttonColor = Colors.defaultButtonColor;
            }

            spriteBatch.Draw(texture: _buttonTexture,destinationRectangle: _buttonRectangle, color: _buttonColor);

            float _textX = _buttonRectangle.X + (_buttonRectangle.Width / 2) - (_font.MeasureString(_buttonText).X / 2);
            float _textY = _buttonRectangle.Y + (_buttonRectangle.Height / 2) - (_font.MeasureString(_buttonText).Y / 2);

            Vector2 _textVector = new Vector2(x: _textX, y: _textY);

            spriteBatch.DrawString(spriteFont: _font,text: _buttonText, position: _textVector,color: _textColor);
        }

        public override void Update(GameTime gameTime)
        {
            _prevoiusMouseState = _mouseState;
            _mouseState = Mouse.GetState();
            _isOnButton = false;

            Rectangle _mouseRectangle = new Rectangle(x: _mouseState.X, y: _mouseState.Y, width: 1, height: 1);

            if (_mouseRectangle.Intersects(_buttonRectangle)) {
                _isOnButton = true;

                if (_mouseState.LeftButton == ButtonState.Released && _prevoiusMouseState.LeftButton == ButtonState.Pressed) {
                    _onClick?.Invoke(this, new EventArgs());
                }
            }

        }
    }
}
