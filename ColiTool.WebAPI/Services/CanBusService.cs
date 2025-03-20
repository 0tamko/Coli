using Microsoft.AspNetCore.SignalR;
using ColiTool.WebAPI.Hubs;
using Microsoft.Extensions.Logging;
using System;

namespace ColiTool.WebAPI.Services
{
    public class CanBusService
    {
        private readonly IHubContext<CanBusHub> _hubContext;
        private readonly ILogger<CanBusService> _logger;

        public CanBusService(IHubContext<CanBusHub> hubContext, ILogger<CanBusService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public void OnCanMessageReceived(string message)
        {
            _logger.LogInformation("Sending message: {Message}", message);
            _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
        }

        public void OnCanStatusChanged(string status)
        {
            _logger.LogInformation("Sending status: {Status}", status);
            _hubContext.Clients.All.SendAsync("ReceiveStatus", status);
        }

        // Simulate CAN Bus events
        public void SimulateCanBusEvents()
        {
            _logger.LogInformation("Simulating CAN Bus events");

            // Simulate receiving a message
            OnCanMessageReceived("Test CAN message");

            // Simulate status change
            OnCanStatusChanged("Status changed");
        }
    }
}

