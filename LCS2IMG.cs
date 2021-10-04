using System.Drawing;

namespace Arihara.LCS2IMG
{
  static class LCS2IMG
  {
    static public Bitmap Transform(int[,] ary, int size)
    {
      Bitmap bitmap = new Bitmap(size, size);
      for (int ix = 0; ix < size; ix++)
      {
        for (int iy = 0; iy < size; iy++)
        {
          if (ary[ix, iy] == 1) bitmap.SetPixel(ix, iy, Color.Black);
          else bitmap.SetPixel(ix, iy, Color.White);
        }
      }
      return bitmap;
    }
  }
}