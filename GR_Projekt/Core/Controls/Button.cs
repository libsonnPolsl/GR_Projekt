using System;
using System.Diagnostics;
using GR_Projekt.Content.Fonts;
using GR_Projekt.Content.Images;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        public Button(ContentManager contentManager, string buttonText, Vector2 position, EventHandler onClick)
        {
            this._buttonTexture = contentManager.Load<Texture2D>(ControlsImages.wideButtonImage);
            this._font = contentManager.Load<SpriteFont>(Fonts.Copperplate(fontSize: 16));
            this._buttonRectangle = new Rectangle(x: (int)position.X, y: (int)position.Y, width: Dimens.buttonWidth, height: Dimens.buttonHeight);

            this._position = position;
            this._buttonText = buttonText;
            this._onClick = onClick;

            this._buttonColor = Colors.defaultButtonColor;
            this._textColor = Colors.textButtonColor;
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

            spriteBatch.Draw(texture: _buttonTexture, destinationRectangle: _buttonRectangle, color: _buttonColor);

            float _textX = _buttonRectangle.X + (_buttonRectangle.Width / 2) - (_font.MeasureString(_buttonText).X / 2);
            float _textY = _buttonRectangle.Y + (_buttonRectangle.Height / 2) - (_font.MeasureString(_buttonText).Y / 2);

            Vector2 _textVector = new Vector2(x: _textX, y: _textY);

            spriteBatch.DrawString(spriteFont: _font, text: _buttonText, position: _textVector, color: _textColor);
        }

        public override void Update(GameTime gameTime)
        {
            _prevoiusMouseState = _mouseState;
            _mouseState = Mouse.GetState();
            _isOnButton = false;

            Rectangle _mouseRectangle = new Rectangle(x: _mouseState.X, y: _mouseState.Y, width: 1, height: 1);

            if (_mouseRectangle.Intersects(_buttonRectangle))
            {
                _isOnButton = true;

                if (_mouseState.LeftButton == ButtonState.Released && _prevoiusMouseState.LeftButton == ButtonState.Pressed)
                {
                    _onClick?.Invoke(this, new EventArgs());
                }
            }

        }
    }
}
