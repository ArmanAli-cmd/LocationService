using LocationService.Domain.Aggregates.LocationAggregate;
using Microsoft.AspNetCore.SignalR;

namespace LocationService.Domain.Hubs
{
    public class LocationHub : Hub
    {
        public async Task SendLocation(string latitude, string longitude)
        {
            await Clients.All.SendAsync("ReceiveLocation", latitude, longitude);
        }
    }
}
