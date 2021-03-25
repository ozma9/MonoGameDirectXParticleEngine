using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParticleEngine.Globals;
using ParticleEngine.Screens;

namespace ParticleEngine
{
    public class Game1 : Game
    {
        private ScreenManager screenManager;

        public Game1()
        {
            //Initialize the graphics device and set the content directory
            GlobalVars.Graphics = new GraphicsDeviceManager(this);
            GlobalVars.Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Content.RootDirectory = "Content";

            //Initialize the Screen Management system
            screenManager = new ScreenManager();
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            //Stop the user resizing the window (keeps it fixed at the specified resolution
            Window.AllowUserResizing = false;

            //Set to windowed mode
            GlobalVars.Graphics.IsFullScreen = false;

            //Set the resolution in the Global Vars for reference
            //GlobalVars.GameSize = new Rectangle(0, 0, 1920, 1080);
            GlobalVars.GameSize = new Rectangle(0, 0, 1280, 720);

            //Set the actual resolution of the game
            GlobalVars.Graphics.PreferredBackBufferWidth = GlobalVars.GameSize.Width;
            GlobalVars.Graphics.PreferredBackBufferHeight = GlobalVars.GameSize.Height;

            //Initialize the back buffer
            GlobalVars.BackBuffer = new RenderTarget2D(GlobalVars.Graphics.GraphicsDevice, GlobalVars.GameSize.Height, GlobalVars.GameSize.Width, false, SurfaceFormat.Color, DepthFormat.None);

            //Applies changes made here
            GlobalVars.Graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Initialize the spritebatch and declare the content directory
            GlobalVars.SpriteBatch = new SpriteBatch(GraphicsDevice);
            GlobalVars.Content = Content;


            //Load Fonts, Textures and Sounds
            Fonts.Load();
            Textures.Load();
            Sounds.Load();

            //Add the example screen to the Screen Manager
            //ScreenManager.AddScreen(new ExampleScreenRain());
            ScreenManager.AddScreen(new ExampleScreenSnow());
        }

        protected override void Update(GameTime gameTime)
        {
            //If the user has clicked off the screen then stop updating the game
            GlobalVars.WindowFocused = IsActive;

            //Update the game time in the Global Vars
            GlobalVars.GameTime = gameTime;

            //Update Screens
            screenManager.Update();

            //Update Input
            UserInput.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GlobalVars.Graphics.GraphicsDevice.SetRenderTarget(null);

            //Draw contents of Screen Manager
            screenManager.Draw();
            screenManager.DrawGUI();

            base.Draw(gameTime);
        }
    }
}
