using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheLearningHub_Hotels.Models;

public partial class CHomepagecontent
{
    public decimal Homepagecontent { get; set; }

    public string? Projectname { get; set; }

    public string? Pagename { get; set; }

    public string? ImagepathTop1 { get; set; }

    public string? ImagepathTop2 { get; set; }

    [NotMapped]
    public IFormFile ImageFile1 { get; set; }
    [NotMapped]
    public IFormFile ImageFile2 { get; set; }

    public string? WelcomeText { get; set; }

    public string? Footerlocation { get; set; }

    public decimal? Footerphonenumber { get; set; }

    public string? Footeremail { get; set; }

    public decimal? Userloginid { get; set; }

    public virtual CUserlogin? Userlogin { get; set; }
}
