using Microsoft.Xna.Framework;
using ParticleEngine.Classes.Containers;
using ParticleEngine.Classes.Objects;
using ParticleEngine.Enums;
using ParticleEngine.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Screens.TestScreens
{
    class StockSystemTest : BaseScreen
    {
        private List<Fixture> shopFloorShelving;

        public StockSystemTest()
        {
            shopFloorShelving = new List<Fixture>();

            Fixture _newDisplay;

            for (int x = 0; x <= 0; x++)
            {
                _newDisplay = new Fixture(Fixturing.ShopFloorDisplay, new Vector2(150 + (x * 128), 150));

                //int _rnd = GlobalVars.GlobalRandom.Next(50, 100);

                for (int y = 0; y <= 500; y++)
                {
                    _newDisplay.AddProductsToShelf(new Product(GlobalVars.GlobalRandom.Next(1, 21)));
                }

                shopFloorShelving.Add(_newDisplay);


            }

        }

        public override void Draw()
        {
            GlobalVars.SpriteBatch.Begin();

            foreach (Fixture _shopFloorDisplay in shopFloorShelving)
            {
                _shopFloorDisplay.Draw();
            }

            GlobalVars.SpriteBatch.End();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

    }
}
