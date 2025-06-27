using System;
using System.Collections.Generic;

namespace TheLearningHub_Hotels.Models;

public partial class CUserlogin
{
    public decimal Userloginid { get; set; }

    public string? Username { get; set; }

    public string? Passwordd { get; set; }

    public decimal? Roleid { get; set; }

    public decimal? UserId { get; set; }

    public virtual ICollection<CAboutpagecontent> CAboutpagecontents { get; set; } = new List<CAboutpagecontent>();

    public virtual ICollection<CBookingpagecontent> CBookingpagecontents { get; set; } = new List<CBookingpagecontent>();

    public virtual ICollection<CContactpagecontent> CContactpagecontents { get; set; } = new List<CContactpagecontent>();

    public virtual ICollection<CHomepagecontent> CHomepagecontents { get; set; } = new List<CHomepagecontent>();

    public virtual ICollection<CHotelspagecontent> CHotelspagecontents { get; set; } = new List<CHotelspagecontent>();

    public virtual ICollection<CRoomspagecontent> CRoomspagecontents { get; set; } = new List<CRoomspagecontent>();

    public virtual ICollection<CServicespagecontent> CServicespagecontents { get; set; } = new List<CServicespagecontent>();

    public virtual ICollection<CTeampagecontent> CTeampagecontents { get; set; } = new List<CTeampagecontent>();

    public virtual ICollection<CTestimonialpagecontent> CTestimonialpagecontents { get; set; } = new List<CTestimonialpagecontent>();

    public virtual CRole? Role { get; set; }

    public virtual CUser? User { get; set; }
}
