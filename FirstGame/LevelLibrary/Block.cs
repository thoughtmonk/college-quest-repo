using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelLibrary
{
    public class Block
    {
        public int BaseTile { get; set; }
        public int HeightTile { get; set; }
        public int TopperTile { get; set; }

        /// <summary>
        /// This class controls the block object. Block object contains 3 attributes
        /// BaseTile   - int: defines the part of the tile that sits at the bottom of a block
        /// HeightTile - int: defines the part of tile that has height
        /// TopperTile - int: defines the part of tile that sits on top of a block
        /// </summary>
        public Block(int baseTile, int heightTile, int topperTile)
        {
            BaseTile = baseTile;
            HeightTile = heightTile;
            TopperTile = topperTile;
        }
    }
}
