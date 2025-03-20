using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColiTool.Database.Tests.Mocks
{
    public class MockCanDriver : ICanDriver
    {
        private Queue<byte[]> _messages = new Queue<byte[]>();

        public void SendMessage(int id, byte[] data)
        {
            _messages.Enqueue(data);
        }

        public byte[] ReceiveMessage()
        {
            {
                if (_messages.Count > 0)
                {
                    return _messages.Dequeue();
                }
                return null;
            }
        }
    }
}
