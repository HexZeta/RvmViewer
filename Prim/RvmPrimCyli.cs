using HexZeta.PlantTools.RvmViewer.Prim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexZeta.PlantTools.RvmViewer
{
    class RvmPrimCyli : RvmPrim
    {
        public RvmPrimCyli(List<string> cont, ref int index)
            : base(cont, index)
        {
            double[] r = ParseDouble(cont[index + 5]);
            Dia = r[0];
            Height = r[1];
            bOffset = 0;
            tOffset = 0;
            tdia = Dia;

            index += 6;
        }

        override public List<Face> GetFaceList()
        {
            //pt
            Position[] list = new Position[ptCount * 2];
            for (int i = 0; i < ptCount; i++)
            {
                double ang = Math.PI / ptCount * 2 * i;
                double x = Dia * Math.Cos(ang);
                double y = Dia * Math.Sin(ang);
                double xt = tdia * Math.Cos(ang);
                double yt = tdia * Math.Sin(ang);
                list[i] = TransTo(x - bOffset / 2, y, -Height / 2);
                list[i + ptCount] = TransTo(xt - tOffset / 2, yt, Height / 2);
            }

            //face
            List<Face> faceList = new List<Face>();
            Face a = new Face();
            Face b = new Face();
            for (int i = 0; i < ptCount; i++)
            {
                a.Add(list[i]);
                b.Add(list[i + ptCount]);
            }
            faceList.Add(a);
            faceList.Add(b);
            for (int i = 0; i < ptCount; i++)
            {
                faceList.Add(new Face(
                    list[i],
                    list[(i + 1) % ptCount],
                    list[((i + 1) % ptCount) + ptCount],
                    list[i + ptCount]));
            }

            return faceList;
        }

        protected double Height, Dia;
        protected double tdia, bOffset, tOffset;
    }
}
