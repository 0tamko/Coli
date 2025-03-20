using System.Collections.Generic;

namespace ColiTool.Common
{
    public class Models
    {
        // SDO požiadavka – Index a SubIndex zadávame ako hex reťazce (napr. "0x6010", "0x04"), Data je zoznam bajtov.
        public class SdoRequest
        {
            public int NodeId { get; set; }
            public string Index { get; set; }    
            public string SubIndex { get; set; }   
                                                   
            public int? Data { get; set; }
            public string Type { get; set; }       
            public string DataType { get; set; }
            
            public SdoRequest() { }

            public SdoRequest(int nodeId, string index, string subIndex, int? data, string type, string dataType)
            {
                NodeId = nodeId;
                Index = index;
                SubIndex = subIndex;
                Data = data;
                Type = type;
                DataType = dataType;
            }
        }

    
    public class SdoResponse
        {
            public int NodeId { get; set; }
            public int Index { get; set; }
            public int SubIndex { get; set; }
            public byte[] Data { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }
            


            public SdoResponse() { }

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

        public class CanMessage
        {
            public uint ArbitrationId { get; set; }
            public byte[] Data { get; set; }
        }
    }

