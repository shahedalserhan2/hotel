using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheLearningHub_Hotels.Models;

public partial class CUser
{
    public decimal UserId { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public int? PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public string? Imagepath { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }

    public virtual ICollection<CReservation> CReservations { get; set; } = new List<CReservation>();

    public virtual ICollection<CTestimonial> CTestimonials { get; set; } = new List<CTestimonial>();

    public virtual ICollection<CUserlogin> CUserlogins { get; set; } = new List<CUserlogin>();
}
