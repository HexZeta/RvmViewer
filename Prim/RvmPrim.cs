using HexZeta.PlantTools.RvmViewer.Prim;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexZeta.PlantTools.RvmViewer
{
    abstract class RvmPrim
    {
        abstract public List<Face> GetFaceList();

        protected RvmPrim(List<string> cont, int index)
        {
            m[0] = ParseDouble(cont[index]);
            m[1] = ParseDouble(cont[index + 1]);
            m[2] = ParseDouble(cont[index + 2]);
        }
        protected Position TransTo(double x, double y, double z)
        {
            return new Position(
                (x * m[0][0] + y * m[0][1] + z * m[0][2] + m[0][3]) * 1000.0,
                (x * m[1][0] + y * m[1][1] + z * m[1][2] + m[1][3]) * 1000.0,
                (x * m[2][0] + y * m[2][1] + z * m[2][2] + m[2][3]) * 1000.0);
        }
        protected double[] ParseDouble(string str)
        {
            string[] sp = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            double[] r = new double[sp.Length];
            for (int i = 0; i < sp.Length; i++)
            {
                r[i] = double.Parse(sp[i]);
            }
            return r;
        }

        double[][] m = new double[3][];

        static protected readonly int ptCount = 12;  //精度. 大致是一个圆由count条边构成
    }


}
