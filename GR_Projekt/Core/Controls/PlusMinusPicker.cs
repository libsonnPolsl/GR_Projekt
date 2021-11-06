using System;
using System.Collections.Generic;
using GR_Projekt.Content.Fonts;
using GR_Projekt.Content.Images;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.Core.Controls
{
    public class PlusMinusPicker : Component
    {
        private Vector2 _position;
        private List<Component> _components;
        private ValueTab _valueTab;
        private string _label;
        private SpriteFont _spriteFont;

        public PlusMinusPicker(ContentManager contentManager, Vector2 position, string label, string valueToShow, EventHandler onPlusClick, EventHandler onMinusClick)
        {
            this._label = label;
            this._position = position;

            _spriteFont = contentManager.Load<SpriteFont>(Fonts.Copperplate(fontSize: 18, fontStyle: FontStyleEnumeration.Bold));
            _components = new List<Component>();

            Rectangle _valueTabRectangle = new Rectangle(x: (int)_position.X + Dimens.plusMinusButtonWidth, y: (int)(_position.Y + _spriteFont.MeasureString(label).Y), width: Dimens.valueTabWidth, height: Dimens.valueTabHeight);
            Rectangle _minusButtonRectangle = new Rectangle((int)_position.X - Paddings.componentHorizontalPadding, _valueTabRectangle.Y, Dimens.plusMinusButtonWidth, Dimens.plusMinusButtonHeight);
            Rectangle _plusButtonRectangle = new Rectangle(_valueTabRectangle.X + Paddings.componentHorizontalPadding + Dimens.valueTabWidth, _valueTabRectangle.Y, Dimens.plusMinusButtonWidth, Dimens.plusMinusButtonHeight);

            PlusMinusButton _minusButton = new PlusMinusButton(contentManager: contentManager, buttonTexture: ControlsImages.minusButtonImage, buttonRectangle: _minusButtonRectangle, onClick: onMinusClick);
            _valueTab = new ValueTab(contentManager: contentManager, dataToShow: valueToShow, _valueTabRectangle);
            PlusMinusButton _plusButton = new PlusMinusButton(contentManager: contentManager, buttonTexture: ControlsImages.plusButtonImage, buttonRectangle: _plusButtonRectangle, onClick: onPlusClick);


            _components.Add(_minusButton);
            _components.Add(_valueTab);
            _components.Add(_plusButton);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_spriteFont, _label, _position, Colors.textButtonColor);

            foreach (Component component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {

            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].Update(gameTime);
            }
        }

        public void UpdateValue(GameTime gameTime, string value)
        {
            _valueTab.UpdateValue(gameTime, value);
        }
    }
}
