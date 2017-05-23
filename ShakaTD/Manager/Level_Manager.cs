using System.Collections.Generic;

using ShakaTD.Components;
using ShakaTD.Levels;
using ShakaTD.Components.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShakaTD.Manager
{
    class Level_Manager
    {
        Level level;

        public List<Game_Component> components; //Vll eine Liste GameComponents machen, dann darein alles hinzufügen, könnte einiges einfacher machen

        public Vector2 spawnPosition { get; private set; }
        public Vector2 goalPosition { get; private set; }

        public int columnMax { get; private set; } //Wenn das Spiel irgendwann vll mal Scrollable in eine Richtung sein soll

        private float levelRunMS;

        public Level_Manager(int LadeLevel)
        {
            level = new Level();
            components = new List<Game_Component>();
            levelRunMS = 0;
            LoadLevel(LadeLevel);
        }

        public void Update(GameTime gameTime)
        {
            components.Sort((x, y) => x.Weight.CompareTo(y.Weight));        //Damit die wichtigeren Objekte oben liegen

            foreach (Game_Component comp in components)
            {
                comp.Update(gameTime);
            }

            //Freigeben von Resorucen bzw einfach nur nicht das zu viele Objekte immer geupdatet/drawt werden
            List<Game_Component> component_Copy = components;
            for (int i = 0; i < components.Count; i++)
            {
                if (!components[i].activ)
                    component_Copy.Remove(component_Copy[i]);
            }
            components = component_Copy;


            levelRunMS += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (levelRunMS >= 500)
            {
                levelRunMS = 0;
                components.Add(new Soldat(spawnPosition, level.map));
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Game_Component comp in components)
            {
                comp.Draw(spriteBatch);
            }
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
            }
            spawnPosition = level.spawnVec;
            goalPosition = level.goalVec;
            columnMax = level.columnMax;
        }
    }
}
