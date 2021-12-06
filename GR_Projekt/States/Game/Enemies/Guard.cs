using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using GR_Projekt.States.Settings.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using GR_Projekt.States.Game.HUD;
using GR_Projekt.Content.Fonts;

namespace GR_Projekt.States.Game.Enemies
{
    public class Guard : Enemies
    {
        private bool isMoving;
        private Texture2D deadTexture;
        private SpriteFont spriteFont;
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
        private Crosshair crosshair;
        Vector2 positionCollise;


        public Guard(SettingsModel settingsModel,GraphicsDevice _graphicsDevice, ContentManager contentManager, Game1 game, Vector2 _position, Vector2 _moveVector, Vector2 _speed, float _strength, float _resistance)
        {
            texture = contentManager.Load<Texture2D>(@"Images\Enemies\guard_left_right");
            deadTexture = contentManager.Load<Texture2D>(@"Images\Enemies\guard_dead");
            this.spriteFont = contentManager.Load<SpriteFont>(Fonts.Copperplate(fontSize: 12));
          

            this.graphicsDevice = _graphicsDevice;
            this.spriteBatch = new SpriteBatch(_graphicsDevice);
            this.graphicsDeviceManager = game.GameGraphicsDeviceManager;


            this.position = _position;
            this.moveVector = _moveVector;
            this.speed = _speed;
            this.strength = _strength;
            this.resistance = _resistance;

            frameSize = new Point(64, 64);
            currentFrame = new Point(0, 0);
            sheetSize = new Point(2, 5);
            deadSheetSize = new Point(4, 1);

            isMoving = false;
            //position = new Vector2(1, 0);

            this.enemiesTransformationEffects = new EnemiesTransformationEffects(graphicsDeviceManager, _graphicsDevice);

            map = new Map(contentManager, _graphicsDevice, game, settingsModel);
            crosshair = new Crosshair(contentManager, new Rectangle(0, 0, graphicsDeviceManager.GraphicsDevice.Viewport.Width, (int)(graphicsDeviceManager.GraphicsDevice.Viewport.Height * 0.8)).Center);
        }

        public void render()
        {
            List<int>[] baseMap = map.GetBaseMap();


            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    if(baseMap[x][y] == 0)
                    {
                        //resp 0-floor
                        position = new Vector2(x * 150, y*100);
                        currentRectangle = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);

                    }
                }
            }


        }

        private Vector2 collisePoint()
        {
            bool collide;
            Vector2 position;
            Point point;
            while (true)
            {
                Random rnd = new Random();
                int x = rnd.Next(1, map.map.Count);
                int y = rnd.Next(1, map.map[0].Count);
                position = new Vector2(x, y);
                point = new Point((int)position.X, (int)position.Y);

                collide = map.Collide(point);
                if (collide)
                {
                    Debug.WriteLine("collise: " + position);
                    positionCollise = position * 150;
                    return position * 150;
                }
            }
        }

        public override void start()
        {
            isMoving = true;
            resistance = 100;
            strength = 10;


            //render();
            position = collisePoint();
            speed = new Vector2(1.0f, 0);
            currentRectangle = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);

            //position = new Vector2(1000, 0);
            //currentRectangle = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);

            //position = new Vector2(x: 3500.0f, y: -3100.0f);
            //position = new Vector2(x: (float)random.Next(0, graphicsDevice.Viewport.Width - frameSize.X), y: (float)random.Next(0, graphicsDevice.Viewport.Height - frameSize.Y));
        }

        public override void reset()
        {
            isMoving = false;
            moveVector = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            Viewport viewport = graphicsDevice.Viewport;
            //sheetSize = new Point(2, 5);


            if (isMoving == false) start();


            if (position.Y >= positionCollise.Y + 400 || position.Y <= positionCollise.Y - 400)
            {
                speed.Y *= -1;
            }

            if (position.X >= positionCollise.X + 100 || position.X <= positionCollise.X - 100)
            {
                speed.X *= -1;
            }


            if (isMoving)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > 100)
                {
                    timeSinceLastFrame -= 100;
                    if (speed.X > 0)
                    {
                        currentFrame.X = 1;
                        ++currentFrame.Y;
                        if (currentFrame.Y >= sheetSize.Y)
                        {
                            currentFrame.Y = 1;
                        }
                    }

                    if (speed.X < 0)
                    {
                        currentFrame.X = 0;
                        ++currentFrame.Y;
                        if (currentFrame.Y >= sheetSize.Y)
                        {
                            currentFrame.Y = 1;
                        }
                    }

                }

                if (resistance <= 5)
                {
                    currentFrame.X = 0;
                    currentFrame.X += 1;
                    currentFrame.Y = 0;
                    if (currentFrame.X >= deadSheetSize.X)
                    {
                        currentFrame.X = 0;
                    }
                    //position = Vector2.Zero;
                    speed = Vector2.Zero;

                }

                position += speed;
                currentRectangle = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);
            }

            //if (resistance <= 0)
            //{
            //    //respawn
            //    reset();
            //}


            MouseState mouse = Mouse.GetState();
            //(Mouse.GetState().LeftButton == ButtonState.Pressed) && 

            //Debug.WriteLine("Current Rectangle: " + currentRectangle);
            //Debug.WriteLine("Crosshair Rectang: " + crosshair.getCrosshairRectangle);
            if ((Mouse.GetState().LeftButton == ButtonState.Pressed))
            {
                this.resistance--;
            }
            
            if ((currentRectangle.Contains(crosshair.getCrosshairRectangle)))
            {
                Debug.WriteLine("Contains" + crosshair.getCrosshairRectangle + "& " + currentRectangle);
                this.resistance--;
            }


        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //this.drawGuardZ(currentRectangle.X, currentRectangle.Y);
            this.drawTexture(currentRectangle);
            Debug.WriteLine("Guard Pos: " + position);

            //if (resistance <= 5)
            //{
            //    currentRectangle = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);
            //    resistance--;
            //}

        }




        public void updateCamera(Vector3 camPosition, Vector3 camTarget)
        {
            this.cameraPosition = camPosition;
            this.cameraTarget = camTarget;
        }

        public void drawGuardZ(int x, int y)
        {
            //Texture2D txt = GetWallTexture(type);
            Matrix view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);
            BasicEffect rightWallEfect = this.enemiesTransformationEffects.getGuardZEffect(view, x, y);

            spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, rightWallEfect);

            spriteBatch.Draw(texture, new Rectangle(x, 0, frameSize.X, frameSize.Y), new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
            spriteBatch.End();
        }

        //drawTx -> TransformationEffects
        private void drawTexture(Rectangle currentRectangle)
        {
            if (texture is null)
            {
                return;
            }
            Matrix view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);
            BasicEffect floorEffect = this.enemiesTransformationEffects.getTextureEffect(view);
            //floorEffect.World = floorEffect.World * Matrix.CreateTranslation(new Vector3(-500, 0, 0));

            spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, floorEffect);

            //spriteBatch.Draw(texture, new Rectangle(x, y, 100, 100), new Rectangle(0, 0, texture.Width, texture.Height), Color.White);

            spriteBatch.Draw(texture, new Rectangle(currentRectangle.X,0, frameSize.X, frameSize.Y * 2), new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White);


            spriteBatch.DrawString(spriteFont, resistance.ToString(), new Vector2(currentRectangle.X+15, 0-20), Color.White);

            if (resistance <= 5)
            {
                spriteBatch.Draw(deadTexture, new Rectangle(currentRectangle.X, 0, frameSize.X, frameSize.Y * 2), new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White);
                --resistance;
                if (resistance <= 0) reset();
            }

            spriteBatch.End();
        }
    }
}
