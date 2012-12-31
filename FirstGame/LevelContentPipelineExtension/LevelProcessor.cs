using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

// TODO: replace these with the processor input and output types.
using TInput = System.String;
using TOutput = LevelLibrary.Level;

namespace LevelContentPipelineExtension
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// Processes the imported level data and creates a new level object
    /// </summary>
    [ContentProcessor(DisplayName = "Level Processor")]
    public class LevelProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            // TODO: process the input object, and return the modified data.
            string[] lines = input.Split(new char[] { '\n' });
            int rows = Convert.ToInt32(lines[0]);
            int columns = Convert.ToInt32(lines[1]);
            int floors = Convert.ToInt32(lines[2]);

            LevelLibrary.Block[, ,] levelData = new LevelLibrary.Block[rows, columns, floors];
            for (int floor = 0; floor < floors; floor++)
            {
                for (int row = 0; row < rows; row++)
                {
                    string[] blocks = lines[floor * rows + row + 3].Split(new char[] { ' ' });
                    for (int column = 0; column < columns; column++)
                    {
                        string[] attributes = blocks[column].Split(new char[] { ':' });
                        int baseTile = Convert.ToInt32(attributes[0]);
                        int heightTile = Convert.ToInt32(attributes[1]);
                        int topperTile = Convert.ToInt32(attributes[2]);
                        levelData[row, column, floor] = new LevelLibrary.Block(baseTile, heightTile, topperTile);
                    }
                }
            }
            return new LevelLibrary.Level(levelData);
        }
    }
}