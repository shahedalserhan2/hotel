using System;
using System.Collections.Generic;

namespace TheLearningHub_Hotels.Models;

public partial class CService
{
    public decimal Serviceid { get; set; }

    public string? Servicename { get; set; }

    public string? Servicetext { get; set; }

    public decimal? Hotelid { get; set; }

    public virtual CHotel? Hotel { get; set; }
}
