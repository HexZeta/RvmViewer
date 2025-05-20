using HexZeta.PlantTools.RvmViewer.Prim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexZeta.PlantTools.RvmViewer
{
    class RvmPrimOther : RvmPrim
    {
        public RvmPrimOther(List<string> cont, ref int index)
            : base(cont, index)
        {
            index += 6;
        }

        override public List<Face> GetFaceList()
        {
            return new List<Face>();
        }
    }
}
