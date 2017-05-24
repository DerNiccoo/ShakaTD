using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakaTD.Manager;

namespace ShakaTD.Components.Tower
{
    class TowerMenu
    {
        private int size = 40;

        //Würde ich ja gerne in eine Struct packen und dan listen weiße durchgehen. ABER DAN WÄRE ES JA KEINE VARIABLE MEHR!!!
        public Rectangle upgradePos;
        public Color upgradeColor;
        private int upgradeOffsetX = -50;
        private int upgradeOffsetY = -50;
        private Texture2D upgradeText;
        public Rectangle sellPos;
        public Color sellColor;
        private int sellOffsetX = 90;
        private int sellOffsetY = -50;
        private Texture2D sellText;

        public TowerMenu()
        {
            upgradeText = Content_Manager.getInstance().Textures["upgradeBtn"];
            upgradeColor = Color.White;
            sellText = Content_Manager.getInstance().Textures["sellBtn"];
            sellColor = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(upgradeText, upgradePos, upgradeColor);
            spriteBatch.Draw(sellText, sellPos, sellColor);
        }

        public void calculatePosition(Vector2 towerPos)
        {
            upgradePos = fixPositions(towerPos, upgradeOffsetX, upgradeOffsetY);
            sellPos = fixPositions(towerPos, sellOffsetX, sellOffsetY);
        }

        private Rectangle fixPositions(Vector2 towerPos, int offsetX, int offsetY)
        {
            int posX = (int)towerPos.X + offsetX;
            int posY = (int)towerPos.Y + offsetY;

            if (posY < 0 || posY > 560)
                posY = (int)towerPos.Y - offsetY + size / 2;

            if (posX < 0 || posX > 1280)
                posX = (int)towerPos.X - offsetX;
            //Anderen Richtungen noch fixen

            return new Rectangle(posX, posY, size, size);
        }
    }
}
