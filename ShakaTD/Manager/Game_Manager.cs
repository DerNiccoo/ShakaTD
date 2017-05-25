using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakaTD.Components;
using ShakaTD.Components.Tower;
using System;

namespace ShakaTD.Manager
{

    class Game_Manager
    {
        public Level_Manager level_Manager;

        private bool buildMode;
        private Tower buildTower;

        float levelZeit;

        public Game_Manager(int currLevel)
        {
            level_Manager = new Level_Manager(currLevel);
            //Hinzufügen der Tower die gekauft werden können. Hier hin damit es abhängig vom Level sein kann.
            level_Manager.towers.Add(new GunTower(new Vector2(330, 580), true));
            level_Manager.towers.Add(new RocketTower(new Vector2(440, 580), true));
            levelZeit = 0;
        }

        public void Update(GameTime gameTime)
        {
            bool LBtn = Input_Manager.getInstance().pressed.LBtn;
            bool RBtn = Input_Manager.getInstance().pressed.RBtn;
            Vector2 msPos = Input_Manager.getInstance().mousePos;
            UI_Manager.getInstance().tower = null;

            level_Manager.Update(gameTime);
            if (level_Manager.levelCompleted)
                return;
            levelZeit += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Tower tower in level_Manager.towers)
            {
                if (!tower.hasTarget && !tower.isBuyMenuTower)
                    tower.findNextTarget(level_Manager.waves[level_Manager.currWave].enemys);

                Rectangle mouseRec = new Rectangle((int)msPos.X, (int)msPos.Y, 1, 1);

                if (tower.isBuyMenuTower && tower.getRec.Intersects(mouseRec))
                {
                    UI_Manager.getInstance().tower = tower;
                }

                if (LBtn && tower.getRec.Intersects(mouseRec) 
                    && !buildMode && tower.isBuyMenuTower)
                {
                    buildMode = true;
                    if (tower is GunTower)
                        buildTower = new GunTower(new Vector2(msPos.X - 40, msPos.Y - 40), true);
                    else if (tower is RocketTower)
                        buildTower = new RocketTower(new Vector2(msPos.X - 40, msPos.Y - 40), true);
                    buildTower.wantBuild = true;
                    buildTower.Weight = 8; //Neuen Tower nach oben legen beim zeichnen.
                    Input_Manager.getInstance().pressed.LBtn = false;
                }

                //Prüfen ob ein bereits plazierter Tower angeklickt wurde
                //Der Tower soll später das onCLick haben, hier wird es nur übergeben
                if (!buildMode)
                {
                    if (LBtn)
                    {
                        tower.towerClicked(mouseRec);
                        Input_Manager.getInstance().pressed.LBtn = false;
                    }
                    else if (RBtn)
                    {
                        Input_Manager.getInstance().pressed.RBtn = false;
                        tower.activTower = false;
                        tower.Weight = 6;
                    }
                }
                else
                    tower.activTower = false;

                if (tower.activTower)
                    UI_Manager.getInstance().tower = tower;
                
            }

            if (buildMode)
            {
                UI_Manager.getInstance().tower = buildTower;
                buildTower.canBePlaced = false;
                buildTower.Position = new Vector2(Toolbox.fixCoords(msPos.X - 40), Toolbox.fixCoords(msPos.Y - 40));
                try
                {
                    if (level_Manager.level.map[(int)buildTower.Position.X / 80, (int)buildTower.Position.Y / 80] == Levels.FieldType.Gras)
                    {
                        if (Input_Manager.getInstance().pressed.LBtn)
                        {
                            buildTower.buildTower();
                            level_Manager.towers.Add(buildTower);
                            buildMode = false;
                            level_Manager.level.map[(int)buildTower.Position.X / 80, (int)buildTower.Position.Y / 80] = Levels.FieldType.Tower;
                            Input_Manager.getInstance().pressed.LBtn = false;
                        }
                        buildTower.canBePlaced = true;
                    }
                } catch (Exception e) { }
                if (Input_Manager.getInstance().pressed.RBtn)
                {
                    buildMode = false;
                    Input_Manager.getInstance().pressed.RBtn = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Placeholder bis ich weiß wo das besser hin sollte. Auch mal ein Besseres Menü bauen. Mit besseren, helleren Farben
            spriteBatch.Draw(Content_Manager.getInstance().Textures["buyMenu"], new Rectangle(0, 560, 1280, 160), Color.White);
            level_Manager.Draw(spriteBatch);
            if (buildMode)
                buildTower.DrawWithMenu(spriteBatch);
            UI_Manager.getInstance().Draw(spriteBatch);
        }
    }
}
