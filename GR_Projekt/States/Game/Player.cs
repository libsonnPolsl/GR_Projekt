using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace GR_Projekt.States.Game
{
    class Player
    {
        private GraphicsDevice _graphics;
        private SpriteBatch spriteBatch;
        private Texture2D[] weaponSprite, reloadSprite;
        private ContentManager content;
        Matrix worldMatrix, viewMatrix, projectionMatrix;
        public Vector3 camTarget, camPosition, translation;
        float angleY = 0.0f, angleX = 0.0f, deltaX = 0.0f, deltaY = 0.0f, sensitivity = 0.002f;
        float moveSpeed = 0.0f, maxMoveSpeed = 10f;
        const float accSpeed = 2f;
        BasicEffect basicEffect;
        VertexPositionColor[] userPrimitives;
        VertexBuffer vertexBuffer;
        private Point frameSize = new Point(800, 600);
        private Point currentFrame = new Point(0, 0);
        private Point sheetSize = new Point(2, 6);

        public Player(ref Matrix worldMatrix, ref Matrix viewMatrix, ref Matrix projectionMatrix,
            GraphicsDevice graphicsDevice, BasicEffect basicEffect, ContentManager content)
        {
            _graphics = graphicsDevice;

            weaponSprite = new Texture2D[2];
            reloadSprite = new Texture2D[5];

            //camTarget = new Vector3(4900.0f, 3800.0f, 4000.0f);
            camTarget = Vector3.Zero;
            camPosition = new Vector3(3500.0f, 100.0f, -3100.0f);

            this.content = content;

            this.worldMatrix = worldMatrix;
            this.viewMatrix = viewMatrix;
            this.projectionMatrix = projectionMatrix;

            this.worldMatrix = Matrix.Identity;
            this.viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);
            this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f),
                _graphics.Viewport.AspectRatio,
                0.1f, 1000f);

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
            
            if (angleY < -1.57)
            {
                angleY = -1.57f;
                rotationMatrix = Matrix.CreateFromYawPitchRoll(angleX, angleY, 0);
            } else if (angleY > 1.57)
            {
                angleY = 1.57f;
                rotationMatrix = Matrix.CreateFromYawPitchRoll(angleX, angleY, 0);
            }
            Vector3 up = Vector3.Transform(Vector3.Up, rotationMatrix);
            translation = Vector3.Transform(translation, rotationMatrix);
            translation.Y = 0;
            camPosition += translation * moveSpeed;
            translation = Vector3.Zero;            
            Vector3 forward = Vector3.Transform(Vector3.Forward, rotationMatrix);
            camTarget = camPosition + forward;
            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, up);            
                

            Mouse.SetPosition(_graphics.Viewport.Width / 2, _graphics.Viewport.Height / 2);
        }

        /*public void DrawCube(GameTime gameTime)
        {            
            _graphics.SetVertexBuffer(vertexBuffer);

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphics.DrawPrimitives(PrimitiveType.LineList, 37, 80);
                _graphics.DrawPrimitives(PrimitiveType.TriangleList, 0, 12);                
            }
        }*/      
        
        public void LoadPlayerAnimation()
        {
            spriteBatch = new SpriteBatch(_graphics);

            weaponSprite[0] = content.Load<Texture2D>(@"Content/Images/Player/shoot-0");
            weaponSprite[1] = content.Load<Texture2D>(@"Content/Images/Player/shoot-1");

            reloadSprite[0] = content.Load<Texture2D>(@"Content/Images/Player/reload-0");
            reloadSprite[1] = content.Load<Texture2D>(@"Content/Images/Player/reload-1");
            reloadSprite[2] = content.Load<Texture2D>(@"Content/Images/Player/reload-2");
            reloadSprite[3] = content.Load<Texture2D>(@"Content/Images/Player/reload-3");
            reloadSprite[4] = content.Load<Texture2D>(@"Content/Images/Player/reload-4");
        }

        public void ShootWeaponAnimation()
        {
            spriteBatch.Draw(weaponSprite[0], new Vector2(0, 0), Color.White);
        }
    }
}