using System;
using GR_Projekt.Core;
using GR_Projekt.Core.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.States.Settings.Components
{
    public class SoundVolumePicker : Component
    {
        private PlusMinusPicker _plusMinusPicker;
        private int _soundsVolume;

        public SoundVolumePicker(ContentManager contentManager, Vector2 position)
        {
            _soundsVolume = (int)Math.Round(SoundEffect.MasterVolume * 10);
            _plusMinusPicker = new PlusMinusPicker(contentManager: contentManager, position: position, label: "Sound effects volume", valueToShow: _soundsVolume.ToString(), onPlusClick: onPlusClick, onMinusClick: onMinusClick);
        }

        private void onMinusClick(object sender, EventArgs e)
        {
            if (SoundEffect.MasterVolume - 0.1f > 0.0f)
            {
                SoundEffect.MasterVolume -= 0.1f;
            }
            else
            {
                SoundEffect.MasterVolume = 0.0f;
            }
        }

        private void onPlusClick(object sender, EventArgs e)
        {
            if (SoundEffect.MasterVolume + 0.1f < 1.0f)
            {
                SoundEffect.MasterVolume += 0.1f;

            }
            else
            {
                SoundEffect.MasterVolume = 1.0f;
            }
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _plusMinusPicker.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            _soundsVolume = (int)Math.Round(SoundEffect.MasterVolume * 10);
            _plusMinusPicker.Update(gameTime);
            _plusMinusPicker.UpdateValue(gameTime, _soundsVolume.ToString());
        }

        public float getSoundsVolume => SoundEffect.MasterVolume;
    }
}
