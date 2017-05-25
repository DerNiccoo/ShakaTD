using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using ShakaTD.Levels;
using ShakaTD.Components.Enemys;


namespace ShakaTD.Components.Tower
{
    class Missile : Game_Component
    {
        private Enemy target;
        private float speed;
        private float damage;
        private float baseSpeed;

        public Missile(Texture2D missileTexture, ref Enemy currTarget, Vector2 pos, float speed, float damage) : base()
        {
            Texture = missileTexture;
            Width = Level.BLOCKSIZE;
            Height = Width;
            target = currTarget;
            baseSpeed = target.speed + 20;
            this.speed = speed;
            this.damage = damage;
            Position = pos;
            origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }

        public override void Update(GameTime gameTime)
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
                activ = false;
                target.leben -= damage;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X + Width / 2, (int)Position.Y + Height / 2, Width, Height),
                    null, Color.White, rotation + (float)(Math.PI * 0.5), origin, SpriteEffects.None, 1);
            //base.Draw(spriteBatch);
        }
    }
}
