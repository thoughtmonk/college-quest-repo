using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

// TODO: replace this with the type you want to write out.
using TWrite = LevelLibrary.Level;

namespace LevelContentPipelineExtension
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    /// 
    /// Writes the level object into an XNA filetype
    /// </summary>
    [ContentTypeWriter]
    public class LevelWriter : ContentTypeWriter<TWrite>
    {
        protected override void Write(ContentWriter output, TWrite value)
        {
            output.Write(value.Rows);
            output.Write(value.Columns);
            output.Write(value.Floors);
            for (int floor = 0; floor < value.Floors; floor++)
            {
                for (int row = 0; row < value.Rows; row++)
                {
                    for (int column = 0; column < value.Columns; column++)
                    {
                        LevelLibrary.Block block = value.GetBlock(row, column, floor);
                        output.Write(block.BaseTile);
                        output.Write(block.HeightTile);
                        output.Write(block.TopperTile);
                    }
                }
            }
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            // TODO: change this to the name of your ContentTypeReader
            // class which will be used to load this data.
            return "LevelLibrary.LevelReader, LevelLibrary";
        }
    }
}
