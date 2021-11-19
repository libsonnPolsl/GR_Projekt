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
        MouseState lastMouseState;
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

            LoadCubeAndBuffer();
        }              

        protected void LoadCubeAndBuffer()
        {
            userPrimitives = new VertexPositionColor[36];
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

            userPrimitives[12] = new VertexPositionColor(new Vector3(1, -1, -1), Color.Red);
            userPrimitives[13] = new VertexPositionColor(new Vector3(1, 1, -1), Color.Red);
            userPrimitives[14] = new VertexPositionColor(new Vector3(-1, -1, -1), Color.Red);

            userPrimitives[15] = new VertexPositionColor(new Vector3(1, 1, -1), Color.Red);
            userPrimitives[16] = new VertexPositionColor(new Vector3(-1, 1, -1), Color.Red);
            userPrimitives[17] = new VertexPositionColor(new Vector3(-1, -1, -1), Color.Red);

            userPrimitives[18] = new VertexPositionColor(new Vector3(-1, -1, -1), Color.Yellow);
            userPrimitives[19] = new VertexPositionColor(new Vector3(-1, 1, -1), Color.Yellow);
            userPrimitives[20] = new VertexPositionColor(new Vector3(-1, -1, 1), Color.Yellow);

            userPrimitives[21] = new VertexPositionColor(new Vector3(-1, 1, -1), Color.Yellow);
            userPrimitives[22] = new VertexPositionColor(new Vector3(-1, 1, 1), Color.Yellow);
            userPrimitives[23] = new VertexPositionColor(new Vector3(-1, -1, 1), Color.Yellow);

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

            vertexBuffer = new VertexBuffer(_graphics,
                typeof(VertexPositionColor),
                36, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(userPrimitives);

            lastMouseState = Mouse.GetState();
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

            basicEffect.World = worldMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectionMatrix;

            deltaX = (float)lastMouseState.X - (float)mouseState.X;
            deltaY = (float)lastMouseState.Y - (float)mouseState.Y;
            angleX += deltaX * sensitivity;
            angleY += deltaY * sensitivity;
            /*Trace.WriteLine("Mouse State = " + (float)mouseState.X);
            Trace.WriteLine("Last Mouse State = " + (float)lastMouseState.X);*/
            Matrix rotationMatrix = Matrix.CreateFromYawPitchRoll(angleX, angleY, 0);
            translation = Vector3.Transform(translation, rotationMatrix);
            translation.Y = 0;
            camPosition += translation * 0.1f;
            translation = Vector3.Zero;
            Vector3 forward = Vector3.Transform(Vector3.Forward, rotationMatrix);
            camTarget = camPosition + forward;
            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);

            lastMouseState = mouseState;
        }

        public void DrawPlayer(GameTime gameTime)
        {
            _graphics.Clear(Color.IndianRed);
            _graphics.SetVertexBuffer(vertexBuffer);

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphics.DrawPrimitives(PrimitiveType.TriangleList, 0, 36);
            }
        }
    }
}