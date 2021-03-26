using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ParticleEngine.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Screens
{
    class ExampleScreenSnow : BaseScreen
    {
        private List<TreeInformaton> treeList;
        public ExampleScreenSnow()
        {
            SetupTrees();
        }
        private void SetupTrees()
        {
            treeList = new List<TreeInformaton>();
            Random _newRandom = new Random();

            for (int x = 0; x <= 99; x++)
            {
                TreeInformaton _newTree = new TreeInformaton();
                _newTree.SourcePos = new Rectangle(_newRandom.Next(0, 11) * 192, _newRandom.Next(8, 14) * 384, 192, 384);
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

        public override void Draw()
        {
            GlobalVars.SpriteBatch.Begin();

            GlobalVars.SpriteBatch.Draw(Textures.snow, GlobalVars.GameSize, Color.White);

            foreach (TreeInformaton _tree in treeList)
            {
                GlobalVars.SpriteBatch.Draw(Textures.trees, _tree.ScreenPos, _tree.SourcePos, Color.White);
            }

            GlobalVars.SpriteBatch.End();
        }

        public override void Update()
        {
          
        }

        public override void HandleInput()
        {
            if (UserInput.KeyPressed(Keys.D1))
            {
                SetupTrees();
            }
        }



    }
}
