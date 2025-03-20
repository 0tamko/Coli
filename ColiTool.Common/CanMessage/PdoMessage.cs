using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColiTool.Common.CanMessage
{
    public class PdoMessage
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public DateTime Timestamp { get; set; }
        public PdoMessage(int id, byte[] data, DateTime timestamp)
        {
            Id = id;
            Data = data;
            Timestamp = timestamp;
        }
    }
}
