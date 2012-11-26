using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace FirstGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TileMap myMap;
        int squaresAcross = 17;
        int squaresDown = 37;

        public int baseOffsetX = -32;
        public int baseOffsetY = -64;
        float HeightRowDepthMod = 0.0000001f;

        SpriteFont pericles6;

        Texture2D hilight;

        SpriteAnimation vlad;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Load sprite sheet into Content Pipeline
            Tile.TileSetTexture = Content.Load<Texture2D>(@"Tilesets\part4_tileset");

            //Load a font into the Content Pipeline
            pericles6 = Content.Load<SpriteFont>(@"Fonts\Pericles6");           
        
            //Load a sprite for mouse selection into the content pipeline
            myMap = new TileMap(Content.Load<Texture2D>(@"Tilesets\mousemap"));

            //Set Camera settings so we can see and move around in the world.
            Camera.ViewWidth = this.graphics.PreferredBackBufferWidth;
            Camera.ViewHeight = this.graphics.PreferredBackBufferHeight;
            Camera.WorldWidth = ((myMap.MapWidth - 2) * Tile.TileStepX);
            Camera.WorldHeight = ((myMap.MapHeight - 2) * Tile.TileStepY);
            Camera.DisplayOffset = new Vector2(baseOffsetX, baseOffsetY);

            //Load highlighted tile texture
            hilight = Content.Load<Texture2D>(@"Tilesets/hilight");
            
            // Load Vlad, the Last True Sprite King
            vlad = new SpriteAnimation(Content.Load<Texture2D>(@"Characters\T_Vlad_Sword_Walking_48x48"));
        
            // Give Vlad Walking Animations, marking the place in the sprite sheet where they are.
            vlad.AddAnimation("WalkEast", 0, 48*0, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkNorth", 0, 48*1, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkNorthEast", 0, 48*2, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkNorthWest", 0, 48*3, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkSouth", 0, 48*4, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkSouthEast", 0, 48*5, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkSouthWest", 0, 48*6, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkWest", 0, 48*7, 48, 48, 8, 0.1f);

            // Give Vlad Idle Animations, so it doesn't look like he is walking when he is standing there.
            vlad.AddAnimation("IdleEast", 0, 48*0, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleNorth", 0, 48*1, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleNorthEast", 0, 48*2, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleNorthWest", 0, 48*3, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleSouth", 0, 48*4, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleSouthEast", 0, 48*5, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleSouthWest", 0, 48*6, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleWest", 0, 48*7, 48, 48, 1, 0.2f);

            // Set Vlad's starting configuration. All this will need to be encapusulated into a file I/O class later on. Then you can
            // pull animations from an xml file somewhere in the heiriarchy.
            vlad.Position = new Vector2(100, 100);
            vlad.DrawOffset = new Vector2(-50, -100);
            vlad.CurrentAnimation = "WalkEast";
            vlad.IsAnimating = true;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            Vector2 moveVector = Vector2.Zero;
            Vector2 moveDir = Vector2.Zero;
            string animation = "";
            int X = 0;
            int Y = 0;
            
            // Grab Keyboard state
            KeyboardState ks = Keyboard.GetState();
            
            // Check keydown states
            if (ks.IsKeyDown(Keys.Left))
            {
                X -= 2;
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                X += 2;
            }
            if (ks.IsKeyDown(Keys.Up))
            {
                Y -= 1;
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                Y += 1;
            }

            moveDir = new Vector2(X, Y);

            if (myMap.GetCellAtWorldPoint(vlad.Position + moveDir).Walkable == false)
            {
                moveDir = Vector2.Zero;
            }
            //Trace.WriteLine("Current Tile: " + myMap.GetCellAtWorldPoint(vlad.Position));

            if(moveDir.X > 0 && moveDir.Y > 0)
                animation = "WalkSouthEast";
            if(moveDir.X < 0 && moveDir.Y > 0)
                animation = "WalkSouthWest";
            if(moveDir.X > 0 && moveDir.Y < 0)
                animation = "WalkNorthEast";
            if(moveDir.X < 0 && moveDir.Y < 0)
                animation = "WalkNorthWest";
            if(moveDir.X > 0 && moveDir.Y == 0)
                animation = "WalkEast";
            if(moveDir.X < 0 && moveDir.Y == 0)
                animation = "WalkWest";
            if(moveDir.X == 0 && moveDir.Y > 0)
                animation = "WalkSouth";
            if(moveDir.X == 0 && moveDir.Y < 0)
                animation = "WalkNorth";

            // Set moving animation if moving, otherwise just stay there.
            if (moveDir.Length() != 0)
            {
                vlad.MoveBy((int)moveDir.X, (int)moveDir.Y);
                if(vlad.CurrentAnimation!=animation)
                    vlad.CurrentAnimation=animation;
            }
            else
            {
                vlad.CurrentAnimation = "Idle" + vlad.CurrentAnimation.Substring(4);
            }

            vlad.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Vector2 firstSquare = new Vector2(Camera.Location.X / Tile.TileStepX, Camera.Location.Y / Tile.TileStepY);
            int firstX = (int)firstSquare.X;
            int firstY = (int)firstSquare.Y;

            Vector2 squareOffset = new Vector2(Camera.Location.X % Tile.TileStepX, Camera.Location.Y % Tile.TileStepY);
            int offsetX = (int)squareOffset.X;
            int offsetY = (int)squareOffset.Y;

            float maxdepth = ((myMap.MapWidth + 1) + ((myMap.MapHeight + 1) * Tile.TileWidth)) * 10;
            float depthOffset;


            for (int y = 0; y < squaresDown; y++)
            {
                int rowOffset = 0;
                if ((firstY + y) % 2 == 1)
                    rowOffset = Tile.OddRowXOffset;
                for (int x = 0; x < squaresAcross; x++)
                {
                    int mapx = (firstX + x);
                    int mapy = (firstY + y);
                    depthOffset = 0.7f - ((mapx + (mapy * Tile.TileWidth)) / maxdepth);

                    if ((mapx >= myMap.MapWidth) || (mapy >= myMap.MapHeight))
                        continue;

                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].BaseTiles)
                    {

                     
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2((mapx * Tile.TileStepX) + rowOffset, mapy * Tile.TileStepY)),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            1.0f
                            ); 
                        
                    }

                    int heightRow = 0;

                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].HeightTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2(
                                    (mapx * Tile.TileStepX) + rowOffset,
                                    mapy * Tile.TileStepY - (heightRow * Tile.HeightTileOffset))),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            depthOffset - ((float)heightRow * HeightRowDepthMod)
                            );
                        heightRow++;
                    }

                    foreach (int tileID in myMap.Rows[y + firstY].Columns[x + firstX].TopperTiles)
                    {

                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2((mapx * Tile.TileStepX) + rowOffset, mapy * Tile.TileStepY)),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            depthOffset - ((float)heightRow * HeightRowDepthMod)
                            );

                    }
                    /*
                    spriteBatch.DrawString(pericles6, (x + firstX).ToString() + ", " + (y + firstY).ToString(),
                        new Vector2((x * Tile.TileStepX) - offsetX + rowOffset + baseOffsetX + 24,
                            (y * Tile.TileStepY) - offsetY + baseOffsetY + 48), Color.White, 0f, Vector2.Zero,
                            1.0f, SpriteEffects.None, 0.0f);*/
                }
            }

            Vector2 hilightLoc = Camera.ScreenToWorld(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            Point hilightPoint = myMap.WorldToMapCell(new Point((int)hilightLoc.X, (int)hilightLoc.Y));

            int hilightrowOffset = 0;
            if ((hilightPoint.Y) % 2 == 1)
                hilightrowOffset = Tile.OddRowXOffset;

            vlad.Draw(spriteBatch, 0, 0);

            spriteBatch.Draw(hilight, Camera.WorldToScreen(
                new Vector2((hilightPoint.X * Tile.TileStepX) + hilightrowOffset,
                (hilightPoint.Y + 2) * Tile.TileStepY)),
            new Rectangle(0, 0, 64, 32),
            Color.White * 0.3f,
            0.0f,
            Vector2.Zero,
            1.0f,
            SpriteEffects.None,
            0.0f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
