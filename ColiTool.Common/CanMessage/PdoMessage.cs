using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColiTool.Common.CanMessage
{
    class PdoMessage
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }


        public static Dictionary<int, string> Mapping = new Dictionary<int, string>()
        {

        };

        public PdoMessage(int id, byte[] data)
        {
            Id = id;
            Data = data;
        }

    }
}
