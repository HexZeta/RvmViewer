using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexZeta.PlantTools.RvmViewer
{
    class RvmPrimRTOR : RvmPrimSCTO
    {
        public RvmPrimRTOR(List<string> cont, ref int index)
            : base(cont, ref index)
        {
            index -= 6;
            RCount = 4;
            double[] arr = ParseDouble(cont[index + 5]);
            diam = (arr[1] - arr[0]);
            r = arr[0] + (arr[1] - arr[0]) / 2;
            height = arr[2];
            ang = arr[3];
            m_count = (int)(4 + r / (diam + height) / 2);
            if (m_count < 0)
            {
                m_count = 0;
            }
            index += 6;
        }
    }
}
