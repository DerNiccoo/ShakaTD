using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakaTD.Components.Enemys;
using System;
using System.Collections.Generic;

using ShakaTD.Levels;
using Microsoft.Xna.Framework.Input;

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
        public int upgradeLevel;
        public TowerUpgrades upgrades;
        public static int cost;
        public Enemy currTarget;
        public Texture2D gunFire;
        public bool hasFired;

        public Tower(Vector2 spawn) : base()
        {
            Position = spawn;
            upgradeLevel = 0;
            Weight = 6;
            Width = Level.BLOCKSIZE;
            Height = Level.BLOCKSIZE;
        }

        public override void Update(GameTime gameTime)
        {
            atkCD += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (atkCD >= 1000 * upgrades.speed[upgradeLevel] && hasTarget)
            {
                hasFired = true;
                atkCD = 0;
                currTarget.leben -= upgrades.damage[upgradeLevel];
                if (currTarget.leben <= 0)
                    hasTarget = false;
                // IMMA FIRING MA LAZOR
            }

            if (hasTarget)
            {
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
        }

        public void findNextTarget(List<Game_Component> comps)
        {
            foreach (Game_Component enemy in comps)
            {
                if (!(enemy is Enemy))
                    continue;

                Enemy target = (Enemy)enemy;
                if (intersects(target.getRec))
                {
                    currTarget = target;
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
    }
}
