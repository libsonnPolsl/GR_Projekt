using System;
using System.Diagnostics;
using GR_Projekt.Content.Fonts;
using GR_Projekt.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GR_Projekt.States
{
    public class GameState : State
    {
        private string numberOfChanges = "Game state";
        private SpriteFont _textFontBold;

        public GameState(ContentManager content, GraphicsDevice graphicsDevice, Game1 game) : base(content, graphicsDevice, game, StateTypeEnumeration.Game) {
            _textFontBold = content.Load<SpriteFont>(Fonts.Arial(fontSize: 16, fontStyle: FontStyleEnumeration.Bold));
        }

        public override void Dispose()
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont: _textFontBold, text: numberOfChanges.ToString(), position: new Vector2(100, 100), color: Colors.red);
        }

        public override void Update(GameTime gameTime, KeyboardState previousState, KeyboardState currentState)
        {
            if (KeyboardHandler.WasKeyPressedAndReleased( previousState, currentState, Keys.Escape))
            {
                _game.ChangeState(newState: new PauseState(_game, _graphicsDevice, _contentManager));
            }


        }
    }
}
