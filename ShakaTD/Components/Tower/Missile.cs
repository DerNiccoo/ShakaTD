using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using ShakaTD.Levels;
using ShakaTD.Components.Enemys;
using ShakaTD.Manager;

namespace ShakaTD.Components.Tower
{
    class Missile : Game_Component
    {
        private Enemy target;
        private float damage;
        private float baseSpeed;
        private bool explosion;
        private bool hasExplosion;
        private Texture2D exploTexture;
        private Rectangle exploRec;
        private float showExplo;

        public Missile(Texture2D missileTexture, ref Enemy currTarget, Vector2 pos, float damage, bool hasExplosion) : base()
        {
            this.hasExplosion = hasExplosion;
            Texture = missileTexture;
            Width = Level.BLOCKSIZE;
            Height = Width;
            target = currTarget;
            baseSpeed = target.speed + 40;
            this.damage = damage;
            Position = pos;
            exploTexture = Content_Manager.getInstance().Textures["explosion" + Toolbox.GetRandom(0, 8)];
            origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            showExplo = 0;
        }

        public override void Update(GameTime gameTime)
        {
            baseSpeed *= 1.05f;
            if (explosion)
            {
                showExplo += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (showExplo >= 200)
                    activ = false;
            }
            else
            {
                Vector2 direction = new Vector2(target.Position.X + Level.BLOCKSIZE / 2, target.Position.Y + Level.BLOCKSIZE / 2) - new Vector2(Position.X + Level.BLOCKSIZE / 2, Position.Y + Level.BLOCKSIZE / 2);
                rotation = (float)Math.Atan2(direction.Y, direction.X);
                direction.Normalize();
                direction *= baseSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position += direction;

                Rectangle rocketHit = new Rectangle((int)(Position.X + Width / 3.0), (int)(Position.Y + Height / 3.0), (int)(Width / 3.0), (int)(Height / 3.0));
                Rectangle enemyHit = new Rectangle((int)(target.Position.X + target.Width / 3.0), (int)(target.Position.Y + target.Height / 3.0), (int)(target.Width / 3.0), (int)(target.Height / 3.0));

                if (rocketHit.Intersects(enemyHit))
                {
                    explosion = true;
                    exploRec = new Rectangle((int)(target.Position.X + 10), (int)(target.Position.Y + 10), 60, 60);
                    target.leben -= damage;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (explosion)
            {
                spriteBatch.Draw(exploTexture, exploRec, Color.White);
            }
            else
            {
                spriteBatch.Draw(Texture, new Rectangle((int)Position.X + Width / 2, (int)Position.Y + Height / 2, Width, Height),
                   null, Color.White, rotation + (float)(Math.PI * 0.5), origin, SpriteEffects.None, 1);
            }
            //base.Draw(spriteBatch);
        }
    }
}
