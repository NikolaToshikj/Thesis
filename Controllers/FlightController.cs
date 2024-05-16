using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FlightController : ControllerBase
{
    private readonly FlightService _flightService;

    public FlightController(FlightService flightService)
    {
        _flightService = flightService;
    }

    [HttpGet("onewaytrip")]
    public async Task<IActionResult> GetOneWayTrip(
        [FromQuery] string from,
        [FromQuery] string to,
        [FromQuery] string date,
        [FromQuery] int adults,
        [FromQuery] int children,
        [FromQuery] int infants,
        [FromQuery] string cabinClass,
        [FromQuery] string currency)
    {
        var result = await _flightService.GetOneWayTripAsync(from, to, date, adults, children, infants, cabinClass, currency);

        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return StatusCode(500, "An error occurred while fetching data from FlightAPI.");
        }
    }
}
