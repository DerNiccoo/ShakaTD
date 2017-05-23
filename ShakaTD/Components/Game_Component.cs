using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShakaTD.Components
{
    class Game_Component
    {
        public AnimatedSprite AnimatedTexture;          //Wenn SpriteSheets vorhanden sind
        public bool IsAnimated;                         //Damit die verwendet werden
        public Texture2D Texture;                       //Normale 2D Texture ohne bewegung
        public Vector2 Position;                        //Position des Objektes
        public int Width, Height;                       //Anzeige Weite //Höhe
        public bool activ;                              //Ob das Objekt noch aktuell ist, false = löschen
        public float deltaX, deltaY;                    //Bewegungsrichtung des Objektes
        public int Weight;                              //Auf welcher Ebene das Obejtk gezeichnet werden soll. Klein = unten , hoch = oben
        public Vector2 origin;                          //Ursprung wenn man etwas drehen möchte
        public float rotation;                          //Der winkel zum Drehen. Gedrehte Objekte bisher nur in eigener Klasse Zeichnen

        public Game_Component()
        {
            activ = true;
            IsAnimated = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (IsAnimated)
                AnimatedTexture.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsAnimated)
                AnimatedTexture.Draw(spriteBatch, Position);
            else
                spriteBatch.Draw(Texture, getRec, Color.White);
        }

        public Rectangle getRec
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Width, Height); }
        }

        public void setPosition()
        {
            Position += new Vector2(deltaX, deltaY);
        }

        public void setPosition(Vector2 newPos)
        {
            Position = newPos;
        }

        public virtual void OnTopCollision()
        {
            OnBaseCollision();
        }

        public virtual void OnBottomCollision()
        {
            OnBaseCollision();
        }

        public virtual void OnLeftCollision()
        {
            OnBaseCollision();
        }

        public virtual void OnRightCollision()
        {
            OnBaseCollision();
        }

        public virtual void OnBaseCollision()
        {

        }

    }
}
