using Microsoft.Xna.Framework;
using ParticleEngine.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Screens
{
    class ExampleScreenSnow : BaseScreen
    {
        public ExampleScreenSnow()
        {

        }

        public override void Draw()
        {
            GlobalVars.SpriteBatch.Begin();

            GlobalVars.SpriteBatch.Draw(Textures.snow, GlobalVars.GameSize, Color.White);

            GlobalVars.SpriteBatch.End();
        }

        public override void Update()
        {
          
        }

        public override void HandleInput()
        {
           
        }



    }
}
