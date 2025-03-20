using ColiTool.Common.CanMessage;
using  ColiTool.Common.CanMessage;

namespace ColitTool.Service
{
    public class Server
    {
        private Dictionary<int, byte[]> objectDictionary = new Dictionary<int, byte[]>();  //slovnik na ukladanie SDO objektov

        public SdoResponse HandleRequest(SdoRequest request)  //metoda na spracovanie SDO poziadaviek
        {
            if (request.Data.Length <= 4)
            {
                return ProcessExpedited(request);
            }
            else
            {
                return ProcessSegmented(request);
            }
        }

        private SdoResponse ProcessExpedited(SdoRequest request)
        {
            objectDictionary[request.Index] = request.Data;    //ulozenie dat do slovnika
            return new SdoResponse(request.NodeId, request.Index, request.SubIndex, request.Data, true, "Expedited transfer successful");
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
            return new SdoResponse(request.NodeId, request.Index, request.SubIndex, request.Data, true, "Segmented transfer successful");
        }
    }
}
