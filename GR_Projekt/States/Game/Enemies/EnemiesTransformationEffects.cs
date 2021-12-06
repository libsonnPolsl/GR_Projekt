using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GR_Projekt.States.Game.Enemies
{
    class EnemiesTransformationEffects
    {
        private GraphicsDeviceManager _graphics;
        private GraphicsDevice _graphicsDevice;

        private readonly float fovAngle = MathHelper.ToRadians(45);
        private readonly float aspectRatio;
        private readonly float near = 0.01f;
        private readonly float far = 20000f;

        public BasicEffect textureEffect;


        public EnemiesTransformationEffects(GraphicsDeviceManager _graphics, GraphicsDevice _graphicsDevice)
        {
            this._graphics = _graphics;
            this._graphicsDevice = _graphicsDevice;
            aspectRatio = _graphics.PreferredBackBufferWidth / _graphics.PreferredBackBufferHeight;

            SetEffects();
        }

        public void SetEffects()
        {
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(fovAngle, aspectRatio, near, far);

            BasicEffect basicEffect = new BasicEffect(this._graphicsDevice)
            {
                TextureEnabled = true,
                VertexColorEnabled = true,

                Projection = projection
            };


            textureEffect = (BasicEffect)basicEffect.Clone();
        }

        public BasicEffect getTextureEffect(Matrix view)
        {
            Matrix world = Matrix.CreateScale(1, -1, 1); //* Matrix.CreateRotationX(MathHelper.ToRadians(90)) * Matrix.CreateTranslation(new Vector3(0, -100, -100));

            textureEffect.World = world;
            textureEffect.View = view;

            return textureEffect;
        }

        public BasicEffect getTextureEffectDoctor(Matrix view)
        {
            //Matrix world = Matrix.CreateScale(1, 1, -1); //* Matrix.CreateRotationX(MathHelper.ToRadians(90)) * Matrix.CreateTranslation(new Vector3(0, -100, -100));

            //textureEffect.World = world;
            //textureEffect.View = view;

            //return textureEffect;

            Matrix world = Matrix.CreateScale(1, -1, 1) * Matrix.CreateRotationY(MathHelper.ToRadians(360)) * Matrix.CreateTranslation(new Vector3(10, 10, 10));

            textureEffect.World = world;
            textureEffect.View = view;

            return textureEffect;
        }

    }
}
