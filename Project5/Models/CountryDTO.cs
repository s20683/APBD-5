using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project5.Models;

public partial class CountryDTO
{
    public string Name { get; set; } = null!;
}
