using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMemeWorld
{
    class FlagSprite
    {
        //The asset name for the Sprite's Texture
        public string AssetName;

        //The size of the Sprite
        public Rectangle Size;

        //Used to size the Sprite up or down from the original image
        public float Scale = 1.0f;

        //The current position of the Sprite
        public Vector2 Position = Vector2.Zero;

        //The direction of the sprite
        SpriteEffects orientation = new SpriteEffects();

        //The texture object used when drawing the sprite
        private Texture2D mSpriteTexture;

        //Framerate stuff
        public int framerate = 0;
        public int maxFramerate;

        //The size of the sprite sheet
        Vector2 sheetSize;

        Point frameSize = new Point(32, 96);
        Point currentFrame = new Point(0, 0);

        //Boolean to allow frame update
        bool frameUpdate = true;

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager, string theAssetName, int theMaxFramerate)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            AssetName = theAssetName;
            maxFramerate = theMaxFramerate;
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            sheetSize.X = (mSpriteTexture.Width / 32);
            sheetSize.Y = (mSpriteTexture.Height / 96);
            orientation = SpriteEffects.FlipHorizontally;
        }

        //Get currentFrame
        public Point getCurrentFrame()
        {
            return currentFrame;
        }

        //Set currentFrame
        public void setCurrentFrame(Point theCurrentFrame)
        {
            currentFrame = theCurrentFrame;
        }

        //Get frameUpdate true/false
        public bool getFrameUpdate()
        {
            return frameUpdate;
        }

        //Set frameUpdate true/false
        public void setFrameUpdate(bool theFrameUpdate)
        {
            frameUpdate = theFrameUpdate;
        }

        //Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        public void Update(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection)
        {
            Position += theDirection * theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;

            if (theDirection.X == 0 && theDirection.Y == 0)
            {   // standing still, go to idle sprite
                currentFrame = new Point(0, 0);
            }

            else if (theDirection.X == 1)
            {   //moving right
                orientation = SpriteEffects.FlipHorizontally;
                currentFrame = FrameUpdate(currentFrame);
            }
            else if (theDirection.X == -1)
            {   //if moving left
                orientation = SpriteEffects.None;
                currentFrame = FrameUpdate(currentFrame);
            }
        }

        //Update current frame
        public Point FrameUpdate(Point currentFrame)
        {
            if (frameUpdate == true)
            {
                framerate++;
                if (framerate >= maxFramerate)
                {
                    ++currentFrame.X;
                    framerate = 0;
                    if (currentFrame.X >= sheetSize.X)
                    {
                        currentFrame.X = 7;
                    }
                }
                return currentFrame;
            }
            else
            {
                return currentFrame;
            }
        }


        //Draw the sprite to the screen
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, Position,
            new Rectangle(currentFrame.X * frameSize.X,
            currentFrame.Y * frameSize.Y,
            frameSize.X,
            frameSize.Y),
            Color.White, 0.0f, Vector2.Zero,
            Scale, orientation, 0);
        }
    }
}

