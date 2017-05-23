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
            game_Manager = new Game_Manager(2); //festlegen des levels
        }

        public override void Update(GameTime gameTime)
        {
            game_Manager.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            game_Manager.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
