using GR_Projekt.Content.Images;
using GR_Projekt.Content.Images.Controls;
using GR_Projekt.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.States.Game.HUD
{
    public class HUDDoomFaceCell : HUDCell
    {
        private ImageComponent _doomFace;

        public HUDDoomFaceCell(ContentManager contentManager, Rectangle cell) : base(cell: cell)
        {

            this._doomFace = new ImageComponent(contentManager, MainMenuImages.menuDoomFace, cell.Center.ToVector2(), cell.Width / 2 - Paddings.componentHorizontalPadding * 2, cell.Height / 1 - Paddings.componentHorizontalPadding * 2);
        }

        public override void Update(string updatedValue)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _doomFace.Draw(gameTime, spriteBatch);
        }
    }
}
