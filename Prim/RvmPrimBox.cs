using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexZeta.PlantTools.RvmViewer.Prim
{
    class RvmPrimBox : RvmPrim
    {
        public RvmPrimBox(List<string> cont, ref int index)
            : base(cont, index)
        {
            double[] r = ParseDouble(cont[index + 5]);
            bX = topX = r[0];
            bY = topY = r[1];
            height = r[2];

            index += 6;
        }

        override public List<Face> GetFaceList()
        {
            //pt
            Position[] list = new Position[8];
            list[0] = TransTo(+bX / 2, -bY / 2, -height / 2);
            list[1] = TransTo(-bX / 2, -bY / 2, -height / 2);
            list[2] = TransTo(+bX / 2, +bY / 2, -height / 2);
            list[3] = TransTo(-bX / 2, +bY / 2, -height / 2);
            list[4] = TransTo(+topX / 2 + offX, -topY / 2 + offY, +height / 2);
            list[5] = TransTo(-topX / 2 + offX, -topY / 2 + offY, +height / 2);
            list[6] = TransTo(+topX / 2 + offX, +topY / 2 + offY, +height / 2);
            list[7] = TransTo(-topX / 2 + offX, +topY / 2 + offY, +height / 2);

            //face
            List<Face> faceList = new List<Face>();
            faceList.Add(new Face(list[0], list[1], list[3], list[2]));
            faceList.Add(new Face(list[4], list[5], list[7], list[6]));
            faceList.Add(new Face(list[0], list[1], list[5], list[4]));
            faceList.Add(new Face(list[1], list[3], list[7], list[5]));
            faceList.Add(new Face(list[2], list[3], list[7], list[6]));
            faceList.Add(new Face(list[2], list[0], list[4], list[6]));

            return faceList;
        }

        protected double bX, bY, topX, topY, offX, offY, height;
    }
}
