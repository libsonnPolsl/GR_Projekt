using GR_Projekt.Content.Fonts;
using GR_Projekt.Core;
using GR_Projekt.States.Game;
using GR_Projekt.States.Game.Enemies;
using GR_Projekt.States.Settings.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace GR_Projekt.States
{
    public class GameState : State
    {
        private Player player;
        private Map map;
        private Guard guard;
        private General general;
        private Doctor doctor;

        private Matrix worldMatrix, viewMatrix, projectionMatrix;
        private GraphicsDevice graphicsDevice;
        private BasicEffect basicEffect;
        private HUDComponent _hud;

        public GameState(ContentManager content, GraphicsDevice graphicsDevice, Game1 game, SettingsModel settingsModel) : base(content, graphicsDevice, game, settingsModel, StateTypeEnumeration.Game)
        {

            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;
            basicEffect.Alpha = 1.0f;
            this.graphicsDevice = graphicsDevice;
            game.IsMouseVisible = false;
            map = new Map(content, graphicsDevice, game, settingsModel);

            player = new Player(ref worldMatrix, ref viewMatrix, ref projectionMatrix, _graphicsDevice, basicEffect, content, map);
            this._hud = new HUDComponent(content, _game.getGraphicsDeviceManager, player);
            guard = new Guard(settingsModel, graphicsDevice, content, game, new Vector2(x: 1000.0f, y: 0.0f), new Vector2(0, 0), new Vector2(1.0f, 0.0f), 10, 100);
            general = new General(settingsModel, graphicsDevice, content, game, new Vector2(x: 900.0f, y: 0.0f), new Vector2(0, 0), new Vector2(1.0f, 0.0f), 10, 100, map);
            doctor = new Doctor(settingsModel, graphicsDevice, content, game, new Vector2(x: 800.0f, y: 1.0f), new Vector2(0, 0), new Vector2(1.0f, 0.0f), 10, 100);

            
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
            map.Draw(gameTime, spriteBatch);

            _hud.Draw(gameTime, spriteBatch);
            //player.DrawCube(gameTime); // Cube and grid for testing purposes
            player.Draw(gameTime, spriteBatch);
            //player.DrawCube(gameTime); // Cube and grid for testing purposes

            //player.DrawCube(gameTime); // Cube and grid for testing purposes

            guard.Draw(gameTime, spriteBatch);
            general.Draw(gameTime, spriteBatch);
            doctor.Draw(gameTime, spriteBatch);

            _hud.Draw(gameTime, spriteBatch);

            Debug.WriteLine("Player camPosition: " + player.camPosition);
            Debug.WriteLine("Player camTarget: " + player.camTarget);
        }

        public override void Update(GameTime gameTime, KeyboardState previousState, KeyboardState currentState)
        {

            if (KeyboardHandler.WasKeyPressedAndReleased(previousState, currentState, Keys.Escape))
            {
                _game.ChangeState(newState: new PauseState(_contentManager, _graphicsDevice, _game, _settingsModel));
                _game.IsMouseVisible = true;
            }

            _hud.Update(gameTime);
            player.Update(gameTime);
            map.updateCamera(player.camPosition, player.camTarget, player.angleX);

            guard.Update(gameTime);
            guard.updateCamera(player.camPosition, player.camTarget);
            
            general.Update(gameTime);
            general.updateCamera(player.camPosition, player.camTarget);
            
            doctor.Update(gameTime);
            doctor.updateCamera(player.camPosition, player.camTarget);

            //if ((general.currentRectangle.Contains(crosshair.getCrosshairRectangle)))
            //{
            //    Debug.WriteLine(crosshair.getCrosshairRectangle);
            //    this.resistance--;
            //}

        }

    }
}
