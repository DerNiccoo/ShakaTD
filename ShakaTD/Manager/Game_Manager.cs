using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakaTD.Components;
using ShakaTD.Components.Tower;
using System;

namespace ShakaTD.Manager
{
    class Game_Manager
    {
        Level_Manager level_Manager;


        private bool buildMode;
        private Tower buildTower;

        float levelZeit;

        public Game_Manager(int currLevel)
        {
            level_Manager = new Level_Manager(currLevel);
            //Hinzufügen der Tower die gekauft werden können. Hier hin damit es abhängig vom Level sein kann.
            level_Manager.components.Add(new GunTower(new Vector2(330, 580), true));
            levelZeit = 0;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 msPos = Input_Manager.getInstance().mousePos;

            level_Manager.Update(gameTime);
            levelZeit += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Game_Component comp in level_Manager.components)
            {
                if (comp is Tower)
                {
                    Tower currTower = (Tower)comp;
                    if (!currTower.hasTarget && !currTower.isBuyMenuTower)
                        currTower.findNextTarget(level_Manager.components);

                    Rectangle mousRec = new Rectangle((int)msPos.X, (int)msPos.Y, 1, 1);

                    if (Input_Manager.getInstance().pressed.LBtn && currTower.getRec.Intersects(mousRec) 
                        && !buildMode && currTower.isBuyMenuTower)
                    {
                        buildMode = true;
                        buildTower = new GunTower(new Vector2(msPos.X - 40, msPos.Y - 40), true);
                    }

                    //Prüfen ob ein bereits plazierter Tower angeklickt wurde
                    //Der Tower soll später das onCLick haben, hier wird es nur übergeben
                    if (!buildMode)
                    {

                        if (Input_Manager.getInstance().pressed.LBtn && currTower.getRec.Intersects(mousRec))
                        {
                            Input_Manager.getInstance().pressed.LBtn = false;
                            currTower.upgradeLevel++;
                            if (currTower.upgradeLevel > currTower.upgradeLevelMax)
                                currTower.upgradeLevel = currTower.upgradeLevelMax;
                        }
                        else if (Input_Manager.getInstance().pressed.RBtn && currTower.getRec.Intersects(mousRec))
                        {
                            Input_Manager.getInstance().pressed.RBtn = false;
                            currTower.upgradeLevel--;
                            if (currTower.upgradeLevel < 0)
                                currTower.upgradeLevel = 0;
                        }
                    }
                }
            }

            if (buildMode)
            {
                buildTower.Position = new Vector2(Toolbox.fixCoords(msPos.X - 40), Toolbox.fixCoords(msPos.Y - 40));
                if (Input_Manager.getInstance().pressed.LBtn)
                {
                    try
                    {
                        if (level_Manager.level.map[(int)buildTower.Position.X / 80, (int)buildTower.Position.Y / 80] == Levels.FieldType.Gras)
                        {
                            buildTower.isBuyMenuTower = false;
                            level_Manager.components.Add(buildTower);
                            buildMode = false;
                            level_Manager.level.map[(int)buildTower.Position.X / 80, (int)buildTower.Position.Y / 80] = Levels.FieldType.Tower;
                        }
                    } catch (Exception e) { }
                    Input_Manager.getInstance().pressed.LBtn = false;
                }
                else if (Input_Manager.getInstance().pressed.RBtn)
                {
                    buildMode = false;
                    Input_Manager.getInstance().pressed.RBtn = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Placeholder bis ich weiß wo das besser hin sollte
            spriteBatch.Draw(Content_Manager.getInstance().Textures["buyMenu"], new Rectangle(0, 560, 1280, 160), Color.White);
            level_Manager.Draw(spriteBatch);
            if (buildMode)
                buildTower.Draw(spriteBatch);
        }
    }
}
