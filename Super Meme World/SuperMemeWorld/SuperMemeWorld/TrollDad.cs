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


namespace SuperMemeWorld
{
    /// <summary>
    /// This class implements the TrollDad Enemy in the game
    /// </summary>
    class TrollDad : Sprite
    {
        const string PLAYER_ASSETNAME = "trolldad";
        const int PLAYER_SPEED = 200;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        int count;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        int theMaxFramerate = 40;
        int START_POSITION_X;
        int START_POSITION_Y;


        public void LoadContent(ContentManager theContentManager, int startX, int startY)
        {
            START_POSITION_X = startX;
            START_POSITION_Y = startY;
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, PLAYER_ASSETNAME, theMaxFramerate);
            Scale = 3.5f;
            count = 0;
            mDirection.X= -1;
        }

        public void Update(GameTime theGameTime)
        {
            count++;
            //If counter = 100, reverse enemy direction, reset count then
            if (count == 100)
            {
                mDirection.X *= -1;
                count = 0;
            }
            mSpeed.X = PLAYER_SPEED;
            base.Update(theGameTime, mSpeed, mDirection);
        }

        public Vector2 getSpeed()
        {
            return mSpeed;
        }

    }
}
