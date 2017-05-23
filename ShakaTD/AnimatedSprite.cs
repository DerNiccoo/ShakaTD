using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ShakaTD
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private double currentsteps;
        private double counter;
        private int dstwidth, dstheight;

        public AnimatedSprite(Texture2D texture, int rows, int columns, int weite, int höhe, double steps = 1)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            counter = 0;
            totalFrames = Rows * Columns;
            currentsteps = steps;
            dstwidth = weite;
            dstheight = höhe;
        }

        public void Update()
        {
            counter += currentsteps;

            if (counter > 1)
            {
                counter = 0;
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, dstwidth, dstheight);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
