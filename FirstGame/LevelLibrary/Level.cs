using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelLibrary
{
    
    public class Level
    {
        private Block[, ,] Blocks { get; set; }

        /// <summary>
        /// This class will control the level objects created by .level files
        /// The level object is a 3D array of Blocks each of which define a base, height, and topper tile
        /// Instantuared with a 3D array of Blocks
        /// </summary>
        public Level(Block[, ,] blocks)
        {
            Blocks = blocks;
        }

        /// <summary>
        /// Returns the block at a set point (row, column, floor)
        /// </summary>
        public Block GetBlock(int row, int column, int floor)
        {
            return Blocks[row, column, floor];
        }

        /// <summary>
        /// Sets the block at a set point (row, column, floor) to be a new block object
        /// </summary>
        public void SetBlock(int row, int column, int floor, Block block)
        {
            Blocks[row, column, floor] = block;
        }

        /// <summary>
        /// Returns the number of rows
        /// </summary>
        public int Rows
        {
            get
            {
                return Blocks.GetLength(0);
            }
        }

        /// <summary>
        /// Return the number of columns
        /// </summary>
        public int Columns
        {
            get
            {
                return Blocks.GetLength(1);
            }
        }

        /// <summary>
        /// Return the number of floors
        /// </summary>
        public int Floors
        {
            get
            {
                return Blocks.GetLength(2);
            }
        }

    }
}
