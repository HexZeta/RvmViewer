using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexZeta.PlantTools.RvmViewer.Prim
{
    class RvmPrimSEXT : RvmPrim
    {
        public RvmPrimSEXT(List<string> cont, ref int index)
            : base(cont, index)
        {
            int faceCount = int.Parse(cont[index + 5].Trim());

            index += 6;
            for (int i = 0; i < faceCount; i++)
            {
                int subfaceCount = int.Parse(cont[index++]);
                for (int temp = 0; temp < subfaceCount; temp++)
                {
                    int count = int.Parse(cont[index++]);
                    Face face = new Face();
                    for (int j = 0; j < count; j++)
                    {
                        double[] posArr = ParseDouble(cont[j * 2 + index]);
                        Position pos = new Position(posArr[0], posArr[1], posArr[2]);
                        face.Add(pos);
                    }
                    faceList.Add(face);
                    index += count * 2;
                }
            }
        }

        override public List<Face> GetFaceList()
        {
            return faceList;
        }

        List<Face> faceList = new List<Face>();
    }
}
