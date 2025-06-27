using System;
using System.Collections.Generic;

namespace TheLearningHub_Hotels.Models;

public partial class CRole
{
    public decimal Roleid { get; set; }

    public string? Rolename { get; set; }

    public virtual ICollection<CUserlogin> CUserlogins { get; set; } = new List<CUserlogin>();
}
