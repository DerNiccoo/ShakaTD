using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShakaTD.Components;
using ShakaTD.Components.Tiles;
using ShakaTD.Levels;
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

        public Level_Manager(int LadeLevel)
        {
            level = new Level();
            components = new List<Game_Component>();
            LoadLevel(LadeLevel);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Tile tile in components)
            {
                tile.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Game_Component tile in components)
            {
                tile.Draw(spriteBatch);
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
