using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColiTool.Common.CanMessage
{
    class SdoResponse
    {
        public int Index { get; set; }
        public int SubIndex { get; set; }
        public byte[] Data { get; set; }
        public bool Success { get; set; }


        public SdoResponse(int index, int subindex, byte[] data, bool success)
        {
            Index = index;
            SubIndex = subindex;
            Data = data;
            Success = success;
        }
    }
}
