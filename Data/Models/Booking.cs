using System;
using System.Collections.Generic;

namespace SkyTicket.Data.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int FlightId { get; set; }

    public int PassengerId { get; set; }

    public string SeatNumber { get; set; } = null!;

    public DateTime BookingDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Flight Flight { get; set; } = null!;

    public virtual Passenger Passenger { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
