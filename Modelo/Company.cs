using System;
using System.Collections.Generic;

namespace Datos;

public partial class Company
{
    public string CompanyId { get; set; } = null!;

    public string? CompanyName { get; set; }

    public virtual ICollection<Charger> Chargers { get; set; } = new List<Charger>();
}
