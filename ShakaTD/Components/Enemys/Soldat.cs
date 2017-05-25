
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
            leben = 100;
            money = 10;
            speed = 100;
            Texture = Content_Manager.getInstance().Textures["enemy1"];
            origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
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
