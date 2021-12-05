using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GR_Projekt.Utils.Dialogue
{
    class NPC
    {
        private GraphicsDevice _graphics;
        private BasicEffect basicEffect;
        NPC(GraphicsDevice graphicsDevice, BasicEffect basicEffect, ContentManager content)
        {
            _graphics = graphicsDevice;
            this.basicEffect = basicEffect;
        }

        public void UpdateDictator(GameTime gameTime) 
        {

        }

        public void DrawDictator(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
