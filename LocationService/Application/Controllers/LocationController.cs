// LocationController.cs
using LocationService.Domain.Aggregates.LocationAggregate;
using LocationService.Domain.Hubs;
using LocationService.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[Route("api/location")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly IHubContext<LocationHub> _hubContext;
    private readonly ApplicationDbContext _context;
    public LocationController(IHubContext<LocationHub> hubContext, ApplicationDbContext context)
    {
        _hubContext = hubContext;
        _context = context;
    }

    [HttpPost]
    public IActionResult ReceiveLocation([FromBody] Location location)
    {
        if (location == null || string.IsNullOrEmpty(location.Latitude) || string.IsNullOrEmpty(location.Longitude))
        {
            return BadRequest("Invalid location data.");
        }
        //Save to the database
        _context.Locations.Add(location);
        _context.SaveChanges();

        // Send location to connected clients using SignalR
        _hubContext.Clients.All.SendAsync("ReceiveLocation", location.Latitude, location.Longitude);
        
        return Ok();
    }
}
