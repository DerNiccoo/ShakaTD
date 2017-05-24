using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ShakaTD.Manager
{
    public struct Pressed
    {
        public bool LBtn;
        public bool RBtn;
    }

    class Input_Manager
    {
        private static Input_Manager instance;

        public Vector2 mousePos;
        public Pressed pressed;
        private Pressed keyDown;

        public Input_Manager()
        {
            mousePos = new Vector2();
        }

        public static Input_Manager getInstance()
        {
            if (instance == null)
                instance = new Input_Manager();
            return instance;
        }

        public void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            mousePos = new Vector2(ms.Position.X, ms.Position.Y);

            if (ms.LeftButton == ButtonState.Pressed && !keyDown.LBtn) keyDown.LBtn = true;
            else if (ms.LeftButton == ButtonState.Released && keyDown.LBtn) { keyDown.LBtn = false; pressed.LBtn = true; }
            if (ms.RightButton == ButtonState.Pressed && !keyDown.RBtn) keyDown.RBtn = true;
            else if (ms.RightButton == ButtonState.Released && keyDown.RBtn) { keyDown.RBtn = false; pressed.RBtn = true; }

        }
    }
}
