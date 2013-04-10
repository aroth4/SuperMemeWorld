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
    class MeGustaShroom : Sprite
    {
        const string SHROOM_ASSETNAME = "shroom";
        const int PLAYER_SPEED = 200;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        int theMaxFramerate = 4;
        int START_POSITION_X;
        int START_POSITION_Y;

        int JUMP_HEIGHT = 18;
        int JUMP_SIGN = 1;
       

        bool jumpStart = false;

        public void LoadContent(ContentManager theContentManager, int startX, int startY)
        {
            START_POSITION_X = startX;
            START_POSITION_Y = startY;
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, SHROOM_ASSETNAME, theMaxFramerate);
            Scale = 4f;
        }

        public void Update(GameTime theGameTime)
        {
            if (jumpStart == true)
                jump();
            Position.X+=6;
            base.Update(theGameTime, mSpeed, mDirection);
        }

        public void setJump(bool theJump)
        {
            jumpStart = theJump;
        }

        public void jump()
        {   //jumpin'
            mDirection.Y = MOVE_UP;
            if (JUMP_HEIGHT < 41)
            {
                //jumpin'
                if (JUMP_HEIGHT == 0)
                {
                    JUMP_SIGN *= -1;
                }
                Position.Y -= (JUMP_HEIGHT * JUMP_SIGN);
                JUMP_HEIGHT -= JUMP_SIGN * 2;
            }
            else
            {
                JUMP_SIGN *= -1;
                mDirection.X = 0;
                mDirection.Y = 0;
                
                jumpStart = false;
            }
        }
    }
}
