using System;
using System.Collections.Generic;
using System.Diagnostics;
using GR_Projekt.Content.Fonts;
using GR_Projekt.Content.Images;
using GR_Projekt.Core;
using GR_Projekt.Core.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.States
{
    public class MenuState : State
    {
        private List<Component> _components;


        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentMenager) : base(contentMenager, graphicsDevice, game)
        {
            Texture2D _buttonTexture = _contentManager.Load<Texture2D>(ControlsImages.wideButtonImage);
            SpriteFont _textFont = _contentManager.Load<SpriteFont>(Fonts.Arial(fontSize: 12));
            string _buttonText = "NEW GAME";
            Vector2 _buttonPosition = new Vector2(x: 100, y: 100);
            Rectangle _buttonRectangle = new Rectangle(x: (int)_buttonPosition.X, y: (int)_buttonPosition.Y, width: 300, height: 100);

            Button _newGameButton = new Button(texture: _buttonTexture, font: _textFont, buttonText: _buttonText, position: _buttonPosition, buttonRectangle: _buttonRectangle, onClick: onNewGameButtonClick);

            _components = new List<Component>();
            _components.Add(_newGameButton);
        }

        private void onNewGameButtonClick(object sender, EventArgs e) {
            _game.ChangeState(newState: new GameState(_contentManager, _graphicsDevice, _game));

        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            foreach (Component component in _components) {
                component.Draw(gameTime: gameTime, spriteBatch: spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        { 
            foreach (Component component in _components)
            {
                component.Update(gameTime: gameTime);
            }
        }
    }
}
