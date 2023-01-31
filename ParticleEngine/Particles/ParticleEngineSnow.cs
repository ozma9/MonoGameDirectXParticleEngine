using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleEngine.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Particles
{
    class ParticleEngineSnow
    {
        private List<SnowParticle> particleList;
        private Random rnd;

        private bool isActive;
        private int snowIntensity;
        private int snowDirectionalInfluence;
        private Color snowColour;
        public ParticleEngineSnow()
        {
            //Begin the class with the rain stopped
            isActive = false;

            //Declare the starting direction of the rain (default 270 - downwards)
            snowDirectionalInfluence = 0;

            //Declare the rain colour (blueish)
            snowColour = Color.White;

            //Define rain intensity (default 3 - low)
            snowIntensity = 3;

            //Initiate the particle list
            particleList = new List<SnowParticle>();

            //Initiate the RNG
            rnd = new Random();
        }

        public void DrawSnow()
        {
            GlobalVars.SpriteBatch.Begin();

            //Draws the rain to screen
            // - The for loop will step backward through the particle list, drawing the most recently added first and the oldest last

            for (int x = particleList.Count - 1; x >= 0; x--)
            {
                SnowParticle p = particleList[x];
                GlobalVars.SpriteBatch.Draw(Textures.pixel, new Rectangle((int)p.screenPos.X, (int)p.screenPos.Y, (int)p.particleScale.X, (int)p.particleScale.Y), new Rectangle(0, 0, 1, 1), p.Colour * MathHelper.Clamp((p.particleAliveTime / p.particleAliveTick - 1), 0, 1), p.particleRotation, new Vector2(MathHelper.ToRadians(p.particleDirection), 0), SpriteEffects.None, 0);
            }

            GlobalVars.SpriteBatch.End();
        }

        public void Update()
        {
            //Update the particle list but only if the screen has focus
            if (isActive)
            {
                for (int x = 0; x <= snowIntensity; x++)
                {
                    SnowParticle _p = new SnowParticle();

                    _p.Colour = snowColour;
                    _p.screenPos = new Vector2(rnd.Next(-150, GlobalVars.GameSize.Width + 150), rnd.Next(-150, GlobalVars.GameSize.Height + 150));
                    _p.particleDirection = rnd.Next(240, 300) + snowDirectionalInfluence;
                    _p.particleScale.X = rnd.Next(1, 5);
                    _p.particleScale.Y = _p.particleScale.X;
                    _p.particleAliveTime = rnd.Next(10, 40) * (_p.particleScale.X + _p.particleScale.Y);
                    _p.particleSpeed = 1;
                    _p.particleRotation = -MathHelper.ToRadians(_p.particleDirection);

                    particleList.Add(_p);
                }
            }


            //Remove old particles
            // - Do this by creating a new list of particles and then removing them based upon the removeParticles list
            // - This method prevents the "collection was modified" exception

            List<SnowParticle> removeParticles = new List<SnowParticle>();

            foreach (SnowParticle _p in particleList)
            {
                _p.particleAliveTick += 1;
                _p.particleRotation -= 0.04f;


                if (_p.particleAliveTick > _p.particleAliveTime) removeParticles.Add(_p);

                _p.screenPos.X = (int)(_p.screenPos.X + (Math.Cos(MathHelper.ToRadians(_p.particleDirection)) * _p.particleSpeed));
                _p.screenPos.Y = (int)(_p.screenPos.Y + (-Math.Sin(MathHelper.ToRadians(_p.particleDirection)) * _p.particleSpeed));
            }

            foreach (SnowParticle _p in removeParticles)
            {
                particleList.Remove(_p);
            }
        }

        public void ChangeSnowIntensity(int _amount)
        {
            snowIntensity += _amount;

            if (snowIntensity > 20)
            {
                snowIntensity = 20;
            }

            if (snowIntensity < 1)
            {
                snowIntensity = 1;
            }

        }

        public void ChangeSnowDir(int _amount)
        {
            snowDirectionalInfluence = _amount;
        }

        public void ToggleSnow()
        {
            isActive = !isActive;
        }

        public int GetSnowIntensity()
        {
            return snowIntensity;
        }

        public int GetParticleCount()
        {
            return particleList.Count;
        }

    }

    class SnowParticle
    {
        //Base class for the snow particles

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
