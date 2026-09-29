using System;
using System.Collections.Generic;

namespace SkyTicket.Data.Models;

public partial class Aircraft
{
    public int Id { get; set; }

    public string Model { get; set; } = null!;

    public int SeatsCount { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
