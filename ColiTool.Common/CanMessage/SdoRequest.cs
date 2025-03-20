using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColiTool.Common.CanMessage
{
    public class SdoRequest
    {
        public int NodeId { get; set; }
        public int Index { get; set; }
        public int SubIndex { get; set; }
        public byte[] Data { get; set; }
        public string Type { get; set; }
        public SdoRequest(int nodeId, int index, int subIndex, byte[] data, string type)
        {
            NodeId = nodeId;
            Index = index;
            SubIndex = subIndex;
            Data = data;
            Type = type;
        }
    }
}
