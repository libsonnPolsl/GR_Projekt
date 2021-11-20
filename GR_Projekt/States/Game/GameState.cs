using System.Diagnostics;
using GR_Projekt.Content.Fonts;
using GR_Projekt.Content.Sounds;
using GR_Projekt.Core;
using GR_Projekt.States.Game;
using GR_Projekt.States.Settings.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GR_Projekt.States
{
    public class GameState : State
    {
        private string text = "Game state";
        private SpriteFont _textFontBold;
        private int _pressed = 0;

        private Player player;
        private Matrix worldMatrix, viewMatrix, projectionMatrix;
        private GraphicsDevice graphicsDevice;
        private BasicEffect basicEffect;

        public GameState(ContentManager content, GraphicsDevice graphicsDevice, Game1 game, SettingsModel settingsModel) : base(content, graphicsDevice, game, settingsModel, StateTypeEnumeration.Game)
        {
            _textFontBold = content.Load<SpriteFont>(Fonts.Copperplate(fontSize: 16, fontStyle: FontStyleEnumeration.Bold));
            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;
            basicEffect.Alpha = 1.0f;
            this.graphicsDevice = graphicsDevice;
            worldMatrix = Matrix.Identity;
            game.IsMouseVisible = false;
            player = new Player(worldMatrix, viewMatrix, projectionMatrix, _graphicsDevice, basicEffect);
        }

        public override void repositionComponents()
        {
        }

        public override void Dispose()
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont: _textFontBold, text: text.ToString(), position: new Vector2(100, 100), color: Colors.red);
            spriteBatch.DrawString(spriteFont: _textFontBold, text: _pressed.ToString(), position: new Vector2(100, 150), color: Colors.red);

            graphicsDevice.Clear(Color.Black);

            player.DrawCube(gameTime); // Cube and grid for testing purposes
        }

        public override void Update(GameTime gameTime, KeyboardState previousState, KeyboardState currentState)
        {
            if (KeyboardHandler.WasKeyPressedAndReleased(previousState, currentState, Keys.Escape))
            {
                _game.ChangeState(newState: new PauseState(_contentManager, _graphicsDevice, _game, _settingsModel));
                _game.IsMouseVisible = true;
            }

            if (KeyboardHandler.WasKeyPressedAndReleased(previousState, currentState, Keys.Space))
            {
                _pressed++;
            }

            player.UpdatePlayer(gameTime);
        }
    }
}
