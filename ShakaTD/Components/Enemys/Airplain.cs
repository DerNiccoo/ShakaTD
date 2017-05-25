using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakaTD.Levels;
using ShakaTD.Manager;

namespace ShakaTD.Components.Enemys
{
    class Airplain : Enemy
    {
        public Airplain(Vector2 spawn, FieldType[,] map) : base (spawn, map)
        {
            leben = 200;
            money = 10;
            speed = 140;
            Texture = Content_Manager.getInstance().Textures["airplain1"];
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
