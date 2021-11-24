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

        public ImageComponent(ContentManager contentManager, string texturePath, Vector2 imageCenter, int width, int height)
        {
            this._imageTexture = contentManager.Load<Texture2D>(texturePath);
            this._imageRectangle = new Rectangle((int)(imageCenter.X - (width / 2)), (int)(imageCenter.Y - (height / 2)), width, height);
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
