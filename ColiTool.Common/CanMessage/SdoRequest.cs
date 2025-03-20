using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColiTool.Common.CanMessage
{
    class SdoRequest
    {
        public int Index { get; set; }
        public int SubIndex { get; set; }
        public byte[] Data { get; set; }


        public SdoRequest(int index, int subindex, byte[] data)
        {

            Index = index;
            SubIndex = subindex;
            Data = data;
        }
    }
}
