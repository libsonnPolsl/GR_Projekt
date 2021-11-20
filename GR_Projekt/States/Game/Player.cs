using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace GR_Projekt.States.Game
{
    class Player
    {
        private GraphicsDevice _graphics;
        Matrix worldMatrix, viewMatrix, projectionMatrix;
        Vector3 camTarget, camPosition, translation;
        float angleY = 0.0f, angleX = 0.0f, deltaX = 0.0f, deltaY = 0.0f, sensitivity = 0.005f;
        float moveSpeed = 0.0f, maxMoveSpeed = 0.15f;
        const float accSpeed = 0.02f;
        BasicEffect basicEffect;
        VertexPositionColor[] userPrimitives;
        VertexBuffer vertexBuffer;

        public Player(Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix,
            GraphicsDevice graphicsDevice, BasicEffect basicEffect)
        {
            _graphics = graphicsDevice;

            camTarget = new Vector3(0.0f, 0.0f, 0.0f);
            camPosition = new Vector3(0.0f, 0.0f, 10.0f);            

            this.worldMatrix = Matrix.Identity;
            this.viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);
            this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f),
                _graphics.Viewport.AspectRatio,
                0.1f, 1000f);

            worldMatrix = this.worldMatrix;
            viewMatrix = this.viewMatrix;
            projectionMatrix = this.projectionMatrix;

            this.basicEffect = basicEffect;
            
            Mouse.SetPosition(_graphics.Viewport.Width / 2, _graphics.Viewport.Height / 2);

            LoadCubeAndBuffer();
        }              

        protected void LoadCubeAndBuffer()
        {
            userPrimitives = new VertexPositionColor[196];
            userPrimitives[0] = new VertexPositionColor(new Vector3(-1, -1, 1), Color.Blue);
            userPrimitives[1] = new VertexPositionColor(new Vector3(-1, 1, 1), Color.Blue);
            userPrimitives[2] = new VertexPositionColor(new Vector3(1, -1, 1), Color.Blue);

            userPrimitives[3] = new VertexPositionColor(new Vector3(-1, 1, 1), Color.Blue);
            userPrimitives[4] = new VertexPositionColor(new Vector3(1, 1, 1), Color.Blue);
            userPrimitives[5] = new VertexPositionColor(new Vector3(1, -1, 1), Color.Blue);

            userPrimitives[6] = new VertexPositionColor(new Vector3(1, -1, 1), Color.White);
            userPrimitives[7] = new VertexPositionColor(new Vector3(1, 1, 1), Color.White);
            userPrimitives[8] = new VertexPositionColor(new Vector3(1, -1, -1), Color.White);

            userPrimitives[9] = new VertexPositionColor(new Vector3(1, 1, 1), Color.White);
            userPrimitives[10] = new VertexPositionColor(new Vector3(1, 1, -1), Color.White);
            userPrimitives[11] = new VertexPositionColor(new Vector3(1, -1, -1), Color.White);

            userPrimitives[12] = new VertexPositionColor(new Vector3(1, -1, -1), Color.MediumVioletRed);
            userPrimitives[13] = new VertexPositionColor(new Vector3(1, 1, -1), Color.MediumVioletRed);
            userPrimitives[14] = new VertexPositionColor(new Vector3(-1, -1, -1), Color.MediumVioletRed);

            userPrimitives[15] = new VertexPositionColor(new Vector3(1, 1, -1), Color.MediumVioletRed);
            userPrimitives[16] = new VertexPositionColor(new Vector3(-1, 1, -1), Color.MediumVioletRed);
            userPrimitives[17] = new VertexPositionColor(new Vector3(-1, -1, -1), Color.MediumVioletRed);

            userPrimitives[18] = new VertexPositionColor(new Vector3(-1, -1, -1), Color.YellowGreen);
            userPrimitives[19] = new VertexPositionColor(new Vector3(-1, 1, -1), Color.YellowGreen);
            userPrimitives[20] = new VertexPositionColor(new Vector3(-1, -1, 1), Color.YellowGreen);

            userPrimitives[21] = new VertexPositionColor(new Vector3(-1, 1, -1), Color.YellowGreen);
            userPrimitives[22] = new VertexPositionColor(new Vector3(-1, 1, 1), Color.YellowGreen);
            userPrimitives[23] = new VertexPositionColor(new Vector3(-1, -1, 1), Color.YellowGreen);

            userPrimitives[24] = new VertexPositionColor(new Vector3(-1, 1, 1), Color.Green);
            userPrimitives[25] = new VertexPositionColor(new Vector3(-1, 1, -1), Color.Green);
            userPrimitives[26] = new VertexPositionColor(new Vector3(1, 1, 1), Color.Green);

            userPrimitives[27] = new VertexPositionColor(new Vector3(-1, 1, -1), Color.Green);
            userPrimitives[28] = new VertexPositionColor(new Vector3(1, 1, -1), Color.Green);
            userPrimitives[29] = new VertexPositionColor(new Vector3(1, 1, 1), Color.Green);

            userPrimitives[30] = new VertexPositionColor(new Vector3(1, -1, 1), Color.Orange);
            userPrimitives[31] = new VertexPositionColor(new Vector3(1, -1, -1), Color.Orange);
            userPrimitives[32] = new VertexPositionColor(new Vector3(-1, -1, 1), Color.Orange);

            userPrimitives[33] = new VertexPositionColor(new Vector3(1, -1, -1), Color.Orange);
            userPrimitives[34] = new VertexPositionColor(new Vector3(-1, -1, -1), Color.Orange);
            userPrimitives[35] = new VertexPositionColor(new Vector3(-1, -1, 1), Color.Orange);

            int coords = 20;
            for (int i = 0; i < 80; i = i + 2)
            {
                userPrimitives[i + 35] = new VertexPositionColor(new Vector3(-20, -1, coords), Color.MediumSpringGreen);
                userPrimitives[i + 36] = new VertexPositionColor(new Vector3(20, -1, coords), Color.MediumSpringGreen);
                coords--;
            }

            for (int i = 80; i < 160; i = i + 2)
            {
                userPrimitives[i + 35] = new VertexPositionColor(new Vector3(coords, -1, 20), Color.ForestGreen);
                userPrimitives[i + 36] = new VertexPositionColor(new Vector3(coords, -1, -20), Color.ForestGreen);
                coords++;
            }

            vertexBuffer = new VertexBuffer(_graphics,
                typeof(VertexPositionColor),
                196, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(userPrimitives);
        }

        public void UpdatePlayer(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            if (keyboard.IsKeyDown(Keys.A))
            {
                translation += Vector3.Left;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                translation += Vector3.Right;
            }
            if (keyboard.IsKeyDown(Keys.W))
            {
                translation += Vector3.Forward;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                translation += Vector3.Backward;
            }

            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.S))
            {
                if (moveSpeed + accSpeed > maxMoveSpeed) moveSpeed = maxMoveSpeed;
                else moveSpeed += accSpeed;
            } else
            {
                if (moveSpeed - accSpeed < 0) moveSpeed = 0;
                else moveSpeed -= accSpeed;
            }

            basicEffect.World = worldMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectionMatrix;

            deltaX = (float)(_graphics.Viewport.Width / 2) - (float)mouseState.X;
            deltaY = (float)(_graphics.Viewport.Height / 2) - (float)mouseState.Y;            
            angleX += deltaX * sensitivity;
            angleY += deltaY * sensitivity;            
            Matrix rotationMatrix = Matrix.CreateFromYawPitchRoll(angleX, angleY, 0);
            translation = Vector3.Transform(translation, rotationMatrix);
            translation.Y = 0;
            camPosition += translation * moveSpeed;
            translation = Vector3.Zero;
            Vector3 forward = Vector3.Transform(Vector3.Forward, rotationMatrix);
            camTarget = camPosition + forward;
            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);

            Mouse.SetPosition(_graphics.Viewport.Width / 2, _graphics.Viewport.Height / 2);
        }

        public void DrawCube(GameTime gameTime)
        {            
            _graphics.SetVertexBuffer(vertexBuffer);

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphics.DrawPrimitives(PrimitiveType.LineList, 37, 80);
                _graphics.DrawPrimitives(PrimitiveType.TriangleList, 0, 12);                
            }
        }
    }
}