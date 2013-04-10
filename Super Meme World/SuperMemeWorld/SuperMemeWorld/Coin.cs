using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMemeWorld
{
    /// <summary>
    /// This is a game component that implements the coin updating and information
    /// </summary>
    class Coin : Sprite
    {
        enum State
        {
            Spinning
        }

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        int theMaxFramerate = 4;

        public void LoadContent(ContentManager theContentManager, int startX, int startY, String theAssetName)
        {
            Position = new Vector2(startX, startY);
            base.LoadContent(theContentManager, theAssetName, theMaxFramerate);
            Scale = 4;
        }

        public void Update(GameTime theGameTime)
        {
            mSpeed.X = 0;
            mDirection.X = 1;
            base.Update(theGameTime, mSpeed, mDirection);
        }
    }
}
