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
    
    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, ClientTripRequestDto clientRequest)
    {
        var trip = await _context.Trips.FindAsync(idTrip);
        if (trip == null)
        {
            return NotFound("Wycieczka nie istnieje.");
        }

        var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == clientRequest.Pesel);
        if (existingClient == null)
        {
            existingClient = clientRequest.ToClientDBO();
            _context.Clients.Add(existingClient);
            await _context.SaveChangesAsync();
        }

        var existingAssignment = await _context.ClientTrips
            .AnyAsync(ct => ct.IdClient == existingClient.IdClient && ct.IdTrip == idTrip);
        if (existingAssignment)
        {
            return BadRequest("Klient jest już zapisany na tę wycieczkę.");
        }

        var clientTrip = new ClientTrip
        {
            IdClient = existingClient.IdClient,
            IdTrip = idTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = clientRequest.PaymentDate
        };
        _context.ClientTrips.Add(clientTrip);
        await _context.SaveChangesAsync();

        return Ok("Klient został pomyślnie przypisany do wycieczki.");
    }
}