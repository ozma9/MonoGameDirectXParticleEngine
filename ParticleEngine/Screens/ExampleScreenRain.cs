using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ParticleEngine.Globals;
using ParticleEngine.Particles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Screens
{
    class ExampleScreenRain : BaseScreen
    {
        private List<TreeInformaton> treeList;
        private ParticleEngineRain rainEffect;

        string helpTextString;
        Vector2 helpTextPos;
        int updateTxtTmr;
        public ExampleScreenRain()
        {
            SetupTrees();

            rainEffect = new ParticleEngineRain();

            UpdateHelpText();
            updateTxtTmr = 250;

        }

        private void SetupTrees()
        {
            treeList = new List<TreeInformaton>();
            Random _newRandom = new Random();

            for (int x = 0; x <= 99; x++)
            {
                TreeInformaton _newTree = new TreeInformaton();
                _newTree.SourcePos = new Rectangle(_newRandom.Next(0, 11) * 192, _newRandom.Next(0, 7) * 384, 192, 384);
                _newTree.ScreenPos = new Rectangle(_newRandom.Next(-192, GlobalVars.GameSize.Width), _newRandom.Next(-384, GlobalVars.GameSize.Height), 192, 384);

                bool _treeOkayToAdd = true;

                foreach (TreeInformaton _tree in treeList)
                {
                    if (_tree.ScreenPos.Contains(_newTree.ScreenPos))
                    {
                        _treeOkayToAdd = false;
                        break;
                    }
                }

                if (_treeOkayToAdd)
                {
                    treeList.Add(_newTree);
                }
            }

        }

        private void UpdateHelpText()
        {
            helpTextString =
              "--- Rain Particle Effects ---" + Environment.NewLine + Environment.NewLine +
              "Particle Count: " + rainEffect.GetParticleCount() + Environment.NewLine +
              "Rain intensity: " + rainEffect.GetRainIntensity() + Environment.NewLine +
              "Space: Start/Stop" + Environment.NewLine +
              "Q: Apply right directional wind" + Environment.NewLine +
              "W: Remove directional wind" + Environment.NewLine +
              "E: Apply left directional wind" + Environment.NewLine +
              "A: Acid Rain" + Environment.NewLine +
              "D: Normal Rain" + Environment.NewLine +
              "Up Key: Increase rain intensity" + Environment.NewLine +
              "Down Key: Decrease rain intensity";

            helpTextPos = new Vector2(
                GlobalVars.GameSize.Width - Fonts.Calibri.MeasureString(helpTextString).X,
                GlobalVars.GameSize.Height - Fonts.Calibri.MeasureString(helpTextString).Y
                );
        }

        public override void Draw()
        {
            GlobalVars.SpriteBatch.Begin();

            GlobalVars.SpriteBatch.Draw(Textures.grass, GlobalVars.GameSize, Color.White);

            foreach (TreeInformaton _tree in treeList)
            {
                GlobalVars.SpriteBatch.Draw(Textures.trees, _tree.ScreenPos, _tree.SourcePos, Color.White);
            }

            GlobalVars.SpriteBatch.Draw(Textures.pixel, new Rectangle(
                (int)helpTextPos.X,
                (int)helpTextPos.Y,
                GlobalVars.GameSize.Width - (int)helpTextPos.X,
                GlobalVars.GameSize.Height - (int)helpTextPos.Y),
                Color.Black * .5f);

            GlobalVars.SpriteBatch.DrawString(Fonts.Calibri, helpTextString, helpTextPos, Color.White);

            GlobalVars.SpriteBatch.End();

            rainEffect.DrawRain();
        }

        public override void Update()
        {
            rainEffect.Update();

            updateTxtTmr -= (int)GlobalVars.GameTime.ElapsedGameTime.TotalMilliseconds;

            if(updateTxtTmr < 0)
            {
                updateTxtTmr = 500;
                UpdateHelpText();
            }

        }

        public override void HandleInput()
        {
            if (UserInput.KeyPressed(Keys.Space))
            {
                rainEffect.ToggleRain();
            }

            if (UserInput.KeyPressed(Keys.Q))
            {
                rainEffect.ChangeRainDir(250);
            }

            if (UserInput.KeyPressed(Keys.W))
            {
                rainEffect.ChangeRainDir(270);
            }

            if (UserInput.KeyPressed(Keys.E))
            {
                rainEffect.ChangeRainDir(290);
            }

            if (UserInput.KeyPressed(Keys.A))
            {
                rainEffect.ChangeRainColour(Color.LawnGreen);
            }

            if (UserInput.KeyPressed(Keys.D))
            {
                rainEffect.ChangeRainColour(Color.AliceBlue);
            }

            if (UserInput.KeyPressed(Keys.Up))
            {
                rainEffect.ChangeRainIntensity(1);
                UpdateHelpText();
            }

            if (UserInput.KeyPressed(Keys.Down))
            {
                rainEffect.ChangeRainIntensity(-1);
                UpdateHelpText();
            }

            if (UserInput.KeyPressed(Keys.D1))
            {
                SetupTrees();
            }
        }

    }

    public struct TreeInformaton
    {
        public Rectangle ScreenPos;
        public Rectangle SourcePos;
    }
}
