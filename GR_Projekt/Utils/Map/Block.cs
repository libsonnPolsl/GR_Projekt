using GR_Projekt.Utils.Map;
using System.Collections.Generic;

namespace GR_Projekt.Utils.Map
{
    public class Block
    {
        public BlockType blockType;
        public List<bool> walls;
        public Block(BlockType blockType, List<bool> walls)
        {
            this.blockType = blockType;
            this.walls = walls;
        }
    }
}
