using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame
{
    static class Tile
    {
        static public Texture2D TileSetTexture;

        // Variables to change depending on tileset
        // Tile sprite size
        static public int TileWidth = 64;
        static public int TileHeight = 64;

        // Tile sprite visible size (for isomorphic use)
        static public int TileStepX = 64;
        static public int TileStepY = 16;

        // Tile sprite depth (for voxel use)
        static public int TileDepth = 10;

        // Offset for every other row (for isomorphic use)
        static public int OddRowXOffset = 32;

        //
        static public int HeightTileOffset = 32;

        //end Tile configuration

        // Move through the Sprite sheet to get the necessary sprite
        static public Rectangle GetSourceRectangle(int tileIndex)
        {
            int tileY = tileIndex / (TileSetTexture.Width / TileWidth);
            int tileX = tileIndex % (TileSetTexture.Width / TileWidth);

            return new Rectangle(tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight);
        }
    }
}
