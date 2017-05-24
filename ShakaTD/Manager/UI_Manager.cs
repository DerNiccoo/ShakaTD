using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakaTD.Components.Tower;
using System;
using System.Collections.Generic;

namespace ShakaTD.Manager
{
    class UI_Manager
    {
        private static UI_Manager instance;
        private Dictionary<String, Texture2D> textures;

        public Tower tower;

        public UI_Manager()
        {
            textures = new Dictionary<String, Texture2D>();
            textures.Add("backL", Content_Manager.getInstance().Textures["barBackLeft"]);
            textures.Add("backM", Content_Manager.getInstance().Textures["barBackMid"]);
            textures.Add("backR", Content_Manager.getInstance().Textures["barBackRight"]);
            textures.Add("yellowL", Content_Manager.getInstance().Textures["barYellowLeft"]);
            textures.Add("yellowM", Content_Manager.getInstance().Textures["barYellowMid"]);
            textures.Add("yellowR", Content_Manager.getInstance().Textures["barYellowRight"]);
            textures.Add("greenL", Content_Manager.getInstance().Textures["barGreenLeft"]);
            textures.Add("greenM", Content_Manager.getInstance().Textures["barGreenMid"]);
            textures.Add("greenR", Content_Manager.getInstance().Textures["barGreenRight"]);
        }

        public static UI_Manager getInstance()
        {
            if (instance == null)
                instance = new UI_Manager();
            return instance;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (tower == null)
                return;

            //100, 590
            DrawInfo(spriteBatch, tower.upgrades.damage, 0, "Damage");
            DrawInfo(spriteBatch, tower.upgrades.speed, 1, "Speed");
            DrawInfo(spriteBatch, tower.upgrades.range, 2, "Range");
        }

        private void DrawInfo(SpriteBatch spriteBatch, float[] upgrade, int offset, string text)
        {
            int percentageNext;
            int percentage = (int)(upgrade[tower.upgradeLevel] / upgrade[tower.upgradeLevelMax] * 100);
            if (tower.upgradeLevel >= tower.upgradeLevelMax - 1)
                percentageNext = 100;
            else
                percentageNext = (int)(upgrade[tower.upgradeLevel + 1] / upgrade[tower.upgradeLevelMax] * 100);

            spriteBatch.Draw(textures["greenL"], new Rectangle(100, 590 + 40 * offset, 10, 20), Color.White);
            for (int i = 0; i < 5; i++)
            {
                if (percentage >= 20 + (i * 20))
                    spriteBatch.Draw(textures["greenM"], new Rectangle(110 + 30 * i, 590 + 40 * offset, 30, 20), Color.White);
                else if (percentageNext >= 20 + (i * 20))
                    spriteBatch.Draw(textures["yellowM"], new Rectangle(110 + 30 * i, 590 + 40 * offset, 30, 20), Color.White);
                else
                    spriteBatch.Draw(textures["backM"], new Rectangle(110 + 30 * i, 590 + 40 * offset, 30, 20), Color.White);
            }
            if (tower.upgradeLevel == tower.upgradeLevelMax)
                spriteBatch.Draw(textures["greenR"], new Rectangle(260, 590 + 40 * offset, 10, 20), Color.White);
            else if( tower.upgradeLevel + 1 == tower.upgradeLevelMax)
                spriteBatch.Draw(textures["yellowR"], new Rectangle(260, 590 + 40 * offset, 10, 20), Color.White);
            else
                spriteBatch.Draw(textures["backR"], new Rectangle(260, 590 + 40 * offset, 10, 20), Color.White);

            spriteBatch.DrawString(Content_Manager.getInstance().Fonts["towerInfo"], text, new Vector2(30, 590 + 40 * offset), Color.Red);
        }
    }
}
