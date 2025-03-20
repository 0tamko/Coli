using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ColiTool.CanBus;  
using ColiTool.Common;  

namespace ColiTool.WebAPI.Services
{

    public class CanBackgroundService : BackgroundService
    {
        private readonly ILogger<CanBackgroundService> _logger;
        private readonly CanBusService _canBusService;

        public CanBackgroundService(
            CanBusService canBusService,
            ILogger<CanBackgroundService> logger)
        {
            _canBusService = canBusService;
            _logger = logger;
        }

        /// <summary>
        /// Táto metóda sa spustí po štarte aplikácie a beží na pozadí až do zastavenia.
        /// </summary>
        /// <param name="stoppingToken">Token, ktorý signalizuje zastavenie služby</param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("CanBackgroundService is starting.");

            // Uisti sa, že CanBusService je inicializovaný (napr. otvorenie SocketCAN alebo simulácia)
            _canBusService.Initialize();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                    var message = await _canBusService.ReceiveAsync(100);

                    if (message != null)
                    {
                        _logger.LogInformation(
                            $"[CanBackgroundService] Received CAN frame. ID=0x{message.ArbitrationId:X}, Data={BitConverter.ToString(message.Data)}"
                        );
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while receiving CAN message.");
                }

                await Task.Delay(10, stoppingToken);
            }

            _logger.LogInformation("CanBackgroundService is stopping.");
        }
    }
}
