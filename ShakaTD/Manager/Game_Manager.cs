using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShakaTD.Manager
{
    class Game_Manager
    {
        Level_Manager level_Manager;

        float levelZeit;

        public Game_Manager(int currLevel)
        {
            level_Manager = new Level_Manager(currLevel);
            levelZeit = 0;
        }

        public void Update(GameTime gameTime)
        {
            level_Manager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            level_Manager.Draw(spriteBatch);
            //Placeholder bis ich weiß wo das besser hin sollte
            spriteBatch.Draw(Content_Manager.getInstance().Textures["buyMenu"], new Rectangle(0, 560, 1280, 160), Color.White);
        }
    }
}
