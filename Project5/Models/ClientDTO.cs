using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project5.Models;

public partial class ClientDTO
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Pesel { get; set; } = null!;
    
    public Client ToDBO()
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
