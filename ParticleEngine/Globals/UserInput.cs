using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Globals
{
    class UserInput
    {
        static KeyboardState CurrentKeyState;
        static KeyboardState LastKeyState;

        static MouseState CurrentMouseState;
        static MouseState LastMouseState;

        public static void Update()
        {
            LastKeyState = CurrentKeyState;
            CurrentKeyState = Keyboard.GetState();

            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        public static bool KeyDown(Keys key)
        {
            return CurrentKeyState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            if (CurrentKeyState.IsKeyDown(key) & LastKeyState.IsKeyUp(key))
                return true;
            return false;
        }

        public static bool LeftMouseClick()
        {
            if (CurrentMouseState.LeftButton == ButtonState.Released && LastMouseState.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }

        public static bool RightMouseClick()
        {
            if (CurrentMouseState.RightButton == ButtonState.Released && LastMouseState.RightButton == ButtonState.Pressed)
                return true;
            return false;
        }

        public static bool MiddleMouseClick()
        {
            if (CurrentMouseState.MiddleButton == ButtonState.Released && LastMouseState.MiddleButton == ButtonState.Pressed)
                return true;
            return false;
        }


        public static bool MiddleMouseDown()
        {
            if (CurrentMouseState.MiddleButton == ButtonState.Pressed) return true;

            return false;
        }
    }
}
