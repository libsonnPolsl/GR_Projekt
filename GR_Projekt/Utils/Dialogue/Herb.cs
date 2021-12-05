using GR_Projekt.Utils.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GR_Projekt.Utils.Dialogue
{
    class Herb
    {
        private GraphicsDevice _graphics;
        private BasicEffect basicEffect;
        private Texture2D herbTexture;
        private int margin = 50;
        private TransformationEffects transformEffects;
        public List<List<Block>> mapList;
        public Herb(GraphicsDevice graphicsDevice, BasicEffect basicEffect, ContentManager content, GraphicsDeviceManager graphicsDeviceManager)
        {
            _graphics = graphicsDevice;
            this.basicEffect = basicEffect;
            herbTexture = content.Load<Texture2D>(@"Images/Dialogue/treeTexture");
            this.transformEffects = new TransformationEffects(graphicsDeviceManager, graphicsDevice);
            
        }
        public void UpdateHerb(GameTime gameTime)
        {
            
        }

        public void DrawHerb(GameTime gameTime, SpriteBatch spriteBatch, Vector3 camPosition, Vector3 camTarget)
        {
            Matrix view = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);
            BasicEffect leftWallEfect = this.transformEffects.getLeftWallEffect(view, 1000, 3000);

            spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, leftWallEfect);

            spriteBatch.Draw(herbTexture, new Rectangle(0, 0, 150, 250), Color.Red);
            spriteBatch.End();
        }
    }
}
