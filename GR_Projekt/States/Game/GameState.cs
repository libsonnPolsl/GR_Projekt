using GR_Projekt.Content.Fonts;
using GR_Projekt.Core;
using GR_Projekt.States.Game;
using GR_Projekt.States.Game;
using GR_Projekt.States.Game.Enemies;
using GR_Projekt.States.Settings.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace GR_Projekt.States
{
    public class GameState : State
    {
        private Player player;
        private Map map;
        private Guard guard;
        private General general;

        private Dialogue dialogue;
        private Matrix worldMatrix, viewMatrix, projectionMatrix;
        private GraphicsDevice graphicsDevice;
        private BasicEffect basicEffect;
        private HUDComponent _hud;
        private Point collisionWithMap;

        public GameState(ContentManager content, GraphicsDevice graphicsDevice, Game1 game, SettingsModel settingsModel) : base(content, graphicsDevice, game, settingsModel, StateTypeEnumeration.Game)
        {
            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;
            basicEffect.Alpha = 1.0f;
            this.graphicsDevice = graphicsDevice;
            game.IsMouseVisible = false;
            player = new Player(ref worldMatrix, ref viewMatrix, ref projectionMatrix, _graphicsDevice, basicEffect, content);
            map = new Map(content, graphicsDevice, game, settingsModel);
            this._hud = new HUDComponent(content, _game.getGraphicsDeviceManager);
            dialogue = new Dialogue(graphicsDevice, basicEffect, content, _game.getGraphicsDeviceManager);
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
            player.RenderWeapon(gameTime, spriteBatch);
            
            dialogue.DrawDialogue(gameTime, spriteBatch);
            //player.DrawCube(gameTime); // Cube and grid for testing purposes
            _hud.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime, KeyboardState previousState, KeyboardState currentState)
        {
            if (KeyboardHandler.WasKeyPressedAndReleased(previousState, currentState, Keys.Escape))
            {
                _game.ChangeState(newState: new PauseState(_contentManager, _graphicsDevice, _game, _settingsModel));
                _game.IsMouseVisible = true;
            }

            _hud.Update(gameTime);
            player.UpdatePlayer(gameTime);
            map.updateCamera(player.camPosition, player.camTarget);
            dialogue.UpdateDialogue(gameTime, player.camPosition, player.camTarget);
        }
    }
}
