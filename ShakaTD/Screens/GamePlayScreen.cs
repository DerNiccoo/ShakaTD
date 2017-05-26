using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShakaTD.Manager;

namespace ShakaTD.Screens
{
    class GamePlayScreen : Screen
    {
        Game_Manager game_Manager;

        public GamePlayScreen() : base()
        {
            UI_Manager.getInstance().Init();
            game_Manager = new Game_Manager(UI_Manager.getInstance().stats.level); //festlegen des levels
        }

        public override void Update(GameTime gameTime)
        {
            if (!game_Manager.level_Manager.levelCompleted)
                game_Manager.Update(gameTime);
            else
            {
                NextScreen = AllScreens.Gameplay;
                UI_Manager.getInstance().Init();    //Level ist noch verbuggt
                changeScreen = true;
            }
            if (UI_Manager.getInstance().stats.leben <= 0)
            {
                NextScreen = AllScreens.Victory; // GameOver eig.
                changeScreen = true;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            game_Manager.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
