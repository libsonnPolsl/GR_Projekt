using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text;

namespace GR_Projekt.States.Game.Enemies
{
    public abstract class Enemies
    {
        protected Texture2D texture;
        protected Point frameSize;
        protected Point currentFrame;
        protected Point sheetSize;

        protected Vector2 position;
        protected Vector2 moveVector;

        //destinationRectangle
        protected Rectangle currentRectangle;

        protected Vector2 speed;
        protected float strength;
        protected float resistance;

        protected SpriteBatch spriteBatch;


        protected int timeSinceLastFrame = 0;

        public abstract void start();
        public abstract void reset();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);



        //public abstract void rendering(GameTime gameTime, SpriteBatch spriteBatch);
        //public abstract void move(GameTime gameTime, int msPerFrame, GraphicsDevice graphicsDevice);
    }
}
