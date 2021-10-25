using System;
using GR_Projekt.Content.Fonts;
using GR_Projekt.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.States
{
    public class GameState : State
    {

        private SpriteFont _textFontBold;
        private string _toShow;

        public GameState(ContentManager content, GraphicsDevice graphicsDevice, Game1 game) : base(content, graphicsDevice, game){

            _textFontBold = content.Load<SpriteFont>(Fonts.Arial(fontSize: 16, fontStyle: FontStyleEnumeration.Bold));

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont: _textFontBold, text: _toShow, position: new Vector2(100, 100), color: Colors.red);
        }

        public override void Update(GameTime gameTime)
        {
            _toShow = gameTime.TotalGameTime.Seconds.ToString();
           
        }
    }
}
