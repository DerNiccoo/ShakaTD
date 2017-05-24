using ShakaTD.Levels;

namespace ShakaTD
{
    public static class Toolbox
    {
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
