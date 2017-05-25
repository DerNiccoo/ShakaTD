using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakaTD.Components.Enemys;
using System;
using System.Collections.Generic;

using ShakaTD.Levels;
using ShakaTD.Manager;

namespace ShakaTD.Components.Tower
{
    /// <summary>
    /// Diese Werte müssen für JEDEN Tower gesetzt werden. Baseline Speed = 1s
    /// </summary>
    /// Da es sonst bitter böse Crashed ♥
    struct TowerUpgrades
    {
        public float[] damage;
        public float[] speed;
        public float[] range;
        public Texture2D[] textureGun;
        public Texture2D[] texturePlatt;
        public Texture2D[] textureMissile;
        public int[] cost;        
    }
    /// <summary>
    /// Tower Basisklasse. Jeder Tower hat mid. diese Eigenschaften. Auch werden hier die Grundfunktionen der Tower gemacht (Schaden auf den Gegner usw.)
    /// Ein Tower beginnt auf der Upgrade Stufe 0. Da dies einiges leichter für die spätere verwendung des Arrays macht.
    /// </summary>
    class Tower : Game_Component
    {
        public float atkCD;
        public bool hasTarget;
        public bool hasGunFire;
        public bool hasMissile;
        public List<Missile> missiles;
        public int upgradeLevel;
        public int upgradeLevelMax;
        public TowerUpgrades upgrades;
        public static int cost;
        public Enemy currTarget;
        public Texture2D gunFire;
        public bool hasFired;

        //Turm im Menü oder Bauen oder upgraden Vars
        public TowerMenu towerMenu;
        public bool isBuyMenuTower;
        public bool wantBuild;
        public bool canBePlaced;
        public bool activTower;

        public Tower(Vector2 spawn, bool buyMenu = false) : base()
        {
            isBuyMenuTower = buyMenu;
            Position = spawn;
            upgradeLevel = 0;
            Weight = 6;
            Width = Level.BLOCKSIZE;
            Height = Level.BLOCKSIZE;
            towerMenu = new TowerMenu();
        }

        public override void Update(GameTime gameTime)
        {
            if (isBuyMenuTower)
            {
                base.Update(gameTime);
                return;
            }

            atkCD += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (atkCD >= 1000 * upgrades.speed[upgradeLevel] && hasTarget)
            {
                hasFired = true;
                atkCD = 0;

                if (!hasMissile)
                    currTarget.leben -= upgrades.damage[upgradeLevel];
                else
                    missiles.Add(new Missile(upgrades.textureMissile[upgradeLevel], ref currTarget, Position, upgrades.damage[upgradeLevel], true));
            }

            if (hasTarget)
            {
                if (currTarget.leben <= 0)
                    hasTarget = false;
                Vector2 direction = currTarget.Position - Position;
                rotation = (float)Math.Atan2(direction.Y, direction.X);
                if (!intersects(currTarget.getRec))
                    hasTarget = false;
            }
                

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(upgrades.texturePlatt[upgradeLevel], getRec, Color.White);
            spriteBatch.Draw(upgrades.textureGun[upgradeLevel], new Rectangle((int)Position.X + Width / 2, (int)Position.Y + Height / 2, Width, Height), 
                null, Color.White, rotation + (float)(Math.PI * 0.5), origin, SpriteEffects.None, 1);

            if (wantBuild)
            {
                if (canBePlaced)
                    spriteBatch.Draw(Content_Manager.getInstance().Textures["canPlace"],
                        new Rectangle((int)Position.X, (int)Position.Y, Width, Height), Color.White);
                else
                    spriteBatch.Draw(Content_Manager.getInstance().Textures["canNotPlace"],
                        new Rectangle((int)Position.X, (int)Position.Y, Width, Height), Color.White);
            }
        }

        public void DrawWithMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Content_Manager.getInstance().Textures["range"],
               new Rectangle((int)Position.X - (int)(upgrades.range[upgradeLevel] * 80),
               (int)Position.Y - (int)(upgrades.range[upgradeLevel] * 80), (int)(upgrades.range[upgradeLevel] * 80 * 2 + 80), (int)(upgrades.range[upgradeLevel] * 80 * 2 + 80)), Color.White);

            if (activTower)
                towerMenu.Draw(spriteBatch);

            Draw(spriteBatch);
        }

        public void findNextTarget(List<Enemy> comps)
        {
            foreach (Enemy enemy in comps)
            {
                if (intersects(enemy.getRec))
                {
                    currTarget = enemy;
                    hasTarget = true;
                    break;
                }
            }
        }

        private bool intersects(Rectangle rect)
        {
            int circle_r = (int)(Level.BLOCKSIZE * upgrades.range[upgradeLevel]);

            int circleDistance_x = Math.Abs(((int)Position.X) - rect.X);
            int circleDistance_y = Math.Abs(((int)Position.Y) - rect.Y);

            if (circleDistance_x > (rect.Width / 2 + circle_r)) { return false; }
            if (circleDistance_y > (rect.Height / 2 + circle_r)) { return false; }

            if (circleDistance_x <= (rect.Width / 2)) { return true; }
            if (circleDistance_y <= (rect.Height / 2)) { return true; }

            int cornerDistance_sq = (circleDistance_x - rect.Width / 2) ^ 2 +
                                 (circleDistance_y - rect.Height / 2) ^ 2;

            return (cornerDistance_sq <= (circle_r ^ 2));
        }

        public void buildTower()
        {
            isBuyMenuTower = false;
            wantBuild = false;
            canBePlaced = false;
            towerMenu.calculatePosition(Position);
        }

        public void towerClicked(Rectangle mousRec)
        {
            if (getRec.Intersects(mousRec))
            {
                Weight = 7;
                activTower = true;
            }
            else
            {
                if (mousRec.Intersects(towerMenu.upgradePos) && upgradeLevel < upgradeLevelMax && activTower) //Hier müsste noch die Kohle verglichen werden
                {
                    upgradeLevel++;
                    if (upgradeLevel == upgradeLevelMax)
                        towerMenu.upgradeColor = Color.DarkRed;
                }
                else if (mousRec.Intersects(towerMenu.sellPos) && activTower)
                {
                    activ = false;
                }
                else
                {
                    Weight = 6;
                    activTower = false;
                }
            }
        }
    }
}
