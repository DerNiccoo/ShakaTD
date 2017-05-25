using ShakaTD.Levels;
using System;

namespace ShakaTD
{
    public static class Toolbox
    {
        private static Random rnd = new Random();

        public static int GetRandom(int min, int max)
        {
            return rnd.Next(min, max);
        }

        public static double GetRandom()
        {
            return rnd.NextDouble();
        }

        public static int fixCoords(float pos)
        {
            if (pos % Level.BLOCKSIZE >= Level.BLOCKSIZE / 2)
            {
                pos = pos - pos % Level.BLOCKSIZE + Level.BLOCKSIZE;
            }
            else
            {
                pos = pos - pos % Level.BLOCKSIZE;
            }

            return (int)pos;
        }
    }
}
