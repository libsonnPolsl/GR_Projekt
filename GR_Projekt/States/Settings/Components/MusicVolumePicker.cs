using System;
using GR_Projekt.Core;
using GR_Projekt.Core.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GR_Projekt.States.Settings.Components
{
    public class MusicVolumePicker : Component
    {
        private PlusMinusPicker _plusMinusPicker;
        private int _musicVolume;

        public MusicVolumePicker(ContentManager contentManager, Vector2 position)
        {
            _musicVolume = (int)Math.Round(MediaPlayer.Volume * 10);
            _plusMinusPicker = new PlusMinusPicker(contentManager: contentManager, position: position, label: "Music volume", valueToShow: _musicVolume.ToString(), onPlusClick: onPlusClick, onMinusClick: onMinusClick);
        }

        private void onMinusClick(object sender, EventArgs e)
        {
            if (MediaPlayer.Volume - 0.1f > 0.0f)
            {
                MediaPlayer.Volume -= 0.1f;
            }
            else
            {
                MediaPlayer.Volume = 0.0f;
            }
        }

        private void onPlusClick(object sender, EventArgs e)
        {
            if (MediaPlayer.Volume + 0.1f < 1.0f)
            {
                MediaPlayer.Volume += 0.1f;

            }
            else
            {
                MediaPlayer.Volume = 1.0f;
            }
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _plusMinusPicker.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            _musicVolume = (int)Math.Round(MediaPlayer.Volume * 10);
            _plusMinusPicker.Update(gameTime);
            _plusMinusPicker.UpdateValue(gameTime, _musicVolume.ToString());
        }

        public float getMusicVolume => MediaPlayer.Volume;
    }
}
