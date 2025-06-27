using Microsoft.AspNetCore.Mvc;
using TheLearningHub_Hotels.Models;

namespace TheLearningHub_Hotels.Controllers
{
    public class GuestController : Controller
    {
        private readonly ModelContext _context;

        public GuestController(ModelContext context)
        {

            _context = context;
        }
        public IActionResult Index()
        {

            ViewData["numberOfHotel"] = _context.CHotels.Count();
            ViewData["numberOfRoom"] = _context.CRooms.Count();
            ViewData["numberOfClients"] = _context.CUserlogins.Where(x => x.Roleid == 1).Count();
            var homepageContent = _context.CHomepagecontents
       .Select(h => new
       {
           h.WelcomeText,
           h.Projectname,
           h.Pagename,
           h.Footerlocation,
           h.Footerphonenumber,
           h.Footeremail
       })
       .FirstOrDefault();
            if (homepageContent != null)
            {
                ViewBag.WelcomeText = homepageContent.WelcomeText ?? "Welcome to Hotelier";
                ViewBag.ProjectName = homepageContent.Projectname ?? "Hotelier";
                ViewBag.PageName = homepageContent.Pagename ?? "Home";
                ViewBag.FooterLocation = homepageContent.Footerlocation ?? "Default Location";
                ViewBag.FooterPhoneNumber = homepageContent.Footerphonenumber ?? 0000000000;
                ViewBag.FooterEmail = homepageContent.Footeremail ?? "info@example.com";
            }
            var hotels = _context.CHotels.ToList();
            var testimonials = _context.CTestimonials.ToList();
            var userLogins = _context.CUserlogins.ToList();
            var rooms = _context.CRooms.ToList();
            var services = _context.CServices.ToList();
            var user = _context.CUsers.ToList();
            var multiTable = from H in hotels
                             join R in rooms on H.Hotelid equals R.Hotelid
                             join T in testimonials on H.Hotelid equals T.Hotelid
                             join us in userLogins on T.UserId equals us.UserId
                             join s in services on H.Hotelid equals s.Hotelid
                             join u in user on T.UserId equals u.UserId
                             select new JoinTable
                             {
                                 hotel = H,
                                 room = R,
                                 testimonial = T,
                                 userlogin = us,
                                 service = s,
                                 user = u
                             };

            var data = Tuple.Create<IEnumerable<CHotel>, IEnumerable<CTestimonial>, IEnumerable<JoinTable>, IEnumerable<CUserlogin>, IEnumerable<CRoom>, IEnumerable<CService>, IEnumerable<CUser>>(
                hotels, testimonials, multiTable, userLogins, rooms, services, user);

            return View(data);


            //    var homepageContent = _context.CHomepagecontents.FirstOrDefault();
            //    ViewData["WelcomeText"] = homepageContent != null ? homepageContent.WelcomeText : "Welcome to Hotelier";

            //    return View();
            //

        }
        public IActionResult About()
        {

            ViewData["numberOfHotel"] = _context.CHotels.Count();
            ViewData["numberOfRoom"] = _context.CRooms.Count();
            ViewData["numberOfClients"] = _context.CUserlogins.Where(x => x.Roleid == 1).Count();
            var hotels = _context.CHotels.ToList();
            var testimonials = _context.CTestimonials.ToList();
            var userLogins = _context.CUserlogins.ToList();
            var rooms = _context.CRooms.ToList();
            var services = _context.CServices.ToList();
            var user = _context.CUsers.ToList();
            var multiTable = from H in hotels
                             join R in rooms on H.Hotelid equals R.Hotelid
                             join T in testimonials on H.Hotelid equals T.Hotelid
                             join us in userLogins on T.UserId equals us.UserId
                             join s in services on H.Hotelid equals s.Hotelid
                             join u in user on T.UserId equals u.UserId
                             select new JoinTable
                             {
                                 hotel = H,
                                 room = R,
                                 testimonial = T,
                                 userlogin = us,
                                 service = s,
                                 user = u
                             };

            var data = Tuple.Create<IEnumerable<CHotel>, IEnumerable<CTestimonial>, IEnumerable<JoinTable>, IEnumerable<CUserlogin>, IEnumerable<CRoom>, IEnumerable<CService>, IEnumerable<CUser>>(
                hotels, testimonials, multiTable, userLogins, rooms, services, user);

            return View(data);

        }
        public IActionResult Room()
        {


            ViewData["numberOfHotel"] = _context.CHotels.Count();
            ViewData["numberOfRoom"] = _context.CRooms.Count();
            ViewData["numberOfClients"] = _context.CUserlogins.Where(x => x.Roleid == 1).Count();
            var homepageContent = _context.CHomepagecontents
       .Select(h => new
       {
           h.WelcomeText,
           h.Projectname,
           h.Pagename,
           h.Footerlocation,
           h.Footerphonenumber,
           h.Footeremail
       })
       .FirstOrDefault();
            if (homepageContent != null)
            {
                ViewBag.WelcomeText = homepageContent.WelcomeText ?? "Welcome to Hotelier";
                ViewBag.ProjectName = homepageContent.Projectname ?? "Hotelier";
                ViewBag.PageName = homepageContent.Pagename ?? "Home";
                ViewBag.FooterLocation = homepageContent.Footerlocation ?? "Default Location";
                ViewBag.FooterPhoneNumber = homepageContent.Footerphonenumber ?? 0000000000;
                ViewBag.FooterEmail = homepageContent.Footeremail ?? "info@example.com";
            }
            var hotels = _context.CHotels.ToList();
            var testimonials = _context.CTestimonials.ToList();
            var userLogins = _context.CUserlogins.ToList();
            var rooms = _context.CRooms.ToList();
            var services = _context.CServices.ToList();
            var user = _context.CUsers.ToList();
            var multiTable = from H in hotels
                             join R in rooms on H.Hotelid equals R.Hotelid
                             join T in testimonials on H.Hotelid equals T.Hotelid
                             join us in userLogins on T.UserId equals us.UserId
                             join s in services on H.Hotelid equals s.Hotelid
                             join u in user on T.UserId equals u.UserId
                             select new JoinTable
                             {
                                 hotel = H,
                                 room = R,
                                 testimonial = T,
                                 userlogin = us,
                                 service = s,
                                 user = u
                             };

            var data = Tuple.Create<IEnumerable<CHotel>, IEnumerable<CTestimonial>, IEnumerable<JoinTable>, IEnumerable<CUserlogin>, IEnumerable<CRoom>, IEnumerable<CService>, IEnumerable<CUser>>(
                hotels, testimonials, multiTable, userLogins, rooms, services, user);

            return View(data);

        }

        public IActionResult Contact()
        {
            var homepageContent = _context.CContactpagecontents
       .Select(h => new
       {

           h.Projectname,
           h.Pagename,
           h.Footerlocation,
           h.Footerphonenumber,
           h.Footeremail,
           h.Generalemail,
           h.Technicalemail,
           h.Bookingemail


       })
       .FirstOrDefault();
            if (homepageContent != null)
            {

                ViewBag.ProjectName = homepageContent.Projectname ?? "Hotelier";
                ViewBag.PageName = homepageContent.Pagename ?? "Home";
                ViewBag.FooterLocation = homepageContent.Footerlocation ?? "Default Location";
                ViewBag.FooterPhoneNumber = homepageContent.Footerphonenumber ?? 0000000000;
                ViewBag.FooterEmail = homepageContent.Footeremail ?? "info@example.com";
                ViewBag.Bookingemail = homepageContent.Bookingemail ?? "info@example.com";
                ViewBag.Generalemail = homepageContent.Generalemail ?? "info@example.com";
                ViewBag.Technicalemail = homepageContent.Technicalemail ?? "info@example.com";

            }
            return View();
        }
      
        public IActionResult Service()
        {
            ViewData["numberOfHotel"] = _context.CHotels.Count();
            ViewData["numberOfRoom"] = _context.CRooms.Count();
            ViewData["numberOfClients"] = _context.CUserlogins.Where(x => x.Roleid == 1).Count();
            var Servicespagecontents = _context.CServicespagecontents
      .Select(h => new
      {

          h.Projectname,
          h.Pagename,
          h.Footerlocation,
          h.Footerphonenumber,
          h.Footeremail
      })
      .FirstOrDefault();
            if (Servicespagecontents != null)
            {

                ViewBag.ProjectName = Servicespagecontents.Projectname ?? "Hotelier";
                ViewBag.PageName = Servicespagecontents.Pagename ?? "Home";
                ViewBag.FooterLocation = Servicespagecontents.Footerlocation ?? "Default Location";
                ViewBag.FooterPhoneNumber = Servicespagecontents.Footerphonenumber ?? 0000000000;
                ViewBag.FooterEmail = Servicespagecontents.Footeremail ?? "info@example.com";
            }
            var hotels = _context.CHotels.ToList();
            var testimonials = _context.CTestimonials.ToList();
            var userLogins = _context.CUserlogins.ToList();
            var rooms = _context.CRooms.ToList();
            var services = _context.CServices.ToList();
            var user = _context.CUsers.ToList();
            var multiTable = from H in hotels
                             join R in rooms on H.Hotelid equals R.Hotelid
                             join T in testimonials on H.Hotelid equals T.Hotelid
                             join us in userLogins on T.UserId equals us.UserId
                             join s in services on H.Hotelid equals s.Hotelid
                             join u in user on T.UserId equals u.UserId
                             select new JoinTable
                             {
                                 hotel = H,
                                 room = R,
                                 testimonial = T,
                                 userlogin = us,
                                 service = s,
                                 user = u
                             };

            var data = Tuple.Create<IEnumerable<CHotel>, IEnumerable<CTestimonial>, IEnumerable<JoinTable>, IEnumerable<CUserlogin>, IEnumerable<CRoom>, IEnumerable<CService>, IEnumerable<CUser>>(
                hotels, testimonials, multiTable, userLogins, rooms, services, user);

            return View(data);
        }
        public IActionResult team()
        {
            var team = _context.CTeams.ToList();
            return View(team);

        }

        public IActionResult Testimonial()
        {
            ViewBag.Hotels = _context.CHotels.ToList();


            var homepageContent = _context.CTestimonialpagecontents
                .Select(h => new
                {

                    h.Projectname,
                    h.Pagename,
                    h.Footerlocation,
                    h.Footerphonenumber,
                    h.Footeremail
                })
                .FirstOrDefault();

            if (homepageContent != null)
            {

                ViewBag.ProjectName = homepageContent.Projectname ?? "Hotelier";
                ViewBag.PageName = homepageContent.Pagename ?? "Home";
                ViewBag.FooterLocation = homepageContent.Footerlocation ?? "Default Location";
                ViewBag.FooterPhoneNumber = homepageContent.Footerphonenumber ?? 0000000000;
                ViewBag.FooterEmail = homepageContent.Footeremail ?? "info@example.com";
            }
            return View();
        }
          
            public IActionResult Booking()
            {
                var homepageContent = _context.CBookingpagecontents
             .Select(h => new
             {

                 h.Projectname,
                 h.Pagename,
                 h.Footerlocation,
                 h.Footerphonenumber,
                 h.Footeremail
             })
             .FirstOrDefault();
                if (homepageContent != null)
                {
                    ViewBag.ProjectName = homepageContent.Projectname ?? "Hotelier";
                    ViewBag.PageName = homepageContent.Pagename ?? "Home";
                    ViewBag.FooterLocation = homepageContent.Footerlocation ?? "Default Location";
                    ViewBag.FooterPhoneNumber = homepageContent.Footerphonenumber ?? 0000000000;
                    ViewBag.FooterEmail = homepageContent.Footeremail ?? "info@example.com";
                }
                ViewBag.Rooms = _context.CRooms.ToList();
                ViewBag.username = HttpContext.Session.GetString("name");
                return View();
            }
        }
    }



        
