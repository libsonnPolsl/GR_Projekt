using GR_Projekt.Content.Fonts;
using GR_Projekt.Core;
using GR_Projekt.States.Game;
using GR_Projekt.States.Settings.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GR_Projekt.States
{
    public class GameState : State
    {

        private int _pressed = 0;

        private Player player;
        private Matrix worldMatrix, viewMatrix, projectionMatrix;
        private GraphicsDevice graphicsDevice;
        private BasicEffect basicEffect;
        private HUD _hud;

        public GameState(ContentManager content, GraphicsDevice graphicsDevice, Game1 game, SettingsModel settingsModel) : base(content, graphicsDevice, game, settingsModel, StateTypeEnumeration.Game)
        {
            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;
            basicEffect.Alpha = 1.0f;
            this.graphicsDevice = graphicsDevice;
            worldMatrix = Matrix.Identity;
            game.IsMouseVisible = false;
            player = new Player(worldMatrix, viewMatrix, projectionMatrix, _graphicsDevice, basicEffect);
            this._hud = new HUD(content, _game.getGraphicsDeviceManager);
        }

        public override void repositionComponents()
        {
        }

        public override void Dispose()
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);
            _hud.Draw(gameTime, spriteBatch);


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

            _hud.Update(gameTime);
            player.UpdatePlayer(gameTime);
        }
    }
}
