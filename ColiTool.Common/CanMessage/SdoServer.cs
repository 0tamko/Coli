using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColiTool.Common.CanMessage
{
    class SdoServer
    {
        private Dictionary<int, byte[]> objectDictionary = new Dictionary<int, byte[]>();  //slovnik na ukladanie SDO objektov

        public SdoResponse HandleRequest(SdoRequest request)  //metoda na spracovanie SDO poziadaviek
        {
            if (request.Data.Length <= 4)

            {
                return ProccessExpedited(request);
            }
            else
            {
                return ProcessSegmented(request);
            }
        }

        private SdoResponse ProccessExpedited(SdoRequest request)
        {
            objectDictionary[request.Index] = request.Data;    //ulozenie dat do slovnika

            return new SdoResponse(request.Index, request.SubIndex, request.Data, true);
        }

        private SdoResponse ProcessSegmented(SdoRequest request)
        {
            int segmentSize = 7;

            List<byte[]> segments = new List<byte[]>();  //list na ukladanie segmentov

            for (int i = 0; i < request.Data.Length; i += segmentSize)
            {
                byte[] segment = request.Data.Skip(i).Take(segmentSize).ToArray();

                segments.Add(segment);
            }

            objectDictionary[request.Index] = request.Data;

            return new SdoResponse(request.Index, request.SubIndex, request.Data, true);
        }
    }
}

