using Microsoft.AspNetCore.SignalR;

namespace ColiTool.WebAPI.Hubs
{
    public class CanBusHub : Hub
    {
        public async Task OnMessageRecieved(string message)
        {
            await Clients.All.SendAsync("MessageRecieved", message);
        }

        public async Task OnStatusChanged(string status)
        {
            await Clients.All.SendAsync("StatusChanged", status);
        }
    }
}
