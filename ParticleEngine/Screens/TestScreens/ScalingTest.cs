using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParticleEngine.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Screens.TestScreens
{
    class ScalingTest : BaseScreen
    {
        private List<TreeObject> TreeList;
        private List<Tile> TileList;

        private Vector2 Offset;
        private Vector2 MiddleMouseDownStart;
        private bool MovingMap;

        private float scale;
        private Point zoomLevel;

        public ScalingTest()
        {
            zoomLevel = new Point(32, 32);
            Reset();
            Offset = Vector2.Zero;
            MiddleMouseDownStart = Vector2.Zero;
            MovingMap = false;
        }

        private void Reset()
        {
            //TreeList = new List<TreeObject>();
            //TreeObject _newTree;

            //for (int x = 0; x <= 29; x++)
            //{
            //    _newTree = new TreeObject(
            //        new Rectangle(GlobalVars.GlobalRandom.Next(0, GlobalVars.GameSize.Width - 192),
            //        GlobalVars.GlobalRandom.Next(0, GlobalVars.GameSize.Height - 384),
            //        192,
            //        384)
            //        );

            //    TreeList.Add(_newTree);
            //}

            TileList = new List<Tile>();
            Tile _newTile;

            for (int y = 0; y <= 15; y++)
            {
                for (int x = 0; x <= 29; x++)
                {
                    _newTile = new Tile(new Rectangle(
                         (x * zoomLevel.X),
                         (y * zoomLevel.Y),
                        32,
                        32));
                    TileList.Add(_newTile);
                }
            }


        }

        public override void Draw()
        {
            GlobalVars.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            GlobalVars.SpriteBatch.Draw(Textures.pixel, GlobalVars.GameSize, Color.DarkOliveGreen);

            //foreach (TreeObject _tree in TreeList)
            //{
            //    GlobalVars.SpriteBatch.Draw(
            //        Textures.trees,
            //        new Vector2(_tree.position.X, _tree.position.Y),
            //        new Rectangle(_tree.sourcePos.X, _tree.sourcePos.Y, _tree.size.X, _tree.size.Y),
            //        Color.White, 0f, _tree.origin, Scaling, SpriteEffects.None, 1f);
            //}

            foreach (Tile _tile in TileList)
            {
                GlobalVars.SpriteBatch.Draw(Textures.systemTiles,
                    new Rectangle(
                        (int)(_tile.position.X + Offset.X),
                        (int)(_tile.position.Y + Offset.Y),
                       zoomLevel.X,
                        zoomLevel.Y),
                    new Rectangle(_tile.sourcePos.X, _tile.sourcePos.Y, 32, 32), Color.White);

                //GlobalVars.SpriteBatch.DrawString(Fonts.Calibri, new Vector2(_tile.position.X /32, _tile.position.Y /32).ToString(), new Vector2(_tile.position.X + Offset.X, _tile.position.Y + Offset.Y), Color.Black);
            }

            GlobalVars.SpriteBatch.DrawString(Fonts.Calibri, Offset.ToString() + Environment.NewLine + zoomLevel.ToString(), Vector2.Zero, Color.Black);

            GlobalVars.SpriteBatch.End();
        }

        public override void Update()
        {
            base.Update();


        }

        public override void HandleInput()
        {
            if (UserInput.KeyPressed(Keys.Space))
            {
                Reset();
            }

            if (UserInput.KeyDown(Keys.OemPlus))
            {
                //scale += 0.5f;

                if (zoomLevel.X < 256)
                {
                    int zoomAmount = 1;

                    if (zoomLevel.X > 100) zoomAmount = 2;

                    zoomLevel.X += zoomAmount;
                    zoomLevel.Y += zoomAmount;

                    Offset.X -= 15f * zoomAmount;
                    Offset.Y -= 7.5f * zoomAmount;


                    Reset();
                }
                else
                {

                }


            }

            if (UserInput.KeyDown(Keys.OemMinus))
            {
                //scale -= 0.5f;

                if (zoomLevel.X > 16)
                {
                    int zoomAmount = 1;

                    if (zoomLevel.X > 100) zoomAmount = 2;

                    zoomLevel.X -= zoomAmount;
                    zoomLevel.Y -= zoomAmount;

                    Offset.X += 15f * zoomAmount;
                    Offset.Y += 7.5f * zoomAmount;

                    Reset();
                }


            }


            if (UserInput.MiddleMouseDown())
            {
                if (!MovingMap)
                {
                    MovingMap = true;
                    MiddleMouseDownStart = new Vector2(Mouse.GetState().X - Offset.X, Mouse.GetState().Y - Offset.Y);
                }
                else
                {
                    Offset = new Vector2(Mouse.GetState().X - MiddleMouseDownStart.X, Mouse.GetState().Y - MiddleMouseDownStart.Y);
                }
            }
            else
            {
                MovingMap = false;
            }


        }


    }

    class TreeObject
    {
        public Vector2 position;
        public Point size;
        public Vector2 centrePosition;
        public Point sourcePos;
        public Vector2 origin;

        public TreeObject(Rectangle _pos)
        {
            position = new Vector2(_pos.X, _pos.Y);
            size = new Point(_pos.Width, _pos.Height);

            centrePosition = new Vector2(position.X + size.X / 2, position.Y + size.Y / 2);
            sourcePos = new Point(GlobalVars.GlobalRandom.Next(0, 11) * 192, GlobalVars.GlobalRandom.Next(0, 8) * 384);
            origin = Vector2.Zero;
        }
    }

    class Tile
    {
        public Vector2 position;
        public Point size;
        public Vector2 centrePosition;
        public Point sourcePos;
        public Vector2 origin;

        public Tile(Rectangle _pos)
        {
            position = new Vector2(_pos.X, _pos.Y);
            size = new Point(_pos.Width, _pos.Height);

            centrePosition = new Vector2(position.X + size.X / 2, position.Y + size.Y / 2);
            sourcePos = new Point(32, 0);
            origin = Vector2.Zero;
        }

    }

}
