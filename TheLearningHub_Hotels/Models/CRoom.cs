using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheLearningHub_Hotels.Models;

public partial class CRoom
{
    public decimal Roomid { get; set; }

    public decimal? Roomnumber { get; set; }

    public decimal? Roomcapacity { get; set; }

    public decimal? PricePerNight { get; set; }

    public string? Isavailable { get; set; }

    public decimal? Hotelid { get; set; }

    public string? Imagepath { get; set; }

    [NotMapped]
    public IFormFile ImageFile { get; set; }

    public virtual ICollection<CReservation> CReservations { get; set; } = new List<CReservation>();

    public virtual CHotel? Hotel { get; set; }
}
