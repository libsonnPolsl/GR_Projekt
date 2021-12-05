using GR_Projekt.Utils.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GR_Projekt.Utils.Dialogue
{
    class Herb
    {
        private GraphicsDevice _graphics;
        private BasicEffect basicEffect;
        private Texture2D herbTexture;
        private int margin = 10;
        private TransformationEffects transformEffects;
        private int xPozTexture, yPozTexture;
        private int widthTexture, heightTexture;
        private Rectangle rectPlayer, rectTexture;

        public Herb(GraphicsDevice graphicsDevice, BasicEffect basicEffect, ContentManager content, GraphicsDeviceManager graphicsDeviceManager)
        {
            _graphics = graphicsDevice;
            this.basicEffect = basicEffect;
            herbTexture = content.Load<Texture2D>(@"Images/Dialogue/treeTexture");
            this.transformEffects = new TransformationEffects(graphicsDeviceManager, graphicsDevice);
            xPozTexture = 3500;
            yPozTexture = 3100;
            widthTexture = 100;
            heightTexture = 100;

            
        }
        public void UpdateHerb(GameTime gameTime)
        {
            
            //Debug.WriteLine("e");
        }

        public void DrawHerb(GameTime gameTime, SpriteBatch spriteBatch, Vector3 camPosition, Vector3 camTarget)
        {
            Matrix view = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);
            BasicEffect leftWallEfect = this.transformEffects.getLeftWallEffect(view, xPozTexture, yPozTexture);

            spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, leftWallEfect);

            spriteBatch.Draw(herbTexture, new Rectangle(xPozTexture, widthTexture, widthTexture, heightTexture), Color.Green);

            spriteBatch.End();
        }

        public bool collisionPlayerWithHerb(Vector3 camPosition)
        {
            rectPlayer = new Rectangle((int)(camPosition.X), (int)(-1 * (camPosition.Z)), 20 * margin, 20 * margin);

            rectTexture = new Rectangle(xPozTexture + 20 * margin, yPozTexture + 20 * margin, 20 * margin, 20 * margin);

            return rectTexture.Intersects(rectPlayer);
        }
    }
}
