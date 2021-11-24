

using GR_Projekt.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GR_Projekt.States.Game
{
    abstract public class HUDCell
    {
        protected Rectangle _cellRectangle;

        public HUDCell(Rectangle cell)
        {
            this._cellRectangle = cell;
        }

        abstract public void Update(string updatedValue);
        abstract public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
