﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Leap_of_Faith
{
    //Partial World Class. This acts as the manager for the platforms and anything else that shows up on screen
    partial class World
    {
        //The platforms that the world controls
        private List<Platform> platforms = new List<Platform>();
        //Graphics Device Manager
        private GraphicsDeviceManager graphics; 

        //Add a platform to the list
        public void addPlatform(Rectangle bounds, Texture2D texture)
        {
            platforms.Add(new Platform(bounds, texture));
        }

        //Move a platform by x Distance
        public void movePlatforms(int distX)
        {
            //Loop through the platforms
        
                foreach (Platform p in platforms)
                {
                    //Move each platform to the left by disX
                    p.Bounds = new Rectangle(p.Bounds.X - distX, p.Bounds.Y, p.Bounds.Width, p.Bounds.Height);
                    //If the platform is off the screen, generate a new one. 
                    if (p.Bounds.X + p.Bounds.Width <= 0)
                    {
                       
                        regenPlatform(p);
                        break;
                    }
                }
           
        }

        //Function that generates a new platform
        private void regenPlatform(Platform p)
        {
            //Remove the platform that is off the screen
            platforms.Remove(p);
            //Get the platform from the end of the list
            Platform temp = platforms[platforms.Count - 1];
            //New random number generator
            Random rand = new Random();
            //This is the amount of Y distance that you will add between the platform currently being generated
            //and the one before it. 
            int yDist = rand.Next(-200, 100 + p.Bounds.Height);
            //The x Location of the new platform
            //This should generate a platform 100 - 200 pixels behind the rightmost edge of the previous platform
            int xVal = temp.Bounds.X + temp.Bounds.Width + rand.Next(100, 200); 
            //Where the platform will be vertically.
            int yVal = temp.Bounds.Y + yDist;

            //If the platform will be off the screen, move it to the bottom of the screen.
            if (yVal >= graphics.PreferredBackBufferHeight - temp.Bounds.Height || yVal <= 0)
            {
                yVal = graphics.PreferredBackBufferHeight - temp.Bounds.Height - 15;
            }
          
            //Set the bounds of the new platform
            p.Bounds = new Rectangle(xVal, yVal, rand.Next(150, 350), 25);
            //Add the new platform to the screen
            platforms.Add(p);
        }

        //Return the platforms
        public List<Platform> getPlatforms()
        {
            return platforms;
        }
    }

    class Platform
    {
        //Bounds and Texture of the Platform
        private Rectangle bounds;
        private Texture2D texture;

        //Public Properties representing the Texture and Bounds of the Platform
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Rectangle Bounds
        {
            get{return bounds;}
            set { bounds = value; }
        }

        //Constructor
        public Platform(Rectangle rect, Texture2D tex)
        {
            bounds = rect;
            texture = tex; 
        }
        //Empty Constructor.
        public Platform()
        {
            bounds = new Rectangle(0, 0, 10, 10);
        }

        
    }
}
