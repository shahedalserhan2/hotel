using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheLearningHub_Hotels.Models;

public partial class CHotel
{
    public decimal Hotelid { get; set; }

    public string? Hotelname { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public string? Imagepath { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }

    public virtual ICollection<CRoom> CRooms { get; set; } = new List<CRoom>();

    public virtual ICollection<CService> CServices { get; set; } = new List<CService>();

    public virtual ICollection<CTestimonial> CTestimonials { get; set; } = new List<CTestimonial>();
}
