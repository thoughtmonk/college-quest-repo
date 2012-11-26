using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace FirstGame
{
    static class Camera
    {

        // Location of the camera
        static public Vector2 location;

        // Location setup
        static public Vector2 Location
        {
            get
            {
                return location;
            }
            set
            {
                // Set the location and make sure it is within the world parameters
                location = new Vector2(
                    MathHelper.Clamp(value.X, 0f, WorldWidth - ViewWidth),
                    MathHelper.Clamp(value.Y, 0f, WorldHeight - ViewHeight));
            }
        }
        //static public Vector2 Location = new Vector2(32, 0);

        // Simple getter and setters for world settings.
        public static int ViewWidth { get; set; }
        public static int ViewHeight { get; set; }
        public static int WorldWidth { get; set; }
        public static int WorldHeight { get; set; }

        // 
        public static Vector2 DisplayOffset { get; set; }

        // Convert the current world to the screen position
        public static Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return worldPosition - Location + DisplayOffset;
        }

        // Convert current screen position to the world position
        public static Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return screenPosition + Location - DisplayOffset;
        }

        // Move the camera by a vector
        public static void Move(Vector2 offset)
        {
            Location += offset;
        }
    }
}
