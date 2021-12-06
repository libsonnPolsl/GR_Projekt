using System.Collections.Generic;
using GR_Projekt.Content.Fonts;
using GR_Projekt.Content.Images;
using GR_Projekt.Core;
using GR_Projekt.States.Game.HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GR_Projekt.States.Game
{
    public class HUDComponent : Component
    {
        private Crosshair _crosshair;

        private Rectangle _hudRectangle;
        private Texture2D _hudBackgroundTexture;

        private SpriteFont _spriteFont;
        private Player _player;

        private List<HUDCell> _hudCells;


        public HUDComponent(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager)
        {
        }

        public HUDComponent(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager, Player player)
        {
            this._player = player;
            this._crosshair = new Crosshair(contentManager, new Rectangle(0, 0, graphicsDeviceManager.GraphicsDevice.Viewport.Width, (int)(graphicsDeviceManager.GraphicsDevice.Viewport.Height * 0.8)).Center);

            int hudXPosition = 0;
            int hudWidth = graphicsDeviceManager.GraphicsDevice.Viewport.Width;
            int hudYPosition = (int)(graphicsDeviceManager.GraphicsDevice.Viewport.Height * 0.8);
            int hudHeight = (int)(graphicsDeviceManager.GraphicsDevice.Viewport.Height * 0.2);

            this._spriteFont = contentManager.Load<SpriteFont>(Fonts.Copperplate(fontSize: 16));
            this._hudCells = new List<HUDCell>();

            this._hudBackgroundTexture = contentManager.Load<Texture2D>(GeneralImages.backgroundImage);
            this._hudRectangle = new Rectangle(x: hudXPosition, y: hudYPosition, width: hudWidth, height: hudHeight);

            float cellWidth = graphicsDeviceManager.GraphicsDevice.Viewport.Width / 5;


            _hudCells.Add(new HUDTextCell(_spriteFont, "Czas gry", 0 + "s", new Rectangle(hudXPosition, _hudRectangle.Y, (int)cellWidth, hudHeight)));
            _hudCells.Add(new HUDTextCell(_spriteFont, "Zdrowie", _player.getPlayerHealth.ToString(), new Rectangle((int)(hudXPosition + cellWidth), _hudRectangle.Y, (int)cellWidth, hudHeight)));
            _hudCells.Add(new HUDDoomFaceCell(contentManager, new Rectangle((int)(hudXPosition + cellWidth * 2), _hudRectangle.Y, (int)cellWidth, hudHeight)));
            _hudCells.Add(new HUDTextCell(_spriteFont, "Punkty", _player.getPlayerScore.ToString(), new Rectangle((int)(hudXPosition + cellWidth * 3), _hudRectangle.Y, (int)cellWidth, hudHeight)));
            _hudCells.Add(new HUDTextCell(_spriteFont, "Amunicja", _player.getInMagAmmo.ToString() + "/" + _player.getTotalAmmo.ToString(), new Rectangle((int)(hudXPosition + cellWidth * 4), _hudRectangle.Y, (int)cellWidth, hudHeight)));

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            _crosshair.Draw(gameTime, spriteBatch);

            spriteBatch.Draw(_hudBackgroundTexture, _hudRectangle, Colors.defaultDrawColor);

            foreach (HUDCell hudCell in _hudCells)
            {
                hudCell.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            _hudCells[0].Update(20.ToString() + "s");
            _hudCells[1].Update(_player.getPlayerHealth.ToString());
            _hudCells[3].Update(_player.getPlayerScore.ToString());
            _hudCells[4].Update(_player.getInMagAmmo.ToString() + "/" + _player.getTotalAmmo.ToString());
        }

        public void UpdateGameOfTime(int timeOfGame)
        {

        }

        public void ChangeCrosshairVisibility(bool visible)
        {
            _crosshair.ChangeVisibility(visible);
        }
    }
}
