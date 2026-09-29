using System;
using System.Collections.Generic;

namespace SkyTicket.Data.Models;

public partial class Flight
{
    public int Id { get; set; }

    public string FlightNumber { get; set; } = null!;

    public int DepartureAirportId { get; set; }

    public int ArrivalAirportId { get; set; }

    public int AircraftId { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public decimal Price { get; set; }

    public virtual Aircraft Aircraft { get; set; } = null!;

    public virtual Airport ArrivalAirport { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Airport DepartureAirport { get; set; } = null!;
}