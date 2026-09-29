using System;
using System.Collections.Generic;

namespace SkyTicket.Data.Models;

public partial class Passenger
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateTime BirthDate { get; set; }

    public string PassportNumber { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
