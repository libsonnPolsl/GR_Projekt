using System;
using GR_Projekt.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.States.Game.HUD
{
    public class HUDTextCell : HUDCell
    {
        private string _valueName;
        private string _value;
        private SpriteFont _spriteFont;

        public HUDTextCell(SpriteFont spriteFont, string valueName, string value, Rectangle cell) : base(cell: cell)
        {
            this._spriteFont = spriteFont;
            this._valueName = valueName;
            this._value = value;
        }

        public override void Update(string updatedValue)
        {
            this._value = updatedValue;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_spriteFont, _valueName, new Vector2(_cellRectangle.Center.X - _spriteFont.MeasureString(_valueName).X / 2, _cellRectangle.Center.Y - _spriteFont.MeasureString(_valueName).Y), Colors.textButtonColor);
            spriteBatch.DrawString(_spriteFont, _value, new Vector2(_cellRectangle.Center.X - _spriteFont.MeasureString(_value).X / 2, _cellRectangle.Center.Y + _spriteFont.MeasureString(_valueName).Y), Colors.textButtonColor);
        }
    }
}
