using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShakaTD.Manager;

namespace ShakaTD.Screens
{
    class Screen
    {
        public bool changeScreen = false;
        public AllScreens NextScreen;

        public Screen()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
        public AllScreens ChangeScreen() //Und NeedChange werden in dem ScreenManager gebraucht um die anzeige zu ändern
        {
            changeScreen = false;
            return NextScreen;
        }

        public bool NeedChange()
        {
            return changeScreen;
        }
    }
}
