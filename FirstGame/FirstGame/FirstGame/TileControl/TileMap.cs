using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame
{
    class TileMap
    {
        public List<MapRow> Rows = new List<MapRow>();

        // Total map size in tiles
        public int MapWidth;
        public int MapHeight;

        private Texture2D mouseMap;
 
        // Convert a given point that the mouse is hovering over into the tile it is on.
        public Point WorldToMapCell(Point worldPoint, out Point localPoint)
        {
            Point mapCell = new Point(
                (int)(worldPoint.X / mouseMap.Width),
                ((int)(worldPoint.Y / mouseMap.Height)) * 2
                );

            int localPointX = worldPoint.X % mouseMap.Width;
            int localPointY = worldPoint.Y % mouseMap.Height;

            int dx = 0;
            int dy = 0;

            uint[] myUint = new uint[1];

            if (new Rectangle(0, 0, mouseMap.Width, mouseMap.Height).Contains(localPointX, localPointY))
            {
                mouseMap.GetData(0, new Rectangle(localPointX, localPointY, 1, 1), myUint, 0, 1);

                if (myUint[0] == 0xFF0000FF)
                {
                    dx = -1;
                    dy = -1;
                    localPointX = localPointX + (mouseMap.Width / 2);
                    localPointY = localPointY + (mouseMap.Height / 2);
                }
                if (myUint[0] == 0xFF00FF00)
                {
                    dx = -1;
                    localPointX = localPointX + (mouseMap.Width / 2);
                    dy = 1;
                    localPointY = localPointY - (mouseMap.Height / 2);
                }
                if (myUint[0] == 0xFF00FFFF)
                {
                    dy = -1;
                    localPointX = localPointX - (mouseMap.Width / 2);
                    localPointY = localPointY + (mouseMap.Height / 2);
                }
                if (myUint[0] == 0xFFFF0000)
                {

                    dy = +1;
                    localPointX = localPointX - (mouseMap.Width / 2);
                    localPointY = localPointY - (mouseMap.Height / 2);
                }
            }

            mapCell.X += dx;
            mapCell.Y += dy - 2;

            localPoint = new Point(localPointX, localPointY);

            Trace.WriteLine("X Position: " + mapCell.X);
            Trace.WriteLine("Y Position: " + mapCell.Y);

            return mapCell;
        }

        // Function mask if you don't need the local coordinates
        public Point WorldToMapCell(Point worldPoint)
        {
            Point dummy;
            return WorldToMapCell(worldPoint, out dummy);
        }

        /* Create map 
         * To do:
         * Create a way to load in maps from outside the function
         * Random maps 
         * As you can tell, it is currently hardcoded!
         */
        public TileMap(Texture2D mouseMap, LevelLibrary.Level level)
        {
            this.mouseMap = mouseMap;

            // Find Width and height from level file
            MapWidth = level.Rows;
            MapHeight = level.Columns;


            for (int y = 0; y < MapHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    thisRow.Columns.Add(new MapCell(0));
                }
                Rows.Add(thisRow);
            }

            // Render level by setting the base, height, and topper tile of each tile 
            // to being those defined by the appropriate block in the level object
            for (int row = 0; row < level.Rows; row++)
            {
                for (int column = 0; column < level.Columns; column++)
                {
                    LevelLibrary.Block block = level.GetBlock(row, column, 0);
                    Rows[row].Columns[column].AddBaseTile(block.BaseTile);
                    Rows[row].Columns[column].AddHeightTile(block.HeightTile);
                    Rows[row].Columns[column].AddTopperTile(block.TopperTile);
                }
            }

        }

        public Point WorldToMapCell(Vector2 worldPoint)
        {
            return WorldToMapCell(new Point((int)worldPoint.X, (int)worldPoint.Y));
        }

        public MapCell GetCellAtWorldPoint(Point worldPoint)
        {
            Point mapPoint = WorldToMapCell(worldPoint);
            return Rows[mapPoint.Y].Columns[mapPoint.X];
        }

        public MapCell GetCellAtWorldPoint(Vector2 worldPoint) 
        {
            return GetCellAtWorldPoint(new Point((int)worldPoint.X, (int)worldPoint.Y));
        }
    }
    
    // Initialize maps list!
    class MapRow
    {
        public List<MapCell> Columns = new List<MapCell>();
    }
}
