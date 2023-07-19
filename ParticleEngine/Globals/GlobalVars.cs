using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Globals
{
    class GlobalVars
    {
        public static ContentManager Content;
        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch SpriteBatch;
        public static RenderTarget2D BackBuffer;
        public static GameTime GameTime;
        public static bool WindowFocused;
        public static Rectangle GameSize;
        public static Random GlobalRandom;
    }
    public struct TreeInformaton
    {
        public Rectangle ScreenPos;
        public Rectangle SourcePos;
    }
}
