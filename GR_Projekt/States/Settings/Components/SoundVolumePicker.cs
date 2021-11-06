using System;
using GR_Projekt.Core;
using GR_Projekt.Core.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.States.Settings.Components
{
    public class SoundVolumePicker : Component
    {
        private PlusMinusPicker _plusMinusPicker;

        public SoundVolumePicker(ContentManager contentManager, Vector2 position)
        {
            _plusMinusPicker = new PlusMinusPicker(contentManager: contentManager, position: position, label: "Sound effects volume", valueToShow: "0", onPlusClick: onPlusClick, onMinusClick: onMinusClick);
        }



        private void onMinusClick(object sender, EventArgs e)
        {
            //TODO
        }

        private void onPlusClick(object sender, EventArgs e)
        {
            //TODO
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _plusMinusPicker.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            //TODO
        }
    }
}
