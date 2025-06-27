using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheLearningHub_Hotels.Models;

public partial class CTeam
{
    public decimal Teamid { get; set; }

    public string? TeamMembername { get; set; }

    public string? Position { get; set; }

    public string? Imagepath { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }
}
