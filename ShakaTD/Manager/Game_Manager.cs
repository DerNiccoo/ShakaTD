using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShakaTD.Components;
using ShakaTD.Components.Tower;

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
            levelZeit += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Game_Component comp in level_Manager.components)
            {
                if (comp is Tower)
                {
                    Tower currTower = (Tower)comp;
                    if (currTower.hasTarget)
                        continue;

                    currTower.findNextTarget(level_Manager.components);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            level_Manager.Draw(spriteBatch);
            //Placeholder bis ich weiß wo das besser hin sollte
            spriteBatch.Draw(Content_Manager.getInstance().Textures["buyMenu"], new Rectangle(0, 560, 1280, 160), Color.White);
        }
    }
}
