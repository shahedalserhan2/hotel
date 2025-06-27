using System;
using System.Collections.Generic;

namespace TheLearningHub_Hotels.Models;

public partial class CTestimonial
{
    public decimal Testimonialid { get; set; }

    public string? TestimonialText { get; set; }

    public decimal? Rating { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? STATUS { get; set; }

    public string? ACTIONS { get; set; }

    public decimal? UserId { get; set; }

    public decimal? Hotelid { get; set; }

    public virtual CHotel? Hotel { get; set; }

    public virtual CUser? User { get; set; }
}
