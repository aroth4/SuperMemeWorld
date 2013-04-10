///SuperMemeWorld is an xna game made by Aaron Roth and Nicholas Lay of York College of Pennsylvania as a CS project
///All textures/sounds used in games were either made by the creators, or listed under a Creative Commons License 
///This code is being provided as a reference for resume references, but is also being listed as an open-source project
///If used in any quantity in other projects, please provide credit

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
using SharpKit.Html;
using SharpKit.JavaScript;


namespace SuperMemeWorld
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //Game states
        public int gameState;
        public const int introMenu = 0, gameRunning = 1, gameWin = 2, gameLoss = 3;
        int count; 

        //Audio
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue trackCue;

        //Textures
        Texture2D StartGame;
        Texture2D Options;
        Texture2D QuitGame;
        Texture2D Title;
        Texture2D EasterEgg;


        //Vectors
        Vector2 Start;
        Vector2 Option;
        Vector2 Quit;
        Vector2 TitlePic;
        Vector2 NoTime;


        //Graphics
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Player/Items
        Player Player1;
        Block Block1;
        TrollDad TrollDad1;
        Coin Coin1, Coin2, Coin3, Coin4, Coin5, Coin6;
        Ground Ground1;
        MeGustaShroom Shroom1;
        Flagpole FlagPole;
        HUD hud;

        //Background
        Background mBackground01, mBackground02, mBackground03, mBackground04, mBackground05, mBackground06, mBackground07, mBackground08, mBackground09, mBackground10, winImage;

        //Move Legal
        bool moveUp;
        bool moveDown;
        bool blockHit = false;

        //Game variable
        int menuItem;
        float Selected;
        int menu;

        //The asset name for the Sprite's Texture
        public string AssetName;

        //The size of the Sprite
        public Rectangle Size;

        //Used to size the Sprite up or down from the original image
        public float Scale = 1.0f;

        //The current position of the Sprite
        public Vector2 Position = Vector2.Zero;

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
            //GameState
            gameState = introMenu;
            count = 0;
            //Player/Items
            Player1 = new Player();
            Block1 = new Block();
            Coin1 = new Coin();
            Coin2 = new Coin();
            Coin3 = new Coin();
            Coin4 = new Coin();
            Coin5 = new Coin();
            Coin6 = new Coin();
            TrollDad1 = new TrollDad();
            Ground1 = new Ground();
            FlagPole = new Flagpole();
            hud = new HUD();
            
            Block1.Scale = 2.0f;
            Block1.Position = new Vector2(400, 100);
            Coin1.Scale = 2.0f;
            Coin1.Position = new Vector2(1000, 175);
            Coin2.Scale = 2.0f;
            Coin2.Position = new Vector2(1500, 300);
            Coin3.Scale = 2.0f;
            Coin3.Position = new Vector2(2000, 300);
            Coin4.Scale = 4.0f;
            Coin4.Position = new Vector2(2500, 180);
            Coin5.Scale = 2.0f;
            Coin5.Position = new Vector2(3000, 300);
            Coin6.Scale = 2.0f;
            Coin6.Position = new Vector2(3500, 175);
            Ground1.Scale = 4.5f;
            Ground1.Position = new Vector2(300, 415);
            Shroom1 = new MeGustaShroom();
            Shroom1.Scale = 2.5f;
            Shroom1.Position = new Vector2(9000, 16);  //6070, 342
            FlagPole.Scale = 4.0f;
            FlagPole.Position = new Vector2(6000, 35);

            //Backgrounds

            mBackground01 = new Background();
            mBackground01.Scale = 1.6f;

            mBackground02 = new Background();
            mBackground02.Scale = 1.6f;

            mBackground03 = new Background();
            mBackground03.Scale = 1.6f;

            mBackground04 = new Background();
            mBackground04.Scale = 1.6f;

            mBackground05 = new Background();
            mBackground05.Scale = 1.6f;

            mBackground06 = new Background();
            mBackground06.Scale = 1.6f;

            mBackground07 = new Background();
            mBackground07.Scale = 1.6f;

            mBackground08 = new Background();
            mBackground08.Scale = 1.6f;

            mBackground09 = new Background();
            mBackground09.Scale = 1.6f;

            mBackground10 = new Background();
            mBackground10.Scale = 1.6f;

            winImage = new Background();
            winImage.Scale = 1.6f;

            //Menu ints
            menuItem = 1;
            menu = 1;
            moveUp = false;
            moveDown = false;

            //Vector locations
            Start.X = Window.ClientBounds.Width / 2 + 20;
            Start.Y = 100;
            Option.X = Window.ClientBounds.Width / 2 + 20;
            Option.Y = 200;
            Quit.X = Window.ClientBounds.Width / 2 + 20;
            Quit.Y = 300;
            TitlePic.X = -10;
            TitlePic.Y = Window.ClientBounds.Height - 400;
            NoTime.X = Window.ClientBounds.Width / 2 - 240;
            NoTime.Y = Window.ClientBounds.Height / 2 - 165;
            Selected = 1.2f;


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

            // Load game textures in
            Player1.LoadContent(this.Content, 125, 296);
            TrollDad1.LoadContent(this.Content, 5800, 310);

            //Load Blocks
            Block1.LoadContent(this.Content, "block", 4);

            //Load coins
            Coin1.LoadContent(this.Content, "Takemymoney2", 4);
            Coin2.LoadContent(this.Content, "Takemymoney2", 4);
            Coin3.LoadContent(this.Content, "Takemymoney2", 4);
            Coin4.LoadContent(this.Content, "Takemymoney2", 12);
            Coin5.LoadContent(this.Content, "Takemymoney2", 4);
            Coin6.LoadContent(this.Content, "Takemymoney2", 4);

            //Load ground
            Ground1.LoadContent(this.Content, "Ground", 4);

            //Load shroom
            Shroom1.LoadContent(this.Content, "shroom", 4);

            //Load flagpole
            FlagPole.LoadContent(this.Content, "Flagpole", 4);

            //Load Backgrounds
            mBackground01.LoadContent(this.Content, "Background01");
            mBackground01.Position = Vector2.Zero;

            mBackground02.LoadContent(this.Content, "Background02");
            mBackground02.Position = new Vector2(mBackground01.Position.X + mBackground01.Size.Width, 0);

            mBackground03.LoadContent(this.Content, "Background03");
            mBackground03.Position = new Vector2(mBackground02.Position.X + mBackground02.Size.Width, 0);

            mBackground04.LoadContent(this.Content, "Background04");
            mBackground04.Position = new Vector2(mBackground03.Position.X + mBackground03.Size.Width, 0);

            mBackground05.LoadContent(this.Content, "Background05");
            mBackground05.Position = new Vector2(mBackground04.Position.X + mBackground04.Size.Width, 0);

            mBackground06.LoadContent(this.Content, "Background01");
            mBackground06.Position = new Vector2(mBackground05.Position.X + mBackground04.Size.Width, 0);

            mBackground07.LoadContent(this.Content, "Background02");
            mBackground07.Position = new Vector2(mBackground06.Position.X + mBackground04.Size.Width, 0);

            mBackground08.LoadContent(this.Content, "Background03");
            mBackground08.Position = new Vector2(mBackground07.Position.X + mBackground04.Size.Width, 0);

            mBackground09.LoadContent(this.Content, "Background04");
            mBackground09.Position = new Vector2(mBackground08.Position.X + mBackground04.Size.Width, 0);

            mBackground10.LoadContent(this.Content, "Background05");
            mBackground10.Position = new Vector2(mBackground09.Position.X + mBackground04.Size.Width, 0);

            //Load winImage
            winImage.LoadContent(this.Content, "NoWin");
            winImage.Position = new Vector2(winImage.Position.X+200, 0);


            //Load audio
            audioEngine = new AudioEngine(@"Content/Audio/GameSounds.xgs");
            waveBank = new WaveBank(audioEngine, @"Content/Audio/Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content/Audio/Sound Bank.xsb");

            // Load images in for the menu
            StartGame = Content.Load<Texture2D>("StartGame");
            Options = Content.Load<Texture2D>("Options");
            QuitGame = Content.Load<Texture2D>("QuitGame");
            Title = Content.Load<Texture2D>("TitleScreen");
            EasterEgg = Content.Load<Texture2D>("NoTime");

            soundBank.PlayCue("Music");
            hud.Font = Content.Load<SpriteFont>("Score");
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
            Vector2 playerSpeed = Player1.getSpeed();
            Vector2 aDirection = new Vector2(-1, 0);
            Vector2 aSpeed = new Vector2(0, 0);
            
            switch (gameState)
            {
                case introMenu:
                    //If user presses start game in menu, switch to second gameState
                    // Allows the game to exit
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape) || GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                        this.Exit();
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == -1)
                    {
                        moveDown = true;
                        trackCue = soundBank.GetCue("MenuSelect");
                        trackCue.Play();

                    }
                    else { moveDown = false; }
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == 1)
                    {
                        moveUp = true;
                        trackCue = soundBank.GetCue("MenuSelect");
                        trackCue.Play();
                    }
                    else { moveUp = false; }


                    //Button Code

                    //If Start game is selected, change gamestate and start game
                    if (menuItem == 1)
                    {
                        if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                        {
                            gameState = gameRunning;
                            trackCue = soundBank.GetCue("EnemyKill");
                            trackCue.Play();
                        }
                    }

                    //If options is selected, open options
                    if (menuItem == 2)
                    {
                        if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                        {
                            menu = 2;
                            
                        }
                    }
                    //If quit is selected, exit game
                    if (menuItem == 3)
                    {
                        if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                        {
                            this.Exit();
                            trackCue = soundBank.GetCue("EnemyKill");
                            trackCue.Play();
                        }
                    }
                    //If user is stuck in the easter egg, press backspace to go back to the menu
                    if (menu == 2)
                    {
                        if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Back) || GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                        {
                            menu = 1;
                            trackCue = soundBank.GetCue("EnemyKill");
                            trackCue.Play();
                        }
                    }

                    //Update move 
                    if (moveUp == true || moveDown == true)
                    {
                        TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 150);
                        MenuMove();
                    }
                    else { TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 50); }

                    base.Update(gameTime);
                    break;

                case gameRunning:
                    // TODO: Add your update logic here
                    TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 13);
                    Player1.Update(gameTime);

                    TrollDad1.Update(gameTime); 
                    //Updates items in game
                    Block1.Update(gameTime);
                    Coin1.Update(gameTime);
                    Coin2.Update(gameTime);
                    Coin3.Update(gameTime);
                    Coin4.Update(gameTime);
                    Coin5.Update(gameTime);
                    Coin6.Update(gameTime);
                    Ground1.Update(gameTime);
                    Shroom1.Update(gameTime);
                    HandleCollisions();

                    //If player collides with pole, he wins.
                    if (Player1.Position.X > FlagPole.Position.X)
                    {

                        FlagPole.Update(gameTime);
                        if (Player1.Position.X+32 == FlagPole.Position.X)
                        {
                            trackCue = soundBank.GetCue("Shrooms");
                            trackCue.Play();
                        }
                        count++;
                        if (count > 50)
                        {
                            gameState = gameWin;
                        }
                    }

                    //Scrolling logic, update item location appropriately 
                    if (mBackground10.Position.X > 200)
                    {
                        if (Player1.Position.X > 500 && playerSpeed.X > 0)
                        {
                            Player1.Position.X = 500;
                            aSpeed = Player1.getSpeed();
                            aSpeed.X /= 1.6f;
                            aSpeed.Y = 0;
                            Block1.Position.X -= 5;
                            Coin1.Position.X -= 5;
                            Coin2.Position.X -= 5;
                            Coin3.Position.X -= 5;
                            Coin4.Position.X -= 5;
                            Coin5.Position.X -= 5;
                            Coin6.Position.X -= 5;
                            Ground1.Position.X -= 5;
                            TrollDad1.Position.X -= 5;
                            Shroom1.Position.X -= 5;
                            FlagPole.Position.X -= 5;
                        }
                    }
                    else
                    {
                        //edge of screen
                        if (Player1.Position.X > 704)
                            Player1.Position.X = 704;
                    }


            //going left off screen fix
            if (Player1.Position.X < -31)
                Player1.Position.X = -31;

                    //Update/scroll backgrounds
                    mBackground01.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    mBackground02.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    mBackground03.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    mBackground04.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    mBackground05.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    mBackground06.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    mBackground07.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    mBackground08.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    mBackground09.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    mBackground10.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.P) || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                        gameState = introMenu;

                    /*
                    //collision thing starts hyar

                    Console.Out.WriteLine(Math.Abs((Player1.Position.Y + 64) - Brick1.Position.Y));

                    Vector2 jumping = Player1.getDirection();

                    if (Math.Abs((Player1.Position.X + 32) - Brick1.Position.X) < 50)
                    {
                        //Player1.Position.X = 600;
                        if (Math.Abs((Player1.Position.Y + 64) - Brick1.Position.Y) < 68 && jumping.Y == -1)
                        {
                            Player1.Position.Y = 240;
                        }
                        //49 and 53 at left, right, respectively
                    }
                    else if (Math.Abs((Player1.Position.X + 32) - Brick1.Position.X) > 50 && (jumping.X == 1 || jumping.X == -1))
                    {
                         Player1.Position.Y = 296;
                    }

                    //collision thing stops hyar
                    */


                    audioEngine.Update();
                    base.Update(gameTime);
                    break;

                case gameWin:

                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.P) || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                        gameState = introMenu;

                    base.Update(gameTime);
                    break;
            }
        }


        /// <summary>
        /// This is called when the the menu should move.
        /// </summary>
        protected void MenuMove()
        {

            //Handles keyboard logic for menu
            if (moveUp == true)
            {
                if (menuItem > 1)
                {
                    menuItem--;

                }
                else
                {
                    menuItem = 3;

                }
            }
            if (moveDown == true)
            {
                if (menuItem < 3)
                {
                    menuItem++;

                }
                else
                {
                    menuItem = 1;

                }

            }
        }

        private void HandleCollisions()
        {
            Sprite toRemove = null;
                
            //hitting smiley blocks
            if (blockHit == false)
            {
                if (Player1.Position.X + 72 > Block1.Position.X && Player1.Position.Y - 44 < Block1.Position.Y)
                {
                    Block1.setFrameUpdate(false);
                    Block1.setCurrentFrame(new Point(1, 0));
                    Shroom1.setJump(true);
                    Shroom1.Position.X = Block1.Position.X;
                    trackCue = soundBank.GetCue("Shrooms");
                    trackCue.Play();
                    blockHit = true;
                }
            }
                
                
            //coins
            if (Player1.Position.X + 72 > Coin1.Position.X && Player1.Position.Y < Coin1.Position.Y)
            {
                toRemove = Coin1;
                hud.Score += 10;
                Coin1.Position.X = 9001;
                trackCue = soundBank.GetCue("CoinPickup");
                trackCue.Play();
            }
            if (Player1.Position.X + 72 > Coin2.Position.X && Player1.Position.Y < Coin2.Position.Y)
            {
                toRemove = Coin2;
                hud.Score += 10;
                Coin2.Position.X = 9001;
                trackCue = soundBank.GetCue("CoinPickup");
                trackCue.Play();
            }
            if (Player1.Position.X + 72 > Coin3.Position.X && Player1.Position.Y < Coin3.Position.Y)
            {
                toRemove = Coin3;
                hud.Score += 10;
                Coin3.Position.X = 9001;
                trackCue = soundBank.GetCue("CoinPickup");
                trackCue.Play();
            }
            if (Player1.Position.X + 72 > Coin4.Position.X && Player1.Position.Y < Coin4.Position.Y)
            {
                toRemove = Coin1;
                hud.Score += 50;
                Coin4.Position.X = 9001;
                trackCue = soundBank.GetCue("CoinPickup");
                trackCue.Play();
            }
            if (Player1.Position.X + 72 > Coin5.Position.X && Player1.Position.Y < Coin5.Position.Y)
            {
                toRemove = Coin2;
                hud.Score += 10;
                Coin5.Position.X = 9001;
                trackCue = soundBank.GetCue("CoinPickup");
                trackCue.Play();
            }
            if (Player1.Position.X + 72 > Coin6.Position.X && Player1.Position.Y < Coin6.Position.Y)
            {
                toRemove = Coin3;
                hud.Score += 10;
                Coin6.Position.X = 9001;
                trackCue = soundBank.GetCue("CoinPickup");
                trackCue.Play();
            }
        }
  

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            switch (gameState)
            {
                case introMenu:
                    spriteBatch.Begin();

                    mBackground01.Draw(this.spriteBatch);
                    mBackground02.Draw(this.spriteBatch);
                    mBackground03.Draw(this.spriteBatch);
                    mBackground04.Draw(this.spriteBatch);
                    mBackground05.Draw(this.spriteBatch);
                    mBackground06.Draw(this.spriteBatch);
                    mBackground07.Draw(this.spriteBatch);
                    mBackground08.Draw(this.spriteBatch);
                    mBackground09.Draw(this.spriteBatch);
                    mBackground10.Draw(this.spriteBatch);

                    spriteBatch.End();
                    if (menu == 1)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(Title,
                            TitlePic,
                            null,
                            Color.White, 0,
                            Vector2.Zero, 1,
                            SpriteEffects.None, 0);
                        spriteBatch.End();

                        // Draw Menu Items
                        if (menuItem != 1)
                        {
                            spriteBatch.Begin();
                            spriteBatch.Draw(StartGame,
                                Start,
                                null,
                                Color.White, 0,
                                Vector2.Zero, 1,
                                SpriteEffects.None, 0);
                            spriteBatch.End();
                        }
                        if (menuItem == 1)
                        {
                            spriteBatch.Begin();
                            spriteBatch.Draw(StartGame,
                                Start,
                                null,
                                Color.White, 0,
                                Vector2.Zero, Selected,
                                SpriteEffects.None, 0);
                            spriteBatch.End();
                        }
                        if (menuItem != 2)
                        {
                            spriteBatch.Begin();
                            spriteBatch.Draw(Options,
                                Option,
                                null,
                                Color.White, 0,
                                Vector2.Zero, 1,
                                SpriteEffects.None, 0);
                            spriteBatch.End();
                        }
                        if (menuItem == 2)
                        {
                            spriteBatch.Begin();
                            spriteBatch.Draw(Options,
                                Option,
                                null,
                                Color.White, 0,
                                Vector2.Zero, Selected,
                                SpriteEffects.None, 0);
                            spriteBatch.End();
                        }
                        if (menuItem != 3)
                        {
                            spriteBatch.Begin();
                            spriteBatch.Draw(QuitGame,
                                Quit,
                                null,
                                Color.White, 0,
                                Vector2.Zero, 1,
                                SpriteEffects.None, 0);
                            spriteBatch.End();
                        }
                        if (menuItem == 3)
                        {
                            spriteBatch.Begin();
                            spriteBatch.Draw(QuitGame,
                                Quit,
                                null,
                                Color.White, 0,
                                Vector2.Zero, Selected,
                                SpriteEffects.None, 0);
                            spriteBatch.End();
                        }
                    }

                    if (menu == 2)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(EasterEgg,
                            NoTime,
                            null,
                            Color.White, 0,
                            Vector2.Zero, 1,
                            SpriteEffects.None, 0);
                        spriteBatch.End();
                    }
                                

                    base.Draw(gameTime);
                    break;


                case gameRunning:

                    spriteBatch.Begin();
                    //Draw all gameRunning objects 
                    mBackground01.Draw(this.spriteBatch);
                    mBackground02.Draw(this.spriteBatch);
                    mBackground03.Draw(this.spriteBatch);
                    mBackground04.Draw(this.spriteBatch);
                    mBackground05.Draw(this.spriteBatch);
                    mBackground06.Draw(this.spriteBatch);
                    mBackground07.Draw(this.spriteBatch);
                    mBackground08.Draw(this.spriteBatch);
                    mBackground09.Draw(this.spriteBatch);
                    mBackground10.Draw(this.spriteBatch);

                    Block1.Draw(this.spriteBatch);

                    Player1.Draw(this.spriteBatch);
                    TrollDad1.Draw(this.spriteBatch);
                    Coin1.Draw(this.spriteBatch);
                    Coin2.Draw(this.spriteBatch);
                    Coin3.Draw(this.spriteBatch);
                    Coin4.Draw(this.spriteBatch);
                    Coin5.Draw(this.spriteBatch);
                    Coin6.Draw(this.spriteBatch);
                    Ground1.Draw(this.spriteBatch);
                    Shroom1.Draw(this.spriteBatch);
                    FlagPole.Draw(this.spriteBatch);

                    hud.Draw(spriteBatch);

                    spriteBatch.End();

                    base.Draw(gameTime);
                    break;

                case gameWin:

                    spriteBatch.Begin();

                    winImage.Draw(this.spriteBatch);

                    spriteBatch.End();
                    base.Draw(gameTime);
                    break;
            }
        }
    }
}
