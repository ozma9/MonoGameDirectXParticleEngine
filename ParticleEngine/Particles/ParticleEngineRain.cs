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
        private List<Particle> pList;
        private Random rnd;

        private bool isActive;
        private int rainIntensity;
        private int rainDirection;
        private Color rainColour;

        public ParticleEngineRain()
        {
            isActive = false;
            rainDirection = 270;
            rainColour = Color.AliceBlue;
            rainIntensity = 3;
            pList = new List<Particle>();
            rnd = new Random();
        }

        public void DrawRain()
        {
            GlobalVars.SpriteBatch.Begin();

            for (int x = pList.Count - 1; x >= 0; x += -1)
            {
                Particle p = pList[x];
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
                    Particle _p = new Particle();

                    _p.Colour = rainColour;
                    _p.screenPos = new Vector2(rnd.Next(-150, (int)GlobalVars.GameSize.Width + 150), 0);
                    _p.particleDirection = rainDirection;
                    _p.particleScale.X = rnd.Next(4, 9);
                    _p.particleScale.Y = 1;
                    _p.particleAliveTime = rnd.Next(20, 30) * _p.particleScale.X;
                    _p.particleSpeed = _p.particleScale.X;
                    _p.particleRotation = -MathHelper.ToRadians(_p.particleDirection);

                    pList.Add(_p);
                }
            }


            List<Particle> removeParticles = new List<Particle>();

            foreach (Particle _p in pList)
            {
                _p.particleAliveTick += 1;

                if (_p.particleAliveTick > _p.particleAliveTime) removeParticles.Add(_p);

                _p.screenPos.X = (int)(_p.screenPos.X + (Math.Cos(MathHelper.ToRadians(_p.particleDirection)) * _p.particleSpeed));
                _p.screenPos.Y = (int)(_p.screenPos.Y + (-Math.Sin(MathHelper.ToRadians(_p.particleDirection)) * _p.particleSpeed));
            }

            foreach (Particle _p in removeParticles)
            {
                pList.Remove(_p);
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
            return pList.Count;
        }

    }

    class Particle
    {
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
