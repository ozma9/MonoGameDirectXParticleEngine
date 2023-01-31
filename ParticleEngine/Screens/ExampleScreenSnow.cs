using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ParticleEngine.Globals;
using ParticleEngine.Particles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Screens
{
    class ExampleScreenSnow : BaseScreen
    {
        private List<TreeInformaton> treeList;
        private ParticleEngineSnow snowEffect;

        string helpTextString;
        Vector2 helpTextPos;
        int updateTxtTmr;
        public ExampleScreenSnow()
        {
            snowEffect = new ParticleEngineSnow();
            SetupTrees();
            UpdateHelpText();
            screenName = "Snow";
        }
        private void SetupTrees()
        {
            treeList = new List<TreeInformaton>();
            Random _newRandom = new Random();

            //Generate a list of trees for the background
            for (int x = 0; x <= 99; x++)
            {
                TreeInformaton _newTree = new TreeInformaton();
                _newTree.SourcePos = new Rectangle(_newRandom.Next(0, 11) * 192, _newRandom.Next(8, 14) * 384, 192, 384);
                _newTree.ScreenPos = new Rectangle(_newRandom.Next(-192, GlobalVars.GameSize.Width), _newRandom.Next(-384, GlobalVars.GameSize.Height), 192, 384);

                bool _treeOkayToAdd = true;

                foreach (TreeInformaton _tree in treeList)
                {
                    //Avoid placing trees on top of one another
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
            //Generate help string to be displayed on screen
            helpTextString =
              "--- Snow Particle Effects ---" + Environment.NewLine + Environment.NewLine +
              "Particle Count: " + snowEffect.GetParticleCount() + Environment.NewLine +
              "Snow intensity: " + snowEffect.GetSnowIntensity() + Environment.NewLine +
              "Space: Start/Stop" + Environment.NewLine +
              "Q: Apply wind" + Environment.NewLine +
              "W: Remove wind" + Environment.NewLine +
              "Up Key: Increase snow intensity" + Environment.NewLine +
              "Down Key: Decrease snow intensity";

            helpTextPos = new Vector2(
                GlobalVars.GameSize.Width - Fonts.Calibri.MeasureString(helpTextString).X,
                GlobalVars.GameSize.Height - Fonts.Calibri.MeasureString(helpTextString).Y
                );
        }

        public override void Draw()
        {
            GlobalVars.SpriteBatch.Begin();

            GlobalVars.SpriteBatch.Draw(Textures.snow, GlobalVars.GameSize, Color.White);

            //Draw all trees to screen
            foreach (TreeInformaton _tree in treeList)
            {
                GlobalVars.SpriteBatch.Draw(Textures.trees, _tree.ScreenPos, _tree.SourcePos, Color.White);
            }

            //Draw the background grass texture
            GlobalVars.SpriteBatch.Draw(Textures.pixel, new Rectangle(
              (int)helpTextPos.X,
              (int)helpTextPos.Y,
              GlobalVars.GameSize.Width - (int)helpTextPos.X,
              GlobalVars.GameSize.Height - (int)helpTextPos.Y),
              Color.Black * .5f);

            //Draw the help text
            GlobalVars.SpriteBatch.DrawString(Fonts.Calibri, helpTextString, helpTextPos, Color.White);

            GlobalVars.SpriteBatch.End();

            //Draw the rain effects from the particle class
            snowEffect.DrawSnow();
        }

        public override void Update()
        {
            snowEffect.Update();

            updateTxtTmr -= (int)GlobalVars.GameTime.ElapsedGameTime.TotalMilliseconds;

            //Update the help text with the particle information every 500ms
            if (updateTxtTmr < 0)
            {
                updateTxtTmr = 500;
                UpdateHelpText();
            }
        }

        public override void HandleInput()
        {
            if (UserInput.KeyPressed(Keys.D1))
            {
                SetupTrees();
            }

            if (UserInput.KeyPressed(Keys.Up))
            {
                snowEffect.ChangeSnowIntensity(1);
                UpdateHelpText();
            }

            if (UserInput.KeyPressed(Keys.Down))
            {
                snowEffect.ChangeSnowIntensity(-1);
                UpdateHelpText();
            }

            if (UserInput.KeyPressed(Keys.Space))
            {
                snowEffect.ToggleSnow();
            }

            if (UserInput.KeyPressed(Keys.Q))
            {
                snowEffect.ChangeSnowDir(-55);
            }

            if (UserInput.KeyPressed(Keys.W))
            {
                snowEffect.ChangeSnowDir(0);
            }
        }



    }
}
