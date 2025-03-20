using ColiTool.WebApi.Tests.Mocks;

namespace ColiTool.WebApi.Tests
{
    public class Tests
    {
        private MockCanDriver _mockCanDriver;

        [SetUp]
        public void Setup()
        {
            _mockCanDriver = new MockCanDriver();
        }

        [Test]
        public void SendMessageTest()
        {

        }

        [Test]
        public void ReceiveMessageTest()
        {

        }
    }
}