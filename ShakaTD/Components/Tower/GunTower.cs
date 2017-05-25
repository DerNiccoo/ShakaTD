using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShakaTD.Manager;
using System;

namespace ShakaTD.Components.Tower
{
    class GunTower : Tower
    {

        public GunTower(Vector2 spawn, bool buyMenu = false) : base(spawn, buyMenu)
        {
            cost = 70;
            upgrades.damage = new float[3] { 1, 3, 8 };
            upgrades.cost = new int[2] { 70, 100};
            upgrades.range = new float[3] { 1, 1.5f, 2.5f};
            upgrades.speed = new float[3] { 0.3f, 0.2f, 0.1f};
            upgrades.textureGun = new Texture2D[3] {Content_Manager.getInstance().Textures["gun1"], Content_Manager.getInstance().Textures["gun2"] , Content_Manager.getInstance().Textures["gun3"] };
            upgrades.texturePlatt = new Texture2D[3] { Content_Manager.getInstance().Textures["plattform1"], Content_Manager.getInstance().Textures["plattform1"], Content_Manager.getInstance().Textures["plattform2"] };
            gunFire = Content_Manager.getInstance().Textures["gunfire1"];
            hasGunFire = true;
            hasMissile = false;
            upgradeLevelMax = 2;
            origin = new Vector2(upgrades.textureGun[0].Width / 2, upgrades.textureGun[0].Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (hasGunFire && hasFired)
            {
                if (atkCD >= 70 - (upgradeLevel * 25)) hasFired = false;

                if (upgradeLevel < 2)
                {
                    spriteBatch.Draw(gunFire, new Rectangle((int)Position.X + Width / 2, (int)Position.Y + Height / 2, Width, Height),
                        null, Color.White, rotation + (float)(Math.PI * 0.5), new Vector2(origin.X, origin.Y + 70), SpriteEffects.None, 1);
                }
                else
                {
                    spriteBatch.Draw(gunFire, new Rectangle((int)Position.X + Width / 2, (int)Position.Y + Height / 2, Width, Height),
                        null, Color.White, rotation + (float)(Math.PI * 0.5), new Vector2(origin.X - 10, origin.Y + 70), SpriteEffects.None, 1);
                    spriteBatch.Draw(gunFire, new Rectangle((int)Position.X + Width / 2, (int)Position.Y + Height / 2, Width, Height),
                        null, Color.White, rotation + (float)(Math.PI * 0.5), new Vector2(origin.X + 10, origin.Y + 70), SpriteEffects.None, 1);
                }
            }
        }
    }
}
