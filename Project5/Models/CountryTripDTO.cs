using System.ComponentModel.DataAnnotations.Schema;

namespace Project5.Models;

public partial class CountryTripDTO
{
    public int IdCountry { get; set; }
    public int IdTrip { get; set; }
    public virtual CountryDTO IdCountryDtoNavigation { get; set; } = null!;
    public virtual TripDTO IdTripDtoNavigation { get; set; } = null!;
}