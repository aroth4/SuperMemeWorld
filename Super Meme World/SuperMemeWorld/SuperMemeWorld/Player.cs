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
    class Player : Sprite
    {
        const string PLAYER_ASSETNAME = "marioSheet";
        const int PLAYER_SPEED = 640;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;

        enum State
        {
            Walking, Jumping, Crouching
        }
        State mCurrentState = State.Walking;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        int JUMP_HEIGHT = 24;
        int JUMP_SIGN = 1;
        int theMaxFramerate = 4;
        int START_POSITION_X;
        int START_POSITION_Y;

        KeyboardState mPreviousKeyboardState;
        GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
        public void LoadContent(ContentManager theContentManager, int startX, int startY)
        {
            START_POSITION_X = startX;
            START_POSITION_Y = startY;
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, PLAYER_ASSETNAME, theMaxFramerate);
            Scale = 4;
        }

        public void Update(GameTime theGameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            
            UpdateMovement(aCurrentKeyboardState);

            mPreviousKeyboardState = aCurrentKeyboardState;

            base.Update(theGameTime, mSpeed, mDirection);
        }

        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            mSpeed = Vector2.Zero;
            mDirection = Vector2.Zero;

            if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X == -1)
            {
                mSpeed.X = PLAYER_SPEED;
                mDirection.X = MOVE_LEFT;
            }

            if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X == 1)
            {
                mSpeed.X = PLAYER_SPEED;
                mDirection.X = MOVE_RIGHT;
            }

            if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == 1)
            {
                mCurrentState = State.Jumping;
            }

            if (aCurrentKeyboardState.IsKeyDown(Keys.Down) == true)
            {
                if (mCurrentState != State.Jumping)
                {   //stops ability for mid-air stopping
                    mCurrentState = State.Crouching;
                    setFrameUpdate(false);
                }
            }

            if (mCurrentState == State.Jumping)
            {
                jump();
            }

            if (mCurrentState == State.Crouching)
            {
                crouch(aCurrentKeyboardState);
            }

        }

        public void jump()
        {   //jumpin'
            mDirection.Y = MOVE_UP;
            setFrameUpdate(false);
            setCurrentFrame(new Point(0, 1));
            if (JUMP_HEIGHT < 25)
            {
                //jumpin'
                if (JUMP_HEIGHT == 0)
                {
                    JUMP_SIGN *= -1;
                    mDirection.Y = MOVE_DOWN;
                }
                Position.Y -= (JUMP_HEIGHT * JUMP_SIGN);
                JUMP_HEIGHT -= JUMP_SIGN * 2;
            }
            else
            {
                JUMP_HEIGHT = 24;
                JUMP_SIGN *= -1;
                mDirection.X = 0;
                mDirection.Y = 0;
                mCurrentState = State.Walking;
                setFrameUpdate(true);
            }
        }

        public void crouch(KeyboardState aCurrentKeyboardState)
        { //crouching
            mDirection.Y = MOVE_DOWN;
            mSpeed = Vector2.Zero;
            setFrameUpdate(false);
            setCurrentFrame(new Point(1, 1));
            if (aCurrentKeyboardState.IsKeyDown(Keys.Down) != true)
            {
                mCurrentState = State.Walking;
                setCurrentFrame(new Point(0, 0));
                setFrameUpdate(true);
            }
        }

        public Vector2 getSpeed()
        {
            return mSpeed;
        }

        public Vector2 getDirection()
        {
            return mDirection;
        }

    }
}
