using System;
using GR_Projekt.Content.Images;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.Core.Controls
{
    public class MenuBackground : Component
    {
        private Texture2D _backgroundTexture;
        private Rectangle _backgroundRectangle;

        public MenuBackground(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            this._backgroundTexture = contentManager.Load<Texture2D>(GeneralImages.backgroundImage);
            this._backgroundRectangle = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backgroundTexture, _backgroundRectangle, Colors.defaultDrawColor);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
