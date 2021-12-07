using GR_Projekt.Utils.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.Utils.Dialogue
{
    class NPC
    {
        private GraphicsDevice _graphics;
        private BasicEffect basicEffect;

        private Texture2D npcTexture;
        private int margin = 10;
        private TransformationEffects transformEffects;
        private int xPozTexture, yPozTexture;
        private int widthTexture, heightTexture;
        private Rectangle rectPlayer, rectTexture;
        public NPC(GraphicsDevice graphicsDevice, BasicEffect basicEffect, ContentManager content, GraphicsDeviceManager graphicsDeviceManager, Point point)
        {
            _graphics = graphicsDevice;
            this.basicEffect = basicEffect;

            _graphics = graphicsDevice;
            this.basicEffect = basicEffect;
            npcTexture = content.Load<Texture2D>(@"Images/Dialogue/soldierTexture");
            this.transformEffects = new TransformationEffects(graphicsDeviceManager, graphicsDevice);
            xPozTexture = point.X * 100;
            yPozTexture = point.Y * 100;
            widthTexture = 70;
            heightTexture = 100;
        }

        public void DrawNPC(GameTime gameTime, SpriteBatch spriteBatch, Vector3 camPosition, Vector3 camTarget)
        {
            Matrix view = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);
            BasicEffect leftWallEfect = this.transformEffects.getLeftWallEffect(view, xPozTexture, yPozTexture);

            spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, leftWallEfect);
            Color color;
            if (!collisionPlayerWithNPC(camPosition))
                color = Color.White;
            else
                color = Color.Blue;

            spriteBatch.Draw(npcTexture, new Rectangle(xPozTexture, heightTexture, widthTexture, heightTexture), color);
            spriteBatch.End();
        }

        public bool collisionPlayerWithNPC(Vector3 camPosition)
        {
            rectPlayer = new Rectangle((int)(camPosition.X), (int)(-1 * (camPosition.Z)), 20 * margin, 20 * margin);

            rectTexture = new Rectangle(xPozTexture + 20 * margin, yPozTexture + 20 * margin, 20 * margin, 20 * margin);

            return rectTexture.Intersects(rectPlayer);
        }
    }
}