using System;
using System.Diagnostics;
using GR_Projekt.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.Content.Images.Controls
{
    public class ImageComponent : Component
    {
        private Rectangle _imageRectangle;
        private Texture2D _imageTexture;

        public ImageComponent(ContentManager contentManager, string texturePath, Vector2 imageCenter, double scale)
        {
            this._imageTexture = contentManager.Load<Texture2D>(texturePath);
            this._imageRectangle = new Rectangle((int)(imageCenter.X - (_imageTexture.Width / 4)), (int)(imageCenter.Y - (_imageTexture.Height / 4)), (int)(_imageTexture.Width * scale), (int)(_imageTexture.Height * scale));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_imageTexture, _imageRectangle, Colors.defaultDrawColor);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
