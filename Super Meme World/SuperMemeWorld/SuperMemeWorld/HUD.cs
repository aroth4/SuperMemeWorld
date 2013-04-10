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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class HUD
    {
        private Vector2 scorePos = new Vector2(30, 30);

        public SpriteFont Font { get; set; }

        public int Score { get; set; }

        public HUD()
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw the score in the top-left of screen
            spriteBatch.DrawString(
            Font,
            "Score: " + Score.ToString(),
            scorePos,
            Color.White);
        }
    }
}