using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleEngine.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Particles
{
    class ParticleEngineRain
    {
      //Setup the class
      // - Declare a list of rain particles to iterate through
      // - Random number for variablity
      // - A boolean value to switch on and off the rain
      // - An int to hold the value for the min and max amount of rain
      // - An int to hold the value of the direction of rain (later converted into a radius
      // - A colour variable to determine the colour of the rain

        private List<RainParticle> particleList;
        private Random rnd;

        private bool isActive;
        private int rainIntensity;
        private int rainDirection;
        private Color rainColour;

        public ParticleEngineRain()
        {
            //Begin the class with the rain stopped
            isActive = false;

            //Declare the starting direction of the rain (default 270 - downwards)
            rainDirection = 270;

            //Declare the rain colour (blueish)
            rainColour = Color.LightSkyBlue;

            //Define rain intensity (default 3 - low)
            rainIntensity = 3;

            //Initiate the particle list
            particleList = new List<RainParticle>();

            //Initiate the RNG
            rnd = new Random();
        }

        public void DrawRain()
        {
            GlobalVars.SpriteBatch.Begin();

            //Draws the rain to screen
            // - The for loop will step backward through the particle list, drawing the most recently added first and the oldest last

            for (int x = particleList.Count - 1; x >= 0; x--)
            {
                RainParticle p = particleList[x];
                GlobalVars.SpriteBatch.Draw(Textures.pixel, new Rectangle((int)p.screenPos.X, (int)p.screenPos.Y, (int)p.particleScale.X, (int)p.particleScale.Y), new Rectangle(0, 0, 1, 1), p.Colour * MathHelper.Clamp((p.particleAliveTime / p.particleAliveTick - 1), 0, 1), p.particleRotation, new Vector2(MathHelper.ToRadians(p.particleDirection), 0), SpriteEffects.None, 0);
            }

            GlobalVars.SpriteBatch.End();
        }

        public void Update()
        {
            if (isActive)
            {
                for (int x = 0; x <= rainIntensity; x++)
                {
                    RainParticle _p = new RainParticle();

                    _p.Colour = rainColour;
                    _p.screenPos = new Vector2(rnd.Next(-150, GlobalVars.GameSize.Width + 150), 0);
                    _p.particleDirection = rainDirection;
                    _p.particleScale.X = rnd.Next(4, 9);
                    _p.particleScale.Y = 1;
                    _p.particleAliveTime = rnd.Next(20, 30) * _p.particleScale.X;
                    _p.particleSpeed = _p.particleScale.X;
                    _p.particleRotation = -MathHelper.ToRadians(_p.particleDirection);

                    particleList.Add(_p);
                }
            }


            List<RainParticle> removeParticles = new List<RainParticle>();

            foreach (RainParticle _p in particleList)
            {
                _p.particleAliveTick += 1;

                if (_p.particleAliveTick > _p.particleAliveTime) removeParticles.Add(_p);

                _p.screenPos.X = (int)(_p.screenPos.X + (Math.Cos(MathHelper.ToRadians(_p.particleDirection)) * _p.particleSpeed));
                _p.screenPos.Y = (int)(_p.screenPos.Y + (-Math.Sin(MathHelper.ToRadians(_p.particleDirection)) * _p.particleSpeed));
            }

            foreach (RainParticle _p in removeParticles)
            {
                particleList.Remove(_p);
            }
        }

        public void ChangeRainDir(int _newDir)
        {
            rainDirection = _newDir;
        }

        public void ChangeRainColour(Color _newColour)
        {
            rainColour = _newColour;
        }

        public void ChangeRainIntensity(int _amount)
        {
            rainIntensity += _amount;

            if (rainIntensity > 20)
            {
                rainIntensity = 20;
            }

            if (rainIntensity < 1)
            {
                rainIntensity = 1;
            }

        }

        public void ToggleRain()
        {
            isActive = !isActive;
        }

        public int GetRainIntensity()
        {
            return rainIntensity;
        }

        public int GetParticleCount()
        {
            return particleList.Count;
        }

    }

    class RainParticle
    {
        //Base class for the rain particles

        public Vector2 screenPos;
        public float particleSpeed;
        public float particleDirection;
        public float particleRotation;
        public Vector2 particleScale;
        public float particleAliveTime;
        public float particleAliveTick;
        public Color Colour;
    }

}
