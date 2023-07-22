using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Globals
{
    class Fonts
    {
        public static SpriteFont Calibri;

        public static void Load()
        {
            Calibri = GlobalVars.Content.Load<SpriteFont>("Fonts/Calibri");
        }

    }

    class Sounds
    {
        public static SoundEffect rain;

        public static void Load()
        {
        }

    }

    class Textures
    {
        public static Texture2D pixel;
        public static Texture2D grass;
        public static Texture2D snow;
        public static Texture2D trees;

        public static Texture2D systemTiles;
        public static Texture2D shelfNew;

        public static void Load()
        {
            pixel = GlobalVars.Content.Load<Texture2D>("Textures/pixel");
            grass = GlobalVars.Content.Load<Texture2D>("Textures/grass_1280x720");
            snow = GlobalVars.Content.Load<Texture2D>("Textures/snow_1280x720");
            trees = GlobalVars.Content.Load<Texture2D>("Textures/trees");

            systemTiles = GlobalVars.Content.Load<Texture2D>("Textures/System");
            shelfNew = GlobalVars.Content.Load<Texture2D>("Textures/ShelfNew");
        }

    }
}
