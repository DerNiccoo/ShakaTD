using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShakaTD.Levels;
using ShakaTD.Manager;

namespace ShakaTD.Components.Tiles
{
    class Tile : Game_Component
    {
        public bool canBeBuild;
        public FieldType tileType;

        public Tile(Vector2 spawnPosition, FieldType typeOfTile = FieldType.Path) : base()
        {
            Position = spawnPosition;
            Width = Level.BLOCKSIZE;
            Height = Level.BLOCKSIZE;
            canBeBuild = false;
            tileType = typeOfTile;

            switch (typeOfTile)
            {
                case FieldType.Path: Texture = Content_Manager.getInstance().Textures["sand"]; break;
                case FieldType.Gras: Texture = Content_Manager.getInstance().Textures["gras"]; canBeBuild = true; break;
            }
        }
    }
}
