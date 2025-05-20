using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexZeta.PlantTools.RvmViewer.Prim
{
    class RvmPrimPYRA : RvmPrimBox
    {
        public RvmPrimPYRA(List<string> cont, ref int index)
            : base(cont, ref index)
        {
            index -= 6;

            double[] arr = ParseDouble(cont[index + 5]);
            bX = arr[0];
            bY = arr[1];
            topX = arr[2];
            topY = arr[3];
            arr = ParseDouble(cont[index + 6]);
            offX = arr[0];
            offY = arr[1];
            height = arr[2];

            index += 7;
        }
    }
}
