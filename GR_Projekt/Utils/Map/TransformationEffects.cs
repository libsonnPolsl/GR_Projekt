using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.Utils.Map
{
    class TransformationEffects
    {
        private GraphicsDeviceManager _graphics;
        private GraphicsDevice _graphicsDevice;

        private readonly float fovAngle = MathHelper.ToRadians(45);
        private readonly float aspectRatio;
        private readonly float near = 0.01f;
        private readonly float far = 20000f;

        public BasicEffect floorEffect;
        public BasicEffect topWallEffect;
        public BasicEffect bottomWallEffect;
        public BasicEffect leftWallEffect;
        public BasicEffect rightWallEffect;

        public TransformationEffects(GraphicsDeviceManager _graphics, GraphicsDevice _graphicsDevice)
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

            floorEffect = (BasicEffect)basicEffect.Clone();
            topWallEffect = (BasicEffect)basicEffect.Clone();
            bottomWallEffect = (BasicEffect)basicEffect.Clone();
            leftWallEffect = (BasicEffect)basicEffect.Clone();
            rightWallEffect = (BasicEffect)basicEffect.Clone();
        }

        public BasicEffect getFloorEffect(Matrix view)
        {
            Matrix world = Matrix.CreateScale(1, -1, 1) * Matrix.CreateRotationX(MathHelper.ToRadians(90)) * Matrix.CreateTranslation(new Vector3(0, -100, -100));

            floorEffect.World = world;
            floorEffect.View = view;

            return floorEffect;
        }

        public BasicEffect getTopWallEffect(Matrix view, int x, int y)
        {

            Matrix world = Matrix.CreateScale(1, -1, 1) * Matrix.CreateRotationY(MathHelper.ToRadians(90)) * Matrix.CreateTranslation(new Vector3(x, 0, -y - 100));

            topWallEffect.World = world;
            topWallEffect.View = view;

            return topWallEffect;
        }

        public BasicEffect getBottomWallEffect(Matrix view, int x, int y)
        {

            Matrix world = Matrix.CreateScale(1, -1, 1) * Matrix.CreateRotationY(MathHelper.ToRadians(90)) * Matrix.CreateTranslation(new Vector3(x + 100, 0, -y - 100));

            bottomWallEffect.World = world;
            bottomWallEffect.View = view;

            return bottomWallEffect;
        }

        public BasicEffect getRightWallEffect(Matrix view, int x, int y)
        {

            Matrix world = Matrix.CreateScale(1, -1, 1) * Matrix.CreateRotationY(MathHelper.ToRadians(360)) * Matrix.CreateTranslation(new Vector3(0, 0, -y - 200));

            bottomWallEffect.World = world;
            bottomWallEffect.View = view;

            return bottomWallEffect;
        }

        public BasicEffect getLeftWallEffect(Matrix view, int x, int y)
        {

            Matrix world = Matrix.CreateScale(1, -1, 1) * Matrix.CreateRotationY(MathHelper.ToRadians(360)) * Matrix.CreateTranslation(new Vector3(0, 0, -y - 100));

            bottomWallEffect.World = world;
            bottomWallEffect.View = view;

            return bottomWallEffect;
        }
    }
}
