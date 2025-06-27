using System;
using System.Collections.Generic;

namespace TheLearningHub_Hotels.Models;

public partial class CReservation
{
    public decimal Reservationsid { get; set; }

    public DateTime? CheckInDate { get; set; }

    public DateTime? CheckOutDate { get; set; }

    public decimal? Toltalprice { get; set; }

    public string? Invoicepdf { get; set; }

    public decimal? UserId { get; set; }

    public decimal? Roomid { get; set; }

    public virtual CRoom? Room { get; set; }

    public virtual CUser? User { get; set; }
}
