using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.States
{
    public abstract class State
    {
        protected ContentManager _contentManager;
        protected Game1 _game;
        protected GraphicsDevice _graphicsDevice;

        public State(ContentManager content, GraphicsDevice graphicsDevice, Game1 game) {
            this._contentManager = content;
            this._game = game;
            this._graphicsDevice = graphicsDevice;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
