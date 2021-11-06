using System;
using GR_Projekt.Content.Fonts;
using GR_Projekt.Content.Images;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GR_Projekt.Core.Controls
{
    public class Checkbox : Component
    {
        private SpriteFont _spriteFont;
        private Texture2D _checkedTexture;
        private Texture2D _unCheckedTexture;
        private Rectangle _checkboxRectangle;
        private Vector2 _position;
        private event EventHandler _onClick;
        private Color _checkboxColor;
        private MouseState _previousMouseState;
        private MouseState _mouseState;
        private bool _isOnCheckbox;
        private bool _isSelected;
        private string _label;


        public Checkbox(ContentManager contentManager, bool isSelected, string label, Vector2 position, EventHandler onClick)
        {
            _spriteFont = contentManager.Load<SpriteFont>(Fonts.Copperplate(fontSize: 18, fontStyle: FontStyleEnumeration.Bold));

            this._isSelected = isSelected;
            this._label = label;
            this._position = position;
            this._checkboxRectangle = new Rectangle(x: (int)position.X, y: (int)(position.Y + _spriteFont.MeasureString(label).Y), width: Dimens.checkboxWidth, height: Dimens.checkboxHeight);
            this._onClick = onClick;
            this._checkboxColor = Colors.defaultButtonColor;

            _checkedTexture = contentManager.Load<Texture2D>(ControlsImages.checkedBoxImage);
            _unCheckedTexture = contentManager.Load<Texture2D>(ControlsImages.uncheckedBoxImage);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_spriteFont, _label, _position, Colors.textButtonColor);

            if (_isOnCheckbox)
            {
                _checkboxColor = Colors.onButtonColor;
            }
            else
            {
                _checkboxColor = Colors.defaultButtonColor;
            }

            if (_isSelected)
            {
                spriteBatch.Draw(_checkedTexture, _checkboxRectangle, _checkboxColor);
            }
            else
            {
                spriteBatch.Draw(_unCheckedTexture, _checkboxRectangle, _checkboxColor);
            }

        }

        public override void Update(GameTime gameTime)
        {
            _previousMouseState = _mouseState;
            _mouseState = Mouse.GetState();
            _isOnCheckbox = false;

            Rectangle _mouseRectangle = new Rectangle(x: _mouseState.X, y: _mouseState.Y, width: 1, height: 1);

            if (_mouseRectangle.Intersects(_checkboxRectangle))
            {
                _isOnCheckbox = true;

                if (_mouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed)
                {
                    _onClick?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
