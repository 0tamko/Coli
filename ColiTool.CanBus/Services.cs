using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ColiTool.Common;
using Microsoft.Extensions.Configuration;
using SocketCANSharp;
using SocketCANSharp.Network;

namespace ColiTool.CanBus
{
    public class CanBusService
    {
        private bool IsInitialized = false;
        private readonly bool _simulateDevice;
        private readonly string _canInterfaceName;
        private RawCanSocket _rawCanSocket; 

        private const string SDO_COMMANDS_FILE_PATH = "can_logs.txt";

        public CanBusService(IConfiguration configuration)
        {
            _simulateDevice = configuration.GetValue<bool>("SimulateDevice", false);
            _canInterfaceName = configuration.GetValue<string>("CanInterfaceName", "can0");
        }

        public void Initialize()
        {
            if (!IsInitialized)
            {
                Console.WriteLine("Initializing CAN Bus adapter...");
                if (!_simulateDevice)
                {
                    var iface = CanNetworkInterface.GetAllInterfaces(true)
                        .FirstOrDefault(i => i.Name.Equals(_canInterfaceName, StringComparison.OrdinalIgnoreCase));
                    if (iface == null)
                    {
                        throw new Exception($"CAN interface '{_canInterfaceName}' not found or is down.");
                    }
                    _rawCanSocket = new RawCanSocket();
                    _rawCanSocket.Bind(iface);

                    _rawCanSocket.ReceiveTimeout = 1000;
                }
                else
                {
                    Console.WriteLine("Simulating device (no real SocketCAN).");
                }
                IsInitialized = true;
            }
        }

        public object GetStatus()
        {
            if (!IsInitialized)
                Initialize();
            return new { Status = _simulateDevice ? "Simulated" : "Real" };
        }

        private static readonly Dictionary<int, (byte Specifier, int Length)> DataTypeMapping = new Dictionary<int, (byte, int)>
        {
            { 0x0001, (0x2F, 1) },  // 1 byte
            { 0x0002, (0x2B, 2) },  // 2 bytes
            { 0x0003, (0x2B, 2) },  // 2 bytes
            { 0x0004, (0x23, 4) },  // 4 bytes
            { 0x0005, (0x2B, 2) },  // 2 bytes
            { 0x0006, (0x2B, 2) },  // 2 bytes
            { 0x0007, (0x23, 4) },  // 4 bytes
            { 0x0008, (0x27, 3) }   // 3 bytes
        };

        public async Task<object> ProcessSdoRequestAsync(Models.SdoRequest request)
        {
            try
            {

                ushort index = Convert.ToUInt16(request.Index, 16);
                byte subindex = Convert.ToByte(request.SubIndex, 16);
                uint messageId = (uint)(0x600 + request.NodeId);

                byte[] payload = new byte[8];
                byte commandSpecifier = 0x00;

                if (request.Type.Equals("read", StringComparison.OrdinalIgnoreCase))
                {
                    commandSpecifier = 0x40;
                }
                else if (request.Type.Equals("write", StringComparison.OrdinalIgnoreCase))
                {
                    if (!request.Data.HasValue)
                        throw new ArgumentException("Write command requires Data value.");
                    if (string.IsNullOrEmpty(request.DataType))
                        throw new ArgumentException("Write command requires DataType value.");

                    int dt = Convert.ToInt32(request.DataType, 16);
                    if (!DataTypeMapping.TryGetValue(dt, out var mapping))
                        throw new ArgumentException($"Unsupported data type: {request.DataType}");

                    commandSpecifier = mapping.Specifier;
                    int expectedLength = mapping.Length;

                    int value = request.Data.Value;
                    byte[] valueBytes = BitConverter.GetBytes(value);
                    Array.Copy(valueBytes, 0, payload, 4, expectedLength);
                }
                else
                {
                    throw new ArgumentException("Unsupported command type. Only 'read' and 'write' are supported.");
                }

                payload[0] = commandSpecifier;
                payload[1] = (byte)(index & 0xFF);
                payload[2] = (byte)((index >> 8) & 0xFF);
                payload[3] = subindex;

                string humanReadable = $"0x{commandSpecifier:X2} 0x{payload[2]:X2} 0x{payload[1]:X2} 0x{subindex:X2} " +
                                       $"{BitConverter.ToString(payload, 4, 4).Replace("-", " ")}";
                string binaryFrameHex = BitConverter.ToString(payload).Replace("-", "");

                Console.WriteLine($"SDO Command: {humanReadable}");
                Console.WriteLine($"SDO Command (hex): {binaryFrameHex}");

                File.AppendAllText(SDO_COMMANDS_FILE_PATH, $"{humanReadable}\n{binaryFrameHex}\n\n");

                if (_simulateDevice)
                {
                    if (request.Type.Equals("read", StringComparison.OrdinalIgnoreCase))
                        return new { message = "Read command simulated successfully.", data = "00 00 00 00" };
                    else
                        return new { message = "Write command simulated successfully." };
                }
                return new { message = "Write command sent successfully." };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing SDO request: {ex.Message}");
                throw;
            }
        }


        private async Task SendCanFrameAsync(uint canId, byte[] data)
        {
            if (_simulateDevice)
                return;
            var frame = new CanFrame(canId, data);
            int bytesWritten = _rawCanSocket.Write(frame);
            if (bytesWritten <= 0)
                throw new Exception("Failed to write CAN frame.");
            await Task.Delay(10);
        }

        private async Task<CanMessage> ReceiveCanFrameAsync(uint expectedId)
        {
            if (_simulateDevice)
                return null;
            DateTime start = DateTime.Now;
            TimeSpan timeout = TimeSpan.FromSeconds(1);
            while (DateTime.Now - start < timeout)
            {
                if (_rawCanSocket.Read(out CanFrame readFrame) > 0)
                {
                    if (readFrame.CanId == expectedId)
                    {
                        return new CanMessage
                        {
                            ArbitrationId = readFrame.CanId,
                            Data = readFrame.Data
                        };
                    }
                }
                await Task.Delay(10);
            }
            return null;
        }

        public async Task<CanMessage> ReceiveAsync(int timeoutMs)
        {
            if (_simulateDevice)
            {
                await Task.Delay(timeoutMs);
                return new CanMessage { ArbitrationId = 0x000, Data = new byte[8] };
            }
            else
            {
                if (_rawCanSocket.Read(out CanFrame readFrame) > 0)
                {
                    return new CanMessage { ArbitrationId = readFrame.CanId, Data = readFrame.Data };
                }
                return null;
            }
        }
    }
}
