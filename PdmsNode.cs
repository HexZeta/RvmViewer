using HexZeta.PlantTools.RvmViewer.Prim;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexZeta.PlantTools.RvmViewer
{
    class PdmsNode
    {
        public List<PdmsNode> Subs = new List<PdmsNode>();
        public List<RvmPrim> Prims = new List<RvmPrim>();

        public string Name = "";
        public Position Pos = new Position();
        //public PdmsNode Owner = null;

        public List<RvmPrim> GetAllPrim()
        {
            List<RvmPrim> list = new List<RvmPrim>(Prims);
            foreach (PdmsNode sub in Subs)
            {
                list.AddRange(sub.GetAllPrim());
            }

            return list;
        }
// 
//         public Position GetPos()
//         {
//             if (Owner != null)
//             {
//                 Position op = Owner.GetPos();
//                 Position r = new Position(Pos.X + op.X, Pos.Y + op.Y, Pos.Z + op.Z);
//             }
//             return Pos;
//         }
    }
}
