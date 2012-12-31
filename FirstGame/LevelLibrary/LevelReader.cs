using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using TRead = LevelLibrary.Level;

namespace LevelLibrary
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content
    /// Pipeline to read the specified data type from binary .xnb format.
    /// 
    /// Unlike the other Content Pipeline support classes, this should
    /// be a part of your main game project, and not the Content Pipeline
    /// Extension Library project.
    /// 
    /// Reads the level XNA file type (not part of pipeline)
    /// </summary>
    public class LevelReader : ContentTypeReader<TRead>
    {
        protected override TRead Read(ContentReader input, TRead existingInstance)
        {
            int rows = input.ReadInt32();
            int columns = input.ReadInt32();
            int floors = input.ReadInt32();

            LevelLibrary.Block[, ,] levelData = new LevelLibrary.Block[rows, columns, floors];

            for (int floor = 0; floor < floors; floor++)
            {
                for (int row = 0; row < rows; row++)
                {
                    for (int column = 0; column < columns; column++)
                    {
                        int baseTile = input.ReadInt32();
                        int heightTile = input.ReadInt32();
                        int topperTile = input.ReadInt32();
                        levelData[row, column, floor] = new LevelLibrary.Block(baseTile, heightTile, topperTile);
                    }
                }
            }

            return new Level(levelData);
        }
    }
}
