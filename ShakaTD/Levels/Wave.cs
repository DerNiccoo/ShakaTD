using System.Collections.Generic;

using ShakaTD.Components.Enemys;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ShakaTD.Levels
{
    struct WavesInfo
    {
        public Enemys allEnemys;
        public float spawnIntervall;
    }
    /// <summary>
    /// Stellt die Wellen bereit, bekommt eine Liste mit allen Gegnern aus der "aktuellen" welle. Die werde ich Public
    /// machen. Und dann kann ich so auch "gezielt" die Gegner an den Towern übergeben und somit auch Splash damage uvm realisieren.
    /// </summary>
    class Wave
    {
        public int rewardMoney;
        public int maxEnemyCount;
        public int currEnemyCount;
        public List<Enemy> enemys;
        public float waveDowntime;
        public bool startNextWave;

        private WavesInfo[] waveInfo;
        private float enemyTimer;

        private Vector2 spawn;
        private FieldType[,] map;

        public Wave(Vector2 spawn, FieldType[,] map, int wave)
        {
            waveDowntime = 5000;
            this.spawn = spawn;
            this.map = map;
            currEnemyCount = 0;
            enemys = new List<Enemy>();

            switch (wave)
            {
                case 1:
                    rewardMoney = 70;
                    maxEnemyCount = 10;
                    waveInfo = new WavesInfo[10];
                    fillSameIntervall(2000);
                    fillSameType(Enemys.Soldat);
                    break;
                case 2:
                    rewardMoney = 100;
                    maxEnemyCount = 20;
                    waveInfo = new WavesInfo[20];
                    fillSameIntervall(2000);
                    fillSameType(Enemys.Soldat);
                    break;
                case 3:
                    rewardMoney = 140;
                    maxEnemyCount = 30;
                    waveInfo = new WavesInfo[30];
                    fillSameIntervall(1000);
                    fillSameType(Enemys.Airplain);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            enemyTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (currEnemyCount < maxEnemyCount && enemyTimer >= waveInfo[currEnemyCount].spawnIntervall)
            {
                enemyTimer = 0;
                spawnEnemy();
            }
            else if (currEnemyCount >= maxEnemyCount && enemyTimer >= waveDowntime && enemys.Count == 0)
            {
                startNextWave = true;
            }
            else if (currEnemyCount >= maxEnemyCount && enemys.Count != 0)
                enemyTimer = 0;

            foreach (Enemy enemy in enemys)
                enemy.Update(gameTime);

            List<Enemy> enemys_Copy = enemys;
            for (int i = 0; i < enemys.Count; i++)
            {
                if (!enemys[i].activ)
                    enemys_Copy.Remove(enemys_Copy[i]);
            }
            enemys = enemys_Copy;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemys)
                enemy.Draw(spriteBatch);
        }

        private void spawnEnemy()
        {
            if (waveInfo[currEnemyCount].allEnemys == Enemys.Soldat)
                enemys.Add(new Soldat(spawn, map));
            else if (waveInfo[currEnemyCount].allEnemys == Enemys.Airplain)
                enemys.Add(new Airplain(spawn, map));

            currEnemyCount++;
        }

        private void fillSameType(Enemys type)
        {
            for (int i = 0; i < waveInfo.Length; i++)
            {
                if (type == Enemys.Soldat)
                    waveInfo[i].allEnemys = Enemys.Soldat;
                else if (type == Enemys.Airplain)
                    waveInfo[i].allEnemys = Enemys.Airplain;
            }
        }

        private void fillSameIntervall(float time)
        {
            for (int i = 0; i < waveInfo.Length; i++)
                waveInfo[i].spawnIntervall = time;
        }
    }
}
