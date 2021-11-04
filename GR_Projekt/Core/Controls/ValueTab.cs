using System;
using System.Diagnostics;
using GR_Projekt.Content.Fonts;
using GR_Projekt.Content.Images;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.Core.Controls
{
    public class ValueTab : Component
    {
        private string _dataToShow;
        private SpriteFont _spriteFont;
        private Texture2D _tabTexture;
        private Rectangle _valueTabRectangle;

        public ValueTab(ContentManager contentManager, string dataToShow, Rectangle valueTabRectangle)
        {
            this._spriteFont = contentManager.Load<SpriteFont>(Fonts.Copperplate(fontSize: 18, fontStyle: FontStyleEnumeration.Bold));
            this._tabTexture = contentManager.Load<Texture2D>(ControlsImages.valueTab);
            this._valueTabRectangle = valueTabRectangle;
            this._dataToShow = dataToShow;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_tabTexture, _valueTabRectangle, Colors.defaultDrawColor);
            float _textX = _valueTabRectangle.X + (_valueTabRectangle.Width / 2) - (_spriteFont.MeasureString(_dataToShow).X / 2);
            float _textY = _valueTabRectangle.Y + (_valueTabRectangle.Height / 2) - (_spriteFont.MeasureString(_dataToShow).Y / 2);

            Vector2 _textVector = new Vector2(x: _textX, y: _textY);

            spriteBatch.DrawString(_spriteFont, _dataToShow, _textVector, Colors.textButtonColor);

        }

        public override void Update(GameTime gameTime)
        {
        }

        public void UpdateValue(GameTime gameTime, string value)
        {
            this._dataToShow = value;
        }
    }
}
