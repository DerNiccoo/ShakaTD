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
            bool LBtn = Input_Manager.getInstance().pressed.LBtn;
            bool RBtn = Input_Manager.getInstance().pressed.RBtn;
            Vector2 msPos = Input_Manager.getInstance().mousePos;
            UI_Manager.getInstance().tower = null;

            level_Manager.Update(gameTime);
            levelZeit += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Game_Component comp in level_Manager.components)
            {
                if (comp is Tower)
                {
                    Tower currTower = (Tower)comp;
                    if (!currTower.hasTarget && !currTower.isBuyMenuTower)
                        currTower.findNextTarget(level_Manager.components);

                    Rectangle mouseRec = new Rectangle((int)msPos.X, (int)msPos.Y, 1, 1);

                    if (currTower.isBuyMenuTower && currTower.getRec.Intersects(mouseRec))
                    {
                        UI_Manager.getInstance().tower = currTower;
                    }

                    if (LBtn && currTower.getRec.Intersects(mouseRec) 
                        && !buildMode && currTower.isBuyMenuTower)
                    {
                        buildMode = true;
                        buildTower = new GunTower(new Vector2(msPos.X - 40, msPos.Y - 40), true);
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
                            currTower.towerClicked(mouseRec);
                            Input_Manager.getInstance().pressed.LBtn = false;
                        }
                        else if (RBtn)
                        {
                            Input_Manager.getInstance().pressed.RBtn = false;
                            currTower.activTower = false;
                            currTower.Weight = 6;
                        }
                    }

                    if (currTower.activTower)
                        UI_Manager.getInstance().tower = currTower;
                }
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
                            level_Manager.components.Add(buildTower);
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
                buildTower.Draw(spriteBatch);
            UI_Manager.getInstance().Draw(spriteBatch);
        }
    }
}
