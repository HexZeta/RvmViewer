using HexZeta.PlantTools.RvmViewer.Prim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexZeta.PlantTools.RvmViewer
{
    class RvmPrimSCTO : RvmPrim
    {
        public RvmPrimSCTO(List<string> cont, ref int index)
            : base(cont, index)
        {
            double[] arr = ParseDouble(cont[index + 5]);
            diam = arr[1] * 2;
            height = diam;
            r = arr[0];
            ang = arr[2];
            m_count = (int)(ang / Math.PI * 6 + 2.49);

            index += 6;
        }

        override public List<Face> GetFaceList()
        {
            //pt
            Position[] list = new Position[RCount * m_count];
            if (list.Length == 0)
            {
                return new List<Face>();
            }
            for (int i = 0; i < RCount; i++)
            {
                double angXY = Math.PI * 2 / RCount * i;
                double l = r + diam * Math.Cos(angXY) / 2;
                for (int j = 0; j < m_count; j++)
                {
                    double angZ = ang / (m_count - 1) * j;
                    double x = l * Math.Cos(angZ);
                    double y = l * Math.Sin(angZ);
                    double z = height * Math.Sin(angXY) / 2;
                    list[i * m_count + j] = TransTo(x, y, z);
                }
            }

            //face
            List<Face> faceList = new List<Face>();
            Face a = new Face();
            Face b = new Face();
            for (int i = 0; i < RCount; i++)
            {
                a.Add(list[m_count * i]);
                b.Add(list[m_count * i + m_count - 1]);
            }
            faceList.Add(a);
            faceList.Add(b);

            for (int i = 0; i < RCount; i++)
            {
                for (int j = 0; j < m_count - 1; j++)
                {
                    faceList.Add(new Face(
                        list[i * m_count + j],
                        list[i * m_count + (j + 1)],
                        list[((i + 1) % RCount) * m_count + j + 1],
                        list[((i + 1) % RCount) * m_count + j]));
                }
            }

            return faceList;
        }

        protected double diam, r, ang; //直径 角度 弯曲半径(弧度)
        protected int m_count;
        protected double height;

        protected int RCount = ptCount;
    }
}
