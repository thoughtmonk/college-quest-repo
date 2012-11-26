using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstGame
{
    class MapCell
    {   
        // List of tiles on the lowest levels!
        public List<int> BaseTiles = new List<int>();

        // List of tiles with height!
        public List<int> HeightTiles = new List<int>();

        // Tiles that go on-top of tiles that don't add to height!
        public List<int> TopperTiles = new List<int>();


        public bool Walkable{ get; set; }

        public int TileID
        {
            get { return BaseTiles.Count > 0 ? BaseTiles[0] : 0; }
            set
            {
                if (BaseTiles.Count > 0)
                    BaseTiles[0] = value;
                else
                    AddBaseTile(value);
            }
        }
        
        // Set Tile ID
        public MapCell(int tileID)
        {
            TileID = tileID;
            Walkable = true;
        }

        // Add a new base tile!
        public void AddBaseTile(int tileID)
        {
            BaseTiles.Add(tileID);
        }

        // Add a new height tile!
        public void AddHeightTile(int tileID)
        {
            HeightTiles.Add(tileID);
        }

        // Add a new topper tile!
        public void AddTopperTile(int tileID)
        {
            TopperTiles.Add(tileID);
        }
    }
}
