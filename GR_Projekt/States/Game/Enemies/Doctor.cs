using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using GR_Projekt.States.Settings.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace GR_Projekt.States.Game.Enemies
{
    public class Doctor : Enemies
    {
        private bool isMoving;
        private Texture2D deadTexture;
        private SpriteFont _arialFont;
        private GraphicsDevice graphicsDevice;
        private GraphicsDeviceManager graphicsDeviceManager;

        private SettingsModel settingsModel;

        private Point deadSheetSize;

        private Random random = new Random();

        public Vector2 getPosition => position;
        public float getPositionX => position.X;
        public float getPositionY => position.Y;
        public Vector2 getSpeed => speed;
        public float getStrength => strength;
        public float getResistance => resistance;

        private EnemiesTransformationEffects enemiesTransformationEffects;

        public Vector3 cameraPosition = new Vector3(4900.0f, 3800.0f, 4000.0f);
        public Vector3 cameraTarget = new Vector3(3500.0f, 0.0f, 500.0f);
        Map map;

        public Vector3 position3d;
        public Vector3 speed3d;


        public Doctor(SettingsModel settingsModel, GraphicsDevice _graphicsDevice, ContentManager contentManager, Game1 game, Vector2 _position, Vector2 _moveVector, Vector2 _speed, float _strength, float _resistance)
        {
            texture = contentManager.Load<Texture2D>(@"Images\Enemies\ss_doctor");
            //deadTexture = contentManager.Load<Texture2D>(@"Images\Enemies\guard_dead");
            //_arialFont = contentManager.Load<SpriteFont>("Arial16Bold");

            this.graphicsDevice = _graphicsDevice;
            spriteBatch = new SpriteBatch(_graphicsDevice);
            graphicsDeviceManager = game.GameGraphicsDeviceManager;


            this.position = _position;
            this.moveVector = _moveVector;
            this.speed = _speed;
            this.strength = _strength;
            this.resistance = _resistance;

            frameSize = new Point(64, 64);
            currentFrame = new Point(0, 0);
            sheetSize = new Point(4, 1);
            
            deadSheetSize = new Point(5, 2);

            isMoving = false;
            //position = new Vector2(1, 0);

            this.enemiesTransformationEffects = new EnemiesTransformationEffects(graphicsDeviceManager, _graphicsDevice);

            map = new Map(contentManager, _graphicsDevice, game, settingsModel);
        }

        public void render()
        {
            List<int>[] baseMap = map.GetBaseMap();
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    Debug.WriteLine(baseMap);

                }
            }

        }

        public override void start()
        {
            isMoving = true;
            resistance = 100;

            //moveVector = new Vector2(x: 0, y: 1);
            //render();


            //speed = new Vector2(1f, 1f);

            //position = new Vector2(1.0f, 0.0f);

            //position = new Vector2(x: 3500.0f, y: -3100.0f);
            //position = new Vector2(x: (float)random.Next(0, graphicsDevice.Viewport.Width - frameSize.X), y: (float)random.Next(0, graphicsDevice.Viewport.Height - frameSize.Y));
        }

        public override void reset()
        {
            isMoving = false;

            moveVector = Vector2.Zero;

            //renew resistance
            resistance = 100;

            //position = new rendering position
            //position = new Vector2(1, 0);

            //position = new Vector2(x: (float)random.Next(0, graphicsDevice.Viewport.Width - frameSize.X), y: (float)random.Next(0, graphicsDevice.Viewport.Height - frameSize.Y));

            //position => to currentRectangle
            currentRectangle = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);
        }

        public override void Update(GameTime gameTime)
        {
            Viewport viewport = graphicsDevice.Viewport;
            //sheetSize = new Point(2, 5);


            if (isMoving == false) start();


            if (position.Y >= viewport.Height - frameSize.Y || position.Y <= 0)
            {
                speed.Y *= -1;
            }

            if (position.X >= viewport.Width - frameSize.Y || position.X <= 0)
            {
                speed.X *= -1;
            }


            if (isMoving)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > 100)
                {
                    timeSinceLastFrame -= 100;

                    currentFrame.X += 1;
                    if (currentFrame.X >= sheetSize.X)
                    {
                        currentFrame.X = 0;
                        ++currentFrame.Y;
                        if (currentFrame.Y >= sheetSize.Y)
                        {
                            currentFrame.Y = 0;
                        }
                    }
                    //if (speed.X > 0)
                    //{
                    //    currentFrame.X = 1;
                    //    ++currentFrame.Y;
                    //    if (currentFrame.Y >= sheetSize.Y)
                    //    {
                    //        currentFrame.Y = 1;
                    //    }
                    //}

                    //if (speed.X < 0)
                    //{
                    //    currentFrame.X = 0;
                    //    ++currentFrame.Y;
                    //    if (currentFrame.Y >= sheetSize.Y)
                    //    {
                    //        currentFrame.Y = 1;
                    //    }
                    //}

                }

                if (resistance <= 5)
                {
                    currentFrame.X += 1;
                    currentFrame.Y = 0;
                    if (currentFrame.X >= deadSheetSize.X)
                    {
                        currentFrame.X = 0;
                    }
                    //position = Vector2.Zero;
                    //speed = Vector2.Zero;

                }

                position += speed;
                currentRectangle = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);
            }

            if (resistance <= 0)
            {
                //respawn
                reset();
            }


            MouseState mouse = Mouse.GetState();
            if ((Mouse.GetState().LeftButton == ButtonState.Pressed) && (currentRectangle.Contains(mouse.X, mouse.Y)))
            {
                this.resistance--;
            }


        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.drawTexture(currentRectangle);



            //spriteBatch.Begin();
            //spriteBatch.Draw(texture: texture, position: position, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White);
            ////spriteBatch.DrawString(_arialFont, "Guard resistance: " + resistance.ToString(), new Vector2(0, 20), Color.White);

            //if (resistance <= 5)
            //{
            //    spriteBatch.Draw(deadTexture, position, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White);
            //    resistance--;
            //}

            //spriteBatch.End();
        }




        public void updateCamera(Vector3 camPosition, Vector3 camTarget)
        {
            this.cameraPosition = camPosition;
            this.cameraTarget = camTarget;
        }

        //drawTx -> TransformationEffects
        private void drawTexture(Rectangle currentRectangle)
        {
            if (texture is null)
            {
                return;
            }
            Matrix view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);
            BasicEffect floorEffect = this.enemiesTransformationEffects.getTextureEffectDoctor(view);

            spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, floorEffect);

            //spriteBatch.Draw(texture, new Rectangle(x, y, 100, 100), new Rectangle(0, 0, texture.Width, texture.Height), Color.White);

            spriteBatch.Draw(texture, currentRectangle, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White);

            spriteBatch.End();
        }

    }
}
