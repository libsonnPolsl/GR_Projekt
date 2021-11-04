using System;
using System.Collections.Generic;
using System.Diagnostics;
using GR_Projekt.Content.Fonts;
using GR_Projekt.Content.Images;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GR_Projekt.Core.Controls
{
    public class PlusMinusPicker : Component
    {
        private Vector2 _position;
        private ValueTab _valueTab;
        private List<PlusMinusButton> _buttons;
        private int _musicVolume;
        private string _label;
        private SpriteFont _spriteFont;


        public PlusMinusPicker(ContentManager contentManager, Vector2 position, string label)
        {
            this._label = label;
            this._position = position;

            _spriteFont = contentManager.Load<SpriteFont>(Fonts.Copperplate(fontSize: 18, fontStyle: FontStyleEnumeration.Bold));

            _musicVolume = (int)Math.Round(MediaPlayer.Volume * 10);
            _buttons = new List<PlusMinusButton>();

            Rectangle _valueTabRectangle = new Rectangle(x: (int)_position.X + Dimens.plusMinusButtonWidth, y: (int)(_position.Y + _spriteFont.MeasureString(label).Y), width: Dimens.valueTabWidth, height: Dimens.valueTabHeight);
            Rectangle _minusButtonRectangle = new Rectangle((int)_position.X - Paddings.componentHorizontalPadding, _valueTabRectangle.Y, Dimens.plusMinusButtonWidth, Dimens.plusMinusButtonHeight);
            Rectangle _plusButtonRectangle = new Rectangle(_valueTabRectangle.X + Paddings.componentHorizontalPadding + Dimens.valueTabWidth, _valueTabRectangle.Y, Dimens.plusMinusButtonWidth, Dimens.plusMinusButtonHeight);

            PlusMinusButton _minusButton = new PlusMinusButton(contentManager: contentManager, buttonTexture: ControlsImages.minusButtonImage, buttonRectangle: _minusButtonRectangle, onClick: onMinusClick);
            _valueTab = new ValueTab(contentManager: contentManager, dataToShow: _musicVolume.ToString(), _valueTabRectangle);
            PlusMinusButton _plusButton = new PlusMinusButton(contentManager: contentManager, buttonTexture: ControlsImages.plusButtonImage, buttonRectangle: _plusButtonRectangle, onClick: onPlusClick);


            _buttons.Add(_minusButton);
            _buttons.Add(_plusButton);

        }

        private void onMinusClick(object sender, EventArgs e)
        {
            MediaPlayer.Volume -= 0.1f;
        }

        private void onPlusClick(object sender, EventArgs e)
        {
            MediaPlayer.Volume += 0.1f;
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_spriteFont, _label, _position, Colors.textButtonColor);

            foreach (Component component in _buttons)
            {
                component.Draw(gameTime, spriteBatch);
            }
            _valueTab.Draw(gameTime, spriteBatch);
        }


        public override void Update(GameTime gameTime)
        {

            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].Update(gameTime);
            }

            _musicVolume = (int)Math.Round(MediaPlayer.Volume * 10);
            _valueTab.UpdateValue(gameTime, _musicVolume.ToString());
        }
    }
}
