
using ShakaTD.Manager;

using ShakaTD.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShakaTD.Components.Enemys
{
    class Soldat : Enemy
    {
        public Soldat(Vector2 spawn, FieldType[,] map) : base(spawn, map)
        {
            leben = 10;
            money = 10;
            speed = 180;
            Texture = Content_Manager.getInstance().Textures["enemy1"];
            origin = new Vector2(Width / 2, Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
