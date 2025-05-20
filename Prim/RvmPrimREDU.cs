using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexZeta.PlantTools.RvmViewer
{
    class RvmPrimREDU : RvmPrimCyli
    {
        public RvmPrimREDU(List<string> cont, ref int index)
            : base(cont, ref index)
        {
            index -= 6;

            double[] arr = ParseDouble(cont[index + 5]);
            Dia = arr[0];
            tdia = arr[1];
            Height = arr[2];
            bOffset = arr[3];
            tOffset = arr[4];

            index += 6;
        }
    }
}
