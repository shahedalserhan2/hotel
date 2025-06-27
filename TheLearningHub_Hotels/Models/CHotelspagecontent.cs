using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheLearningHub_Hotels.Models;

public partial class CHotelspagecontent
{
    public decimal Hotelspagecontentid { get; set; }

    public string? Projectname { get; set; }

    public string? Pagename { get; set; }

    public string? ImagepathTop { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }

    public string? Footerlocation { get; set; }

    public decimal? Footerphonenumber { get; set; }

    public string? Footeremail { get; set; }

    public decimal? Userloginid { get; set; }

    public virtual CUserlogin? Userlogin { get; set; }
}
