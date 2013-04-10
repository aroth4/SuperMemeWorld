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
    /// This class implements the power up blocks in the game
    /// </summary>
    class Block : Sprite
    {
        Vector2 mDirection = new Vector2(0, 1);
        Vector2 mSpeed = Vector2.Zero;

        int theMaxFramerate = 0;

        public void LoadContent(ContentManager theContentManager, int startX, int startY, String theAssetName)
        {
            Position = new Vector2(startX, startY);
            base.LoadContent(theContentManager, theAssetName, theMaxFramerate);
            Scale = 4;
        }

        public void Update(GameTime theGameTime)
        {

            base.Update(theGameTime, mSpeed, mDirection);
        }
    }
}
