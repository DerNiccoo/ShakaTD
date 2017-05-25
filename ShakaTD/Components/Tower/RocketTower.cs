using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

using ShakaTD.Manager;


namespace ShakaTD.Components.Tower
{
    class RocketTower : Tower
    {
        


        public RocketTower(Vector2 spawn, bool buyMenu) : base(spawn, buyMenu)
        {
            cost = 100;
            upgradeLevelMax = 1;
            upgrades.damage = new float[2] { 20f, 45f };
            upgrades.cost = new int[1] { 130};
            upgrades.range = new float[2] { 1.5f, 2f };
            upgrades.speed = new float[2] { 1.5f, 1.3f };
            upgrades.textureGun = new Texture2D[2] { Content_Manager.getInstance().Textures["rocketPlatt"], Content_Manager.getInstance().Textures["rocketPlatt"] };
            upgrades.texturePlatt = new Texture2D[2] { Content_Manager.getInstance().Textures["plattform2"], Content_Manager.getInstance().Textures["plattform3"] };
            upgrades.textureMissile = new Texture2D[2] { Content_Manager.getInstance().Textures["missile1"], Content_Manager.getInstance().Textures["missile2"] };
            hasGunFire = false;
            hasMissile = true;
            origin = new Vector2(upgrades.textureGun[0].Width / 2, upgrades.textureGun[0].Height / 2);

            missiles = new List<Missile>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Missile missile in missiles)
                missile.Update(gameTime);

            List<Missile> missileCopy = missiles;
            for (int i = 0; i < missiles.Count; i++)
            {
                if (!missiles[i].activ)
                    missileCopy.Remove(missileCopy[i]);
            }
            missiles = missileCopy;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (!hasFired)
            {
                spriteBatch.Draw(upgrades.textureMissile[upgradeLevel], new Rectangle((int)Position.X + Width / 2, (int)Position.Y + Height / 2, Width, Height),
                    null, Color.White, rotation + (float)(Math.PI * 0.5), new Vector2(origin.X, origin.Y + 10), SpriteEffects.None, 1);
            }
            else
                if (atkCD >= 500) hasFired = false;

            foreach (Missile missile in missiles)
                missile.Draw(spriteBatch);
        }
    }
}
