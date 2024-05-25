using System.ComponentModel.DataAnnotations.Schema;

namespace Project5.Models;

public partial class CountryTrip
{
    public int IdCountry { get; set; }
    public int IdTrip { get; set; }

    [ForeignKey(nameof(IdCountry))]
    public virtual Country IdCountryNavigation { get; set; } = null!;

    [ForeignKey(nameof(IdTrip))]
    public virtual Trip IdTripNavigation { get; set; } = null!;
}