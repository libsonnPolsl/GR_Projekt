using System;
using GR_Projekt.Content.Images;
using GR_Projekt.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.States.Game.HUD
{
    public class Crosshair
    {
        private bool _visible;
        private Texture2D _crosshairTexture;
        private Rectangle _crosshairRectangle;

        public Rectangle getCrosshairRectangle => _crosshairRectangle;

        public Crosshair(ContentManager contentManager, Point gameScreenCenter)
        {
            this._crosshairTexture = contentManager.Load<Texture2D>(GameImages.crosshairImage);
            this._crosshairRectangle = new Rectangle((gameScreenCenter.X - Dimens.crosshairSize / 2), (gameScreenCenter.Y - Dimens.crosshairSize / 2), Dimens.crosshairSize, Dimens.crosshairSize);
            this._visible = true;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void ChangeVisibility(bool visible)
        {
            this._visible = visible;
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_visible)
            {
                spriteBatch.Draw(_crosshairTexture, _crosshairRectangle, Colors.defaultDrawColor);
            }

        }
    }
}
