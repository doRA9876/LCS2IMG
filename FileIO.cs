using System;
using System.IO;
using System.Numerics;

namespace Arihara.LCS2IMG
{
  static class FileIO
  {
    public static bool LoadVelocityFile(string path, ref Vector3[,,] velocityField)
    {
      if (!File.Exists(path))
      {
        Console.WriteLine("Target File \"{0}\" is not exists.", path);
        return false;
      }

      string fileData = string.Empty;
      using (StreamReader sr = new StreamReader(path))
      {
        fileData = sr.ReadToEnd();
      }
      fileData = fileData.Replace(" ", "");
      fileData = fileData.Replace("[", "");
      fileData = fileData.Replace("]", "");
      fileData = fileData.Replace("=", ",");
      string[] culums = fileData.Split('\n');
      string[][] datas = new string[culums.Length - 1][];
      for (int i = 0; i < culums.Length - 1; i++)
      {
        datas[i] = culums[i].Split(',');
      }

      int maxX = 0, maxY = 0, maxZ = 0;
      for (int i = 0; i < culums.Length - 1; i++)
      {
        int x = int.Parse(datas[i][0]);
        int y = int.Parse(datas[i][1]);
        int z = int.Parse(datas[i][2]);
        if (maxX < x) maxX = x;
        if (maxY < y) maxY = y;
        if (maxZ < z) maxZ = z;
      }
      maxX += 1; maxY += 1; maxZ += 1;

      velocityField = new Vector3[maxX, maxY, maxZ];
      for (int i = 0; i < culums.Length - 1; i++)
      {
        int x = int.Parse(datas[i][0]);
        int y = int.Parse(datas[i][1]);
        int z = int.Parse(datas[i][2]);
        velocityField[x, y, z] = new Vector3(float.Parse(datas[i][3]), float.Parse(datas[i][4]), float.Parse(datas[i][5]));
      }

      return true;
    }



    public static bool LoadFTLEFile(string path, ref float[,,] ftle, int lenX, int lenY, int lenZ)
    {
      if (!File.Exists(path))
      {
        Console.WriteLine("Target File \"{0}\" is not exists.", path);
        return false;
      }

      string fileData = string.Empty;
      using (StreamReader sr = new StreamReader(path))
      {
        fileData = sr.ReadToEnd();
      }

      string[] culums = fileData.Split('\n');
      ftle = new float[lenX, lenY, lenZ];

      for (int ix = 0; ix < lenX; ix++)
      {
        for (int iy = 0; iy < lenY; iy++)
        {
          for (int iz = 0; iz < lenZ; iz++)
          {
            string[] data = culums[ix * (lenY * lenZ) + iy * lenZ + iz].Split(' ');
            if (lenZ > 1) ftle[ix, iy, iz] = float.Parse(data[3]);
            else ftle[ix, iy, iz] = float.Parse(data[2]);
          }
        }
      }

      return true;
    }

    public static bool LoadLCS2DFile(string path, ref int[,] lcs, int lenX, int lenY)
    {
      if (!File.Exists(path))
      {
        Console.WriteLine("Target File \"{0}\" is not exists.", path);
        return false;
      }

      string fileData = string.Empty;
      using (StreamReader sr = new StreamReader(path))
      {
        fileData = sr.ReadToEnd();
      }

      string[] culums = fileData.Split('\n');
      lcs = new int[lenX, lenY];

      for (int ix = 0; ix < lenX; ix++)
      {
        for (int iy = 0; iy < lenY; iy++)
        {
          string[] data = culums[ix * lenY + iy].Split(' ');
          int x = int.Parse(data[0]);
          int y = int.Parse(data[1]);
          lcs[x, y] = int.Parse(data[2]);
        }
      }
      return true;
    }

    public static void WriteFTLEFile(string path, Vector3[,,] pos, float[,,] ftleField, int lenX, int lenY, int lenZ)
    {
      using (StreamWriter sw = new StreamWriter(path))
      {
        for (int ix = 0; ix < lenX; ix++)
        {
          for (int iy = 0; iy < lenY; iy++)
          {
            if (lenZ == 1)
            {
              float x = pos[ix, iy, 0].X;
              float y = pos[ix, iy, 0].Y;
              sw.WriteLine(string.Format("{0} {1} {2}", x, y, ftleField[ix, iy, 0]));
            }
            else
            {
              for (int iz = 0; iz < lenZ; iz++)
              {
                float x = pos[ix, iy, iz].X;
                float y = pos[ix, iy, iz].Y;
                float z = pos[ix, iy, iz].Z;
                sw.WriteLine(string.Format("{0} {1} {2} {3}", x, y, z, ftleField[ix, iy, iz]));
              }
            }
          }
        }
      }
    }

    public static void WriteFTLEFile(string path, float[,,] ftleField, int lenX, int lenY, int lenZ)
    {
      using (StreamWriter sw = new StreamWriter(path))
      {
        for (int ix = 0; ix < lenX; ix++)
        {
          for (int iy = 0; iy < lenY; iy++)
          {
            if (lenZ == 1)
            {
              sw.WriteLine($"{ix} {iy} {ftleField[ix, iy, 0]}");
            }
            else
            {
              for (int iz = 0; iz < lenZ; iz++)
              {
                sw.WriteLine($"{ix} {iy} {iz} {ftleField[ix, iy, iz]}");
              }
            }
          }
        }
      }
    }

    public static void WriteFTLEFile(string path, float[,] ftleField, int lenX, int lenY)
    {
      using (StreamWriter sw = new StreamWriter(path))
      {
        for (int ix = 0; ix < lenX; ix++)
        {
          for (int iy = 0; iy < lenY; iy++)
          {
            sw.WriteLine($"{ix} {iy} {ftleField[ix, iy]}");
          }
        }
      }
    }

    public static void WriteLCSFile(string path, Vector3[,,] pos, int[,,] lcs, int lenX, int lenY, int lenZ)
    {
      using (StreamWriter sw = new StreamWriter(path))
      {
        for (int ix = 0; ix < lenX; ix++)
        {
          for (int iy = 0; iy < lenY; iy++)
          {
            if (lenZ == 1)
            {
              float x = pos[ix, iy, 0].X;
              float y = pos[ix, iy, 0].Y;
              sw.WriteLine(string.Format("{0} {1} {2}", x, y, lcs[ix, iy, 0]));
            }
            else
            {
              for (int iz = 0; iz < lenZ; iz++)
              {
                float x = pos[ix, iy, iz].X;
                float y = pos[ix, iy, iz].Y;
                float z = pos[ix, iy, iz].Z;
                sw.WriteLine(string.Format("{0} {1} {2} {3}", x, y, z, lcs[ix, iy, iz]));
              }
            }
          }
        }
      }
    }

    public static void WriteLCSFile(string path, int[,,] lcs, int lenX, int lenY, int lenZ)
    {
      using (StreamWriter sw = new StreamWriter(path))
      {
        for (int ix = 0; ix < lenX; ix++)
        {
          for (int iy = 0; iy < lenY; iy++)
          {
            if (lenZ == 1)
            {
              sw.WriteLine($"{ix} {iy} {lcs[ix, iy, 0]}");
            }
            else
            {
              for (int iz = 0; iz < lenZ; iz++)
              {
                sw.WriteLine($"{ix} {iy} {iz} {lcs[ix, iy, iz]}");
              }
            }
          }
        }
      }
    }

    public static void WriteLCSFile(string path, int[,] lcs, int lenX, int lenY)
    {
      using (StreamWriter sw = new StreamWriter(path))
      {
        for (int ix = 0; ix < lenX; ix++)
        {
          for (int iy = 0; iy < lenY; iy++)
          {
            sw.WriteLine($"{ix} {iy} {lcs[ix, iy]}");
          }
        }
      }
    }

    public static void WriteVelocity2DFile(string path, int t, Vector3[,] pos, Vector3[,] vel, int lenX, int lenY)
    {
      using (StreamWriter sw = new StreamWriter(path))
      {
        for (int ix = 0; ix < lenX; ix++)
        {
          for (int iy = 0; iy < lenY; iy++)
          {
            sw.WriteLine(string.Format("{0} {1} {2} {3} ", pos[ix, iy].X, pos[ix, iy].Y, vel[ix, iy].X, vel[ix, iy].Y));
          }
        }
      }
    }

    public static void WriteGradientFile(string path, Vector2[,] gradient, int lenX, int lenY)
    {
      using (StreamWriter sw = new StreamWriter(path))
      {
        for (int ix = 0; ix < lenX; ix++)
        {
          for (int iy = 0; iy < lenY; iy++)
          {
            sw.WriteLine(string.Format("{0} {1} {2} {3} ", ix, iy, gradient[ix, iy].X, gradient[ix, iy].Y));
          }
        }
      }
    }
  }
}