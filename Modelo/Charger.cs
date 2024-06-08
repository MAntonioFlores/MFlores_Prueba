using System;
using System.Collections.Generic;

namespace Datos;

public partial class Charger
{
    public string Id { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CompanyId { get; set; }

    public virtual Company? Company { get; set; }
}
