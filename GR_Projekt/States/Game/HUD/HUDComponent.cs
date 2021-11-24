using System.Collections.Generic;
using GR_Projekt.Content.Fonts;
using GR_Projekt.Content.Images;
using GR_Projekt.Core;
using GR_Projekt.States.Game.HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.States.Game
{
    public class HUDComponent : Component
    {
        private Rectangle _hudRectangle;
        private Texture2D _hudBackgroundTexture;

        private SpriteFont _spriteFont;
        //TODO get player data
        private Player _player;

        //TODO remove mock
        private string _playerHealth;
        private string _playerAmmo;
        private string _playerArmour;
        private string _playerScore;

        private List<HUDCell> _hudCells;


        public HUDComponent(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager)
        {
            int hudXPosition = 0;
            int hudWidth = graphicsDeviceManager.GraphicsDevice.Viewport.Width;
            int hudYPosition = (int)(graphicsDeviceManager.GraphicsDevice.Viewport.Height * 0.8);
            int hudHeight = (int)(graphicsDeviceManager.GraphicsDevice.Viewport.Height * 0.2);

            this._spriteFont = contentManager.Load<SpriteFont>(Fonts.Copperplate(fontSize: 16));
            this._hudCells = new List<HUDCell>();

            this._hudBackgroundTexture = contentManager.Load<Texture2D>(GeneralImages.backgroundImage);
            this._hudRectangle = new Rectangle(x: hudXPosition, y: hudYPosition, width: hudWidth, height: hudHeight);

            float cellWidth = graphicsDeviceManager.GraphicsDevice.Viewport.Width / 5;


            _hudCells.Add(new HUDTextCell(_spriteFont, "Armour", "100", new Rectangle(hudXPosition, _hudRectangle.Y, (int)cellWidth, hudHeight)));
            _hudCells.Add(new HUDTextCell(_spriteFont, "Health", "100", new Rectangle((int)(hudXPosition + cellWidth), _hudRectangle.Y, (int)cellWidth, hudHeight)));
            _hudCells.Add(new HUDDoomFaceCell(contentManager, new Rectangle((int)(hudXPosition + cellWidth * 2), _hudRectangle.Y, (int)cellWidth, hudHeight)));
            _hudCells.Add(new HUDTextCell(_spriteFont, "Points", "100", new Rectangle((int)(hudXPosition + cellWidth * 3), _hudRectangle.Y, (int)cellWidth, hudHeight)));
            _hudCells.Add(new HUDTextCell(_spriteFont, "Ammo", "100", new Rectangle((int)(hudXPosition + cellWidth * 4), _hudRectangle.Y, (int)cellWidth, hudHeight)));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_hudBackgroundTexture, _hudRectangle, Colors.defaultDrawColor);

            foreach (HUDCell hudCell in _hudCells)
            {
                hudCell.Draw(gameTime, spriteBatch);
            }

        }

        public override void Update(GameTime gameTime)
        {
            _hudCells[0].Update(gameTime.TotalGameTime.TotalSeconds.ToString());
            _hudCells[1].Update((-gameTime.TotalGameTime.TotalSeconds).ToString());
            _hudCells[2].Update((-gameTime.TotalGameTime.TotalSeconds).ToString());
            _hudCells[3].Update(gameTime.TotalGameTime.TotalSeconds.ToString());
            _hudCells[4].Update((-gameTime.TotalGameTime.TotalSeconds).ToString());
        }
    }
}
