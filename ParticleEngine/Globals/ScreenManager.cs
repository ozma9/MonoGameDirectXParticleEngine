using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Globals
{
    public abstract class BaseScreen
    {
        public string screenName = "";
        public ScreenState screenState = ScreenState.Active;
        public bool isFocused = false;
        public bool GrabFocus = true;


        public virtual void HandleInput()
        {
            // Handle Input for keyboard events.
        }

        public virtual void Update()
        {
            // General update events.
        }

        public virtual void Draw()
        {
            // Primary Draw events.
        }

        public virtual void DrawEffects()
        {
            // Secondary draw events (effects).
        }

        public virtual void DrawGUI()
        {
            // Anything that isn't affected by shaders.
        }

        public virtual void Unload()
        {
            // Shut down screen.
            screenState = ScreenState.Shutdown;
        }

    }
    public class ScreenManager
    {
        private static List<BaseScreen> Screens;
        private static List<BaseScreen> removeScreens;
        private static List<BaseScreen> newScreens;


        //FPS
        private int fps = 0;
        private int fpsCounter = 0;
        private double fpsTimer = 0;
        private string fpsText = "";
        private Color colour = Color.White;
        private int fpsY;
        private bool showFPS;

        public ScreenManager()
        {
            Screens = new List<BaseScreen>();
            removeScreens = new List<BaseScreen>();
            newScreens = new List<BaseScreen>();
            showFPS = true;
        }

        public void Update()
        {

            foreach (BaseScreen foundscreen in Screens)
            {
                if (foundscreen.screenState == ScreenState.Shutdown)
                {
                    removeScreens.Add(foundscreen);
                }
                else
                {
                    foundscreen.isFocused = false;
                }
            }

            // Remove dead screens
            foreach (BaseScreen foundscreen in removeScreens)
            {
                Screens.Remove(foundscreen);
            }

            removeScreens.Clear();

            // Add new screens
            foreach (BaseScreen foundscreen in newScreens)
            {
                Screens.Add(foundscreen);
            }

            newScreens.Clear();

            // Check screen focus
            if (Screens.Count > 0)
            {
                for (int i = Screens.Count - 1; i >= 0; i += -1)
                {
                    if (Screens[i].GrabFocus)
                    {
                        Screens[i].isFocused = true;
                        break;
                    }

                }

            }

            if (GlobalVars.WindowFocused)
            {
                // Handle Input for focused screen
                foreach (BaseScreen foundscreen in Screens)
                {
                    foundscreen.Update();
                    foundscreen.HandleInput();
                }

                if (UserInput.KeyPressed(Keys.F1))
                {
                    showFPS = !showFPS;
                }
            }
        }

        // Draw Screens
        public void Draw()
        {
            foreach (BaseScreen foundscreen in Screens)
            {
                if (foundscreen.screenState == ScreenState.Active)
                {
                    foundscreen.Draw();
                }
            }
        }

        // Draw any effects
        public void DrawEffects()
        {
            foreach (BaseScreen foundscreen in Screens)
            {
                if (foundscreen.screenState == ScreenState.Active)
                {
                    foundscreen.DrawEffects();
                }
            }
        }

        //Draw GUI
        public void DrawGUI()
        {
            foreach (BaseScreen foundscreen in Screens)
            {
                if (foundscreen.screenState == ScreenState.Active)
                {
                    foundscreen.DrawGUI();
                }
            }

            if (showFPS)
            {
                if (GlobalVars.GameTime.TotalGameTime.TotalMilliseconds >= fpsTimer)
                {
                    fps = fpsCounter;
                    fpsTimer = GlobalVars.GameTime.TotalGameTime.TotalMilliseconds + 1000;
                    fpsCounter = 1;
                    fpsText = "Fps: " + fps;
                    fpsY = GlobalVars.GameSize.Height - (int)Fonts.Calibri.MeasureString(fpsText).Y;

                    if (fps > 50)
                    {
                        colour = Color.LightGreen;
                    }
                    else if (fps > 30)
                    {
                        colour = Color.White;
                    }
                    else if (fps > 25)
                    {
                        colour = Color.Yellow;
                    }
                    else
                    {
                        colour = Color.Red;
                    }

                }
                else
                {
                    fpsCounter += 1;
                }

                GlobalVars.SpriteBatch.Begin();
                GlobalVars.SpriteBatch.DrawString(Fonts.Calibri, fpsText, new Vector2(0, fpsY), colour);
                GlobalVars.SpriteBatch.End();
            }
        }

        // Add screen
        public static void AddScreen(BaseScreen _name)
        {
            bool _addScreen = true;

            foreach (BaseScreen foundscreen in Screens)
            {
                if (_name.screenName == foundscreen.screenName)
                {
                    _addScreen = false;
                    break;
                }
            }

            if (_addScreen)
            {
                newScreens.Add(_name);
            }
        }

        // Remove screen
        public static void UnloadScreen(string _name)
        {
            foreach (BaseScreen foundscreen in Screens)
            {
                if (foundscreen.screenName == _name)
                {
                    foundscreen.Unload();
                    break;
                }

            }

        }

        // See if a screen is active
        public static bool QueryScreen(string _name)
        {
            foreach (BaseScreen foundscreen in Screens)
            {
                if (foundscreen.screenName == _name)
                {
                    return true;
                }
            }
            return false;
        }

    }

    public enum ScreenState
    {
        Active,
        Shutdown,
        Hidden
    }
}
