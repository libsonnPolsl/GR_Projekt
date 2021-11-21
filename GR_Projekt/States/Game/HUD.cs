using System;
using GR_Projekt.Content.Images;
using GR_Projekt.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.States.Game
{
    public class HUD : Component
    {
        private Rectangle _hudRectangle;
        private Texture2D _hudBackgroundTexture;
        public HUD(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager)
        {
            //TODO change texture
            this._hudBackgroundTexture = contentManager.Load<Texture2D>(GeneralImages.backgroundImage);
            this._hudRectangle = new Rectangle(x: (int)0.0, y: (int)(graphicsDeviceManager.GraphicsDevice.Viewport.Height * 0.8), width: graphicsDeviceManager.GraphicsDevice.Viewport.Width, height: (int)(graphicsDeviceManager.GraphicsDevice.Viewport.Height * 0.2));

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_hudBackgroundTexture, _hudRectangle, Colors.defaultDrawColor);
        }

        public override void Update(GameTime gameTime)
        {
            //TODO update
        }
    }
}
