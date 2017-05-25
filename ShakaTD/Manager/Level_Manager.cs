using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShakaTD.Components;
using ShakaTD.Levels;
using ShakaTD.Components.Tower;


namespace ShakaTD.Manager
{
    class Level_Manager
    {
        public Level level;
        public bool levelCompleted;

        public List<Game_Component> components; //Vll eine Liste GameComponents machen, dann darein alles hinzufügen, könnte einiges einfacher machen
        public List<Tower> towers;

        public Vector2 spawnPosition { get; private set; }
        public Vector2 goalPosition { get; private set; }

        public List<Wave> waves;
        public int currWave;

        public int columnMax { get; private set; } //Wenn das Spiel irgendwann vll mal Scrollable in eine Richtung sein soll

        public Level_Manager(int LadeLevel)
        {
            level = new Level();
            components = new List<Game_Component>();
            waves = new List<Wave>();
            towers = new List<Tower>();
            currWave = 0;
            LoadLevel(LadeLevel);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Game_Component comp in components)
                comp.Update(gameTime);

            waves[currWave].Update(gameTime);
            if (waves[currWave].startNextWave)
            {
                if ((currWave + 1) >= waves.Count)
                {
                    levelCompleted = true;
                    return;
                }
                else
                    currWave++;
            }

            foreach (Tower tower in towers)
                tower.Update(gameTime);

            List<Tower> towers_Copy = towers;
            for (int i = 0; i < towers.Count; i++)
            {
                if (!towers[i].activ)
                {
                    level.map[Toolbox.fixCoords(towers[i].Position.X) / 80, Toolbox.fixCoords(towers[i].Position.Y) / 80] = FieldType.Gras;
                    towers_Copy.Remove(towers_Copy[i]);
                }
            }
            towers = towers_Copy;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Wichtig hier war das die ganzen Layer stimmen. Da ich ja jetzt nicht mehr das mit dem Gewicht mache kann es passieren das Überlappungen kommen
            //Daher ist das hier gefühlt sehr unübersichtlich. Tiles => Gegner => Missiles => SpecialTower(ActivTower//WantBuild)
            Tower specialTower = null;
            foreach (Game_Component comp in components)
                comp.Draw(spriteBatch);

            waves[currWave].Draw(spriteBatch);

            foreach (Tower tower in towers)
            {
                if (tower.activTower || tower.wantBuild)
                    specialTower = tower;
                else
                    tower.Draw(spriteBatch);
            }                

            foreach (Tower tower in towers)
            {
                if (tower.hasMissile)
                {
                    foreach (Missile missile in tower.missiles)
                        missile.Draw(spriteBatch);
                }
            }
            if (specialTower != null)
                specialTower.DrawWithMenu(spriteBatch);
        }
        
        private void LoadLevel(int LadeLevel)
        {
            try
            {
                level.LoadLevel(@"Levels\Level" + LadeLevel + ".txt", ref components);
            }
            catch
            {
                level.LoadLevel(@"Levels\Level1.txt", ref components);
                LadeLevel = 1;
            }
            spawnPosition = level.spawnVec;
            goalPosition = level.goalVec;
            columnMax = level.columnMax;

            if (LadeLevel == 1)
            {
                waves.Add(new Wave(spawnPosition, level.map, 1));
                waves.Add(new Wave(spawnPosition, level.map, 2));
            }
            else if (LadeLevel == 2)
            {
                waves.Add(new Wave(spawnPosition, level.map, 1));
                waves.Add(new Wave(spawnPosition, level.map, 2));
                waves.Add(new Wave(spawnPosition, level.map, 3));
            }
            else
            {
                waves.Add(new Wave(spawnPosition, level.map, 1));
            }
        }
    }
}
