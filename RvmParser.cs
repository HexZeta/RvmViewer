using HexZeta.PlantTools.RvmViewer.Prim;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexZeta.PlantTools.RvmViewer
{
    class RvmParser
    {
        public RvmParser(List<string> cont)
        {
            for (index = 0; index < cont.Count; )
            {
                if (cont[index++] == "CNTB")
                {
                    Node = ProcessCNT(cont);
                }
            }
        }

        public List<string> ConvertToOff()
        {
            List<Position> ptList = new List<Position>();
            List<List<int>> faceList  = new List<List<int>>();
            InitPtAndFace(ptList, faceList);

            List<string> result = new List<string>() { "OFF" };
            result.Add(string.Format("{0} {1} {2}", ptList.Count, faceList.Count, 0));
            foreach (Position p in ptList)
            {
                result.Add(string.Format("{0} {1} {2}", p.X, p.Y, p.Z));
            }
            foreach (List<int> face in faceList)
            {
                StringBuilder build = new StringBuilder(face.Count.ToString());
                foreach (int i in face)
                {
                    build.Append(" ");
                    build.Append(i.ToString());
                }
                result.Add(build.ToString());
            }

            return result;
        }

        public List<string> ConvertToObj()
        {
            List<Position> ptList = new List<Position>();
            List<List<int>> faceList = new List<List<int>>();
            InitPtAndFace(ptList, faceList);

            List<string> result = new List<string>();
            foreach (Position p in ptList)
            {
                result.Add(string.Format("v {0} {1} {2}", p.X, p.Y, p.Z));
            }
            for (int i = 0; i < faceList.Count; i++ )
            {
                List<int> face = faceList[i];
                if (face.Count == 0)
                {
                    result.Add("g gm" + i.ToString());
                    continue;
                }
                StringBuilder build = new StringBuilder("f");
                foreach (int j in face)
                {
                    build.Append(" ");
                    build.Append(j.ToString());
                }
                result.Add(build.ToString());
            }

            return result;
        }

        private void InitPtAndFace(List<Position> ptList, List<List<int>> faceList)
        {
            Dictionary<Position, int> ptDict = new Dictionary<Position, int>();
            if (Node == null)
                return;
            List<RvmPrim> allPrim = Node.GetAllPrim();
            foreach (RvmPrim prim in allPrim)
            {
                faceList.Add(new List<int>());
                foreach (Face face in prim.GetFaceList())
                {
                    List<int> f = new List<int>();
                    foreach (Position p in face.PtList)
                    {
                        int pti;
                        if (!ptDict.TryGetValue(p, out pti))
                        {
                            ptList.Add(p);
                            pti = ptList.Count;
                            ptDict[p] = pti;
                        }
                        f.Add(pti);
                    }
                    faceList.Add(f);
                }
            }
        }

        public readonly PdmsNode Node;

        private int index;
        private PdmsNode ProcessCNT(List<string> cont)
        {
            PdmsNode currentNode = new PdmsNode();
            index++;
            currentNode.Name = cont[index++];
            index++;//double[] pos = ParseDouble(cont[index++]);
            //currentNode.Pos = new Position(pos[0], pos[1], pos[2]);
            index++;

            for (string flag = cont[index++]; flag != "CNTE"; flag = cont[index++] )
            {
                if (flag == "CNTB")
                {
                    PdmsNode sub = ProcessCNT(cont);
                    currentNode.Subs.Add(sub);
                }
                else if (flag == "PRIM")
                {
                    index++;
                    RvmPrim prim = null;
                    switch (cont[index++].Trim())
                    {
                        case "1": prim = new RvmPrimPYRA(cont, ref index); break;
                        case "2": prim = new RvmPrimBox(cont, ref index); break;
                        case "8": prim = new RvmPrimCyli(cont, ref index); break;
                        case "3": prim = new RvmPrimRTOR(cont, ref index); break;
                        case "4": prim = new RvmPrimSCTO(cont, ref index); break;
                        case "5": prim = new RvmPrimDISH(cont, ref index); break;
                        case "6": prim = new RvmPrimDISH(cont, ref index); break;
                        case "7": prim = new RvmPrimREDU(cont, ref index); break;
                        case "11": prim = new RvmPrimSEXT(cont, ref index); break;

                        case "10": prim = new RvmPrimOther(cont, ref index); break;
                        case "9": prim = new RvmPrimOther(cont, ref index); break;

                        default: throw new Exception("未知的类型"); 
                    }
                    currentNode.Prims.Add(prim);
                }
            }

            index ++;
            return currentNode;
        }
    }
}
