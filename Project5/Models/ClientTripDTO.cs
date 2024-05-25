namespace Project5.Models;

public class ClientTripDTO
{
    public int IdClient { get; set; }

    public int IdTrip { get; set; }

    public DateTime RegisteredAt { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual ClientDTO IdClientDtoNavigation { get; set; } = null!;

    public virtual TripDTO IdTripDtoNavigation { get; set; } = null!;
}