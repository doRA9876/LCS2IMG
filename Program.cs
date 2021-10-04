using System.Drawing;

namespace Arihara.LCS2IMG
{
  class Program
  {
    static void Main(string[] args)
    {
      int[,] lcs = null;
      int resolution = 256;
      FileIO.LoadLCS2DFile(@"D:\Projects\LCS\data\results\lcs-300.txt", ref lcs, resolution, resolution);
      Bitmap bitmap = LCS2IMG.Transform(lcs, resolution);
      bitmap.Save("./result.png", System.Drawing.Imaging.ImageFormat.Png);
    }
  }
}
