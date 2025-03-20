using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColiTool.Common.CanMessage
{
    public class SdoResponse
    {
        public int NodeId { get; set; }
        public int Index { get; set; }
        public int SubIndex { get; set; }
        public byte[] Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public SdoResponse(int nodeId, int index, int subIndex, byte[] data, bool success, string message)
        {
            NodeId = nodeId;
            Index = index;
            SubIndex = subIndex;
            Data = data;
            Success = success;
            Message = message;
        }
    }
}
