using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using System;
using Microsoft.Xna.Framework.Audio;

namespace GR_Projekt.States.Game
{
    public class Player
    {
        private readonly GraphicsDevice _graphics;
        private Texture2D currentTexture;
        private readonly Texture2D[] weaponSprite, reloadSprite;
        private readonly ContentManager content;
        private MouseState prevMouse;
        private Rectangle currentRectangle;
        Matrix worldMatrix, viewMatrix, projectionMatrix;
        public Vector3 camTarget, camPosition, translation, scanCollision;
        private float angleY = 0.0f;
        public float angleX = 0.0f;
        private float deltaX = 0.0f;
        private float deltaY = 0.0f;
        private readonly float sensitivity = 0.002f;
        private float moveSpeed = 0.0f;
        private readonly float maxMoveSpeed = 10f;
        private float currentTime = .0f;
        private float lastCurrentTime = .0f;
        const float accSpeed = 2f, spriteScreenTime = 35f;
        BasicEffect basicEffect;
        private SoundEffect shootingSound, emptyClipSound;
        private static readonly Point frameSize = new Point(800, 600);
        private Point currentFrame = new Point(0, 0);
        private Point sheetSize = new Point(2, 3);
        private bool isShooting, isReloading;
        protected int index = 0, _ammo, _inMagAmmo, _health, _score;
        float timeMS;
        Map map;

        public Player(ref Matrix worldMatrix, ref Matrix viewMatrix, ref Matrix projectionMatrix,
            GraphicsDevice graphicsDevice, BasicEffect basicEffect, ContentManager content, Map map)
        {
            this._ammo = 12;
            this._inMagAmmo = 12;
            this._score = 0;
            this._health = 100;

            _graphics = graphicsDevice;

            weaponSprite = new Texture2D[2];
            reloadSprite = new Texture2D[5];

            camTarget = Vector3.Zero;
            camPosition = new Vector3(3500.0f, 15.0f, -3100.0f);

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

            prevMouse = Mouse.GetState();

            currentRectangle = new Rectangle(0, 0, 800, 600);

            this.map = map;

            LoadContent();
        }


        public void Update(GameTime gameTime)
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
            if (keyboard.IsKeyDown(Keys.R) && _ammo < _inMagAmmo && !isShooting && !isReloading)
            {
                Reload(gameTime);
            }

            if (isShooting) AnimateShooting(gameTime);

            if (isReloading) AnimateReload(gameTime);

            if (mouseState.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released && !isShooting && !isReloading)
            {
                Shoot(gameTime);
            }

            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.S))
            {
                if (moveSpeed + accSpeed > maxMoveSpeed) moveSpeed = maxMoveSpeed;
                else moveSpeed += accSpeed;
            }
            else
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
            }
            else if (angleY > 1.57)
            {
                angleY = 1.57f;
                rotationMatrix = Matrix.CreateFromYawPitchRoll(angleX, angleY, 0);
            }
            Vector3 up = Vector3.Transform(Vector3.Up, rotationMatrix);
            translation = Vector3.Transform(translation, rotationMatrix);
            translation.Y = 0;
            scanCollision = camPosition + translation * 80;
            if (!map.Collide(new Point((int)scanCollision.X, (int)scanCollision.Z)))
            {
                camPosition += translation * moveSpeed;
            }
            translation = Vector3.Zero;
            Vector3 forward = Vector3.Transform(Vector3.Forward, rotationMatrix);
            camTarget = camPosition + forward;
            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, up);

            prevMouse = mouseState;

            Mouse.SetPosition(_graphics.Viewport.Width / 2, _graphics.Viewport.Height / 2);

            timeMS += gameTime.ElapsedGameTime.Milliseconds;
        }

        public void LoadContent()
        {
            weaponSprite[0] = content.Load<Texture2D>(@"Images/Player/shoot-0");
            weaponSprite[1] = content.Load<Texture2D>(@"Images/Player/shoot-1");

            reloadSprite[0] = content.Load<Texture2D>(@"Images/Player/reload-0");
            reloadSprite[1] = content.Load<Texture2D>(@"Images/Player/reload-1");
            reloadSprite[2] = content.Load<Texture2D>(@"Images/Player/reload-2");
            reloadSprite[3] = content.Load<Texture2D>(@"Images/Player/reload-3");
            reloadSprite[4] = content.Load<Texture2D>(@"Images/Player/reload-4");

            currentTexture = weaponSprite[0];

            shootingSound = content.Load<SoundEffect>(@"Sounds/shoot");
            emptyClipSound = content.Load<SoundEffect>(@"Sounds/emptyClip");
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            try
            {
                spriteBatch.Begin();
            }
            catch (InvalidOperationException e)
            {

            }
            spriteBatch.Draw(currentTexture, new Vector2(_graphics.Viewport.Width / 2 - 400, (_graphics.Viewport.Height * 0.8f) - 600), currentRectangle, Color.White);

            Trace.WriteLine(currentRectangle);

            spriteBatch.End();
        }

        public void AnimateReload(GameTime gameTime)
        {
            currentTime = timeMS;
            if (lastCurrentTime + spriteScreenTime < currentTime)
            {
                lastCurrentTime = currentTime;
                currentTexture = reloadSprite[index];
                currentFrame.X++;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    currentFrame.Y++;
                    if (currentFrame.Y >= sheetSize.Y && index < 3)
                    {
                        currentFrame.Y = 0;
                        currentTexture = reloadSprite[++index];
                    }
                    else if (currentFrame.Y >= sheetSize.Y && index == 3)
                    {
                        currentFrame.Y = 0;
                        sheetSize.Y = 1;
                        sheetSize.X = 2;
                        currentTexture = reloadSprite[++index];
                    }
                    else if (currentFrame.Y >= sheetSize.Y && index == 4)
                    {
                        currentFrame.Y = 0;
                        sheetSize.Y = 3;
                        sheetSize.X = 2;
                        currentTexture = weaponSprite[0];
                        index = 0;
                        isReloading = false;
                    }
                }

                currentRectangle.X = currentFrame.X * 800;
                currentRectangle.Y = currentFrame.Y * 600;
            }
        }

        public void AnimateShooting(GameTime gameTime)
        {
            currentTime = timeMS;

            if (lastCurrentTime + spriteScreenTime < currentTime)
            {
                lastCurrentTime = currentTime;

                currentFrame.X++;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    currentFrame.Y++;
                    if (currentFrame.Y >= sheetSize.Y && sheetSize != new Point(1, 2))
                    {
                        currentFrame.Y = 0;
                        sheetSize.Y = 2;
                        sheetSize.X = 1;
                        currentTexture = weaponSprite[1];
                    }
                    else if (currentFrame.Y >= sheetSize.Y && sheetSize == new Point(1, 2))
                    {
                        currentFrame.Y = 0;
                        sheetSize.Y = 3;
                        sheetSize.X = 2;
                        currentTexture = weaponSprite[0];
                        isShooting = false;
                    }
                }

                currentRectangle.X = currentFrame.X * 800;
                currentRectangle.Y = currentFrame.Y * 600;

            }
        }

        public void Shoot(GameTime gameTime)
        {
            if (_ammo > 0)
            {
                isShooting = true;
                lastCurrentTime = timeMS;
                shootingSound.Play();
                _ammo--;
            }
            else
            {
                emptyClipSound.Play();
            }
        }

        public void Reload(GameTime gameTime)
        {
            isReloading = true;
            _ammo = _inMagAmmo;
        }

        public int getTotalAmmo => _ammo;
        public int getInMagAmmo => _inMagAmmo;
        public int getPlayerScore => _score;
        public int getPlayerHealth => _health;
    }
}