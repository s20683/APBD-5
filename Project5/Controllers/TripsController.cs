using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5.Models;

namespace Project5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly TripContext _context;

    public TripsController(TripContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetTrips()
    {
        var trips = await _context.Trips
            .OrderByDescending(t => t.DateFrom)
            .Select(t => new
            {
                t.Name,
                t.Description,
                t.DateFrom,
                t.DateTo,
                t.MaxPeople,
                Countries = t.CountryTrips.Select(ct => new { ct.IdCountryNavigation.Name }).ToList(),
                Clients = t.ClientTrips.Select(ct => new { ct.IdClientNavigation.FirstName, ct.IdClientNavigation.LastName }).ToList()
            })
            .ToListAsync();

        return Ok(trips);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }

        var assignedTrips = await _context.ClientTrips
            .Where(ct => ct.IdClient == id)
            .AnyAsync();

        if (assignedTrips)
        {
            return BadRequest("Nie można usunąć klienta, ponieważ ma przypisane wycieczki.");
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}