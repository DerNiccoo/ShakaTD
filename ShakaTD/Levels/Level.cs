using System.Collections.Generic;

using ShakaTD.Components.Tiles;
using System.IO;
using Microsoft.Xna.Framework;

namespace ShakaTD.Levels
{
    enum FieldType
    {
        Gras, Path
    }
    class Level
    {
        public const int BLOCKSIZE = 80;

        public int columnMax { get; private set; }

        public Vector2 spawnVec { get; private set; }
        public Vector2 goalVec { get; private set; }

        public void LoadLevel(string filename, ref List<Tile> tiles)
        {
            string[] lines = File.ReadAllLines(filename);

            int row = 0, column = 0;
            FieldType state = FieldType.Gras;
            foreach (string line in lines)
            {
                column = 0;
                foreach (char letter in line)
                {
                    state = FieldType.Gras;
                    Vector2 tileVec = new Vector2(column * BLOCKSIZE, row * BLOCKSIZE);
                    switch (letter)
                    {
                        case '0': state = FieldType.Gras; break;
                        case '1': state = FieldType.Path; break;
                        case 'S':
                            state = FieldType.Path;
                            spawnVec = tileVec;
                            break;
                        case 'Z':
                            state = FieldType.Path;
                            goalVec = tileVec;
                            break;
                    }

                    tiles.Add(new Tile(tileVec, state));

                    column++;
                }
                row++;
            }
            columnMax = column;
        }
    }
}
