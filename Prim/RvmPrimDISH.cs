using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexZeta.PlantTools.RvmViewer.Prim
{
    class RvmPrimDISH : RvmPrim
    {
        public RvmPrimDISH(List<string> cont, ref int index)
            : base(cont, index)
        {
            double[] arr = ParseDouble(cont[index + 5]);
            dia = arr[0];
            height = arr[1];

            index += 6;
        }

        override public List<Face> GetFaceList()
        {
            //pt
            Position[] list = new Position[ptCount * 3 + 1];
            for (int i = 0; i < ptCount; i++)
            {
                double ang = Math.PI / ptCount * 2 * i;
                double x = dia * Math.Cos(ang);
                double y = dia * Math.Sin(ang);
                list[i] = TransTo(x, y, 0);
                list[i + ptCount] = TransTo(x * 0.8, y * 0.8, height / 3);
                list[i + ptCount + ptCount] = TransTo(x * 0.5, y * 0.5, height / 3 * 2);
            }
            list[ptCount * 3] = TransTo(0, 0, height);

            //face
            List<Face> faceList = new List<Face>();
            for (int j = 0; j < ptCount; j++)
            {
                for (int i = 0; i < 2; i++)
                {
                    faceList.Add(new Face(
                            list[i * ptCount + j],
                            list[i * ptCount + ((j + 1) % ptCount)],
                            list[(i + 1) * ptCount + ((j + 1) % ptCount)],
                            list[(i + 1) * ptCount + j]));
                }
                faceList.Add(new Face(
                            list[2 * ptCount + j],
                            list[2 * ptCount + ((j + 1) % ptCount)],
                            list[3 * ptCount]));
            }

            return faceList;
        }

        protected double dia, height;
    }
}
