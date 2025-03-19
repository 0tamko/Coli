using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColiTool.Common.CanMessage
{
    public class CanModel
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public int Length { get; set; }
        public DateTime Timestamp { get; set; }



        public CanModel(int id, byte[] data, int length, DateTime timestamp)
        {
            Id = id;
            Data = data;
            Length = length;
            Timestamp = timestamp;
        }


       }


    }

