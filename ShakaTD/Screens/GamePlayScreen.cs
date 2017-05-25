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
            game_Manager = new Game_Manager(UI_Manager.getInstance().stats.currLevel); //festlegen des levels
        }

        public override void Update(GameTime gameTime)
        {
            if (!game_Manager.level_Manager.levelCompleted)
                game_Manager.Update(gameTime);
            else
            {
                NextScreen = AllScreens.Gameplay;
                UI_Manager.getInstance().stats.currLevel++;
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
