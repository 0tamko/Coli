using System;
using System.Collections.Generic;
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
                {
                    return ProcessExpedited(request);
                }
            }
        }
    }
}
