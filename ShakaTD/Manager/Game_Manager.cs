using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
