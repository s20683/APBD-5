namespace Project5.Models;

public class ClientTripRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Pesel { get; set; }
    public int IdTrip { get; set; }
    public string TripName { get; set; }
    public DateTime? PaymentDate { get; set; }
    
    public Client ToClientDBO()
    {
        return new Client
        {
            FirstName = this.FirstName,
            LastName = this.LastName,
            Email = this.Email,
            Telephone = this.Telephone,
            Pesel = this.Pesel
        };
    }
}