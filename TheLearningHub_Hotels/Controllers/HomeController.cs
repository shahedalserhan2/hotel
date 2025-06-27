using MailKit.Search;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TheLearningHub_Hotels.Models;

namespace TheLearningHub_Hotels.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        private readonly EmailService _emailService;

        private readonly IWebHostEnvironment _webHostEnvironment;
    


        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment,EmailService emailService )
        {
            _emailService = emailService;
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(string searchQuery)
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

            // Initialize queries
            var hotels = _context.CHotels.AsQueryable();
            var testimonials = _context.CTestimonials.AsQueryable();
            var userLogins = _context.CUserlogins.AsQueryable();
            var rooms = _context.CRooms.AsQueryable();
            var services = _context.CServices.AsQueryable();
            var users = _context.CUsers.AsQueryable();

            // Apply search filter if searchQuery is provided
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                
        var lowerSearchQuery = searchQuery.ToLower();

                // Filter by hotel name with case-insensitive search
                hotels = hotels.Where(h => h.Hotelname.ToLower().Contains(lowerSearchQuery));

                // Get hotel IDs from filtered hotels
                var filteredHotelIds = hotels.Select(h => h.Hotelid).ToList();


            }

            // Create the join table with the filtered hotels
            var multiTable = from H in hotels
                             join R in rooms on H.Hotelid equals R.Hotelid
                             join T in testimonials on H.Hotelid equals T.Hotelid
                             join us in userLogins on T.UserId equals us.UserId
                             join s in services on H.Hotelid equals s.Hotelid
                             join u in users on T.UserId equals u.UserId
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
                hotels.ToList(), testimonials.ToList(), multiTable.ToList(), userLogins.ToList(), rooms.ToList(), services.ToList(), users.ToList());

            ViewBag.SearchQuery = searchQuery; // Pass the search query to the view

            return View(data);
        }

        public IActionResult Profile()
        {

            ViewBag.username = HttpContext.Session.GetString("name");
            int? custid = HttpContext.Session.GetInt32("custid");
            var Servicespagecontents = _context.CHomepagecontents
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

            if (!custid.HasValue)
            {
                return NotFound();
            }

            var user = _context.CUsers.SingleOrDefault(p => p.UserId == custid.Value);
            if (user == null)
            {
                return NotFound();
            }

            //ViewData["user"] = user;
            return View(user);
        }
        public IActionResult Edit()
        {
            ViewBag.username = HttpContext.Session.GetString("name");
            int? custid = HttpContext.Session.GetInt32("custid");

            if (!custid.HasValue)
            {
                return NotFound();
            }

            var user = _context.CUsers.SingleOrDefault(p => p.UserId == custid.Value);
            if (user == null)
            {
                return NotFound();
            }

            //ViewData["user"] = user;
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile([Bind("UserId,Fname,Lname,PhoneNumber,Email,ImageFile")] CUser cUser)
        {
            if (ModelState.IsValid)
            {
                if (cUser.ImageFile != null)
                {
                    // 1- get rootpath
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    string fileName = Guid.NewGuid().ToString() + "_" + cUser.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png
                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cUser.ImageFile.CopyToAsync(fileStream);
                    }

                    cUser.Imagepath = fileName;
                }

                try
                {
                    // Update the user information
                    _context.Update(cUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CUserExists(cUser.UserId))
                    {
                        return NotFound();
                    }

                }

                return RedirectToAction(nameof(Index));
            }

            // Return the view with the model if there are validation errors
            return View(cUser);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("UserId,Fname,Lname,PhoneNumber,Email,ImageFile")] CUser cUser)
        {


            if (ModelState.IsValid)
            {
                if (cUser.ImageFile != null)
                {
                    // 1- get rootpath
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    string fileName = Guid.NewGuid().ToString() + "_" + cUser.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png
                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cUser.ImageFile.CopyToAsync(fileStream);
                    }

                    cUser.Imagepath = fileName;
                }

                try
                {
                    // Update the user information
                    _context.Update(cUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CUserExists(cUser.UserId))
                    {
                        return NotFound();
                    }

                }

                return RedirectToAction(nameof(Index));
            }

            // Return the view with the model if there are validation errors
            return View(cUser);

        }






        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
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
        [HttpGet]
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
            int? custid = HttpContext.Session.GetInt32("custid");

            if (!custid.HasValue)
            {
                return NotFound();
            }

            var user = _context.CUsers.SingleOrDefault(p => p.UserId == custid.Value);
            if (user == null)
            {
                return NotFound();
            }

            var reservation = new CReservation
            {
                UserId = user.UserId,
              
            };
            return View(reservation);
        

    }






        public IActionResult Room(decimal hotelId)
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
            var rooms = _context.CRooms
         .Where(r => r.Hotelid == hotelId)
         .ToList();


            var testimonials = _context.CTestimonials.ToList();
            var userLogins = _context.CUserlogins.ToList();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Booking(CReservation cReservation)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Rooms = await _context.CRooms.ToListAsync();
                return View(cReservation);
            }

            int? custid = HttpContext.Session.GetInt32("custid");
            if (!custid.HasValue)
            {
                ModelState.AddModelError("", "User not found in session.");
                ViewBag.Rooms = await _context.CRooms.ToListAsync();
                return View(cReservation);
            }

            var room = await _context.CRooms.FindAsync(cReservation.Roomid);
            if (room == null)
            {
                ModelState.AddModelError("", "Room not found.");
                ViewBag.Rooms = await _context.CRooms.ToListAsync();
                return View(cReservation);
            }
            cReservation.UserId = custid.Value;

            cReservation.Toltalprice = room.PricePerNight * ((cReservation.CheckOutDate - cReservation.CheckInDate)?.Days ?? 1);
            _context.Add(cReservation);
            await _context.SaveChangesAsync();

            // Store the total price in session for use in the Bank action
            HttpContext.Session.SetString("TotalPrice", cReservation.Toltalprice.ToString());

            // Redirect to the Bank action
            return RedirectToAction("Bank");
        }

        [HttpGet]
        public IActionResult Bank()
        {
            var Servicespagecontents = _context.CHomepagecontents
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
            ViewBag.username = HttpContext.Session.GetString("name");
            int? custid = HttpContext.Session.GetInt32("custid");

            if (!custid.HasValue)
            {
                return NotFound();
            }

            // Retrieve the total price from the session
            var totalPriceString = HttpContext.Session.GetString("TotalPrice");
            if (decimal.TryParse(totalPriceString, out var totalPrice))
            {
                ViewBag.TotalPrice = totalPrice;
            }
            else
            {
                ViewBag.TotalPrice = 0m; // Default value if not found or parse fails
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Bank(CBank CBank, string Creditcardnumber, DateTime? Creditcardexp)
        {
            if (!ModelState.IsValid)
            {
                return View(CBank);
            }

            // Retrieve the total price from the session
            var totalPriceString = HttpContext.Session.GetString("TotalPrice");
            if (!decimal.TryParse(totalPriceString, out var totalPrice))
            {
                ModelState.AddModelError("", "Total price not found.");
                return View(CBank);
            }

            int? custid = HttpContext.Session.GetInt32("custid");
            if (!custid.HasValue)
            {
                ModelState.AddModelError("", "User not found in session.");
                return View(CBank);
            }

            // Validate credit card number and expiration date
            if (string.IsNullOrWhiteSpace(Creditcardnumber) || !Creditcardexp.HasValue)
            {
                ModelState.AddModelError("", "Credit card number and expiration date are required.");
                return View(CBank);
            }

            // Find the bank account matching the credit card number and expiration date
            var bankAccount = await _context.CBanks
                .FirstOrDefaultAsync(b => b.Creditcardnumber == decimal.Parse(Creditcardnumber) && b.Creditcardexp == Creditcardexp);

            if (bankAccount == null)
            {
                ModelState.AddModelError("", "No matching bank account found.");
                return View(CBank);
            }

            if (bankAccount.MONEY < totalPrice)
            {
                ModelState.AddModelError("", "Insufficient funds.");
                return View(CBank);
            }
            bankAccount.MONEY -= totalPrice;
            _context.CBanks.Update(bankAccount);
            await _context.SaveChangesAsync();
            // Prepare the last four digits of the credit card for the email
            var creditCardNumberString = Creditcardnumber.ToString(); // Convert decimal to string
            var lastFourDigits = creditCardNumberString.Length >= 4
                ? creditCardNumberString.Substring(creditCardNumberString.Length - 4)
                : "****"; // Default to **** if less than 4 digits

            // Send the invoice email
            await _emailService.SendEmailAsync("shahedserhan2001@outlook.com", "Your Invoice", $@"
    <h2>Invoice</h2>
    <p>Thank you for your payment!</p>
    <p><strong>Credit Card Number:</strong> **** **** **** {lastFourDigits}</p>
    <p><strong>Expiration Date:</strong> {Creditcardexp?.ToString("yyyy-MM-dd")}</p>
    <p><strong>Total Amount Charged:</strong> {totalPrice}</p>
    <p><strong>Remaining Balance:</strong> {bankAccount.MONEY}</p>
");

            // Optionally, clear the total price from the session if it's no longer needed
            HttpContext.Session.Remove("TotalPrice");

            // Redirect to the index or confirmation page
            return RedirectToAction("Index");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(CTestimonial cTestimonial)
        {
            int? custid = HttpContext.Session.GetInt32("custid");
            if (!custid.HasValue)
            {
                ModelState.AddModelError("", "User not found in session.");
                ViewBag.Rooms = await _context.CRooms.ToListAsync();
               
            }
            cTestimonial.UserId = custid.Value;
            cTestimonial.CreatedAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                // إضافة التستومنيال إلى قاعدة البيانات
                _context.Add(cTestimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // إعادة التوجيه إلى الصفحة الرئيسية أو صفحة أخرى
            }

          
            return View(cTestimonial);
        }











        public IActionResult Service()
        {
            ViewData["numberOfHotel"] = _context.CHotels.Count();
            ViewData["numberOfRoom"] = _context.CRooms.Count();
            ViewData["numberOfClients"] = _context.CUserlogins.Where(x => x.Roleid == 1).Count();
            var Servicespagecontents = _context.CHomepagecontents
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
        [HttpGet]
        public IActionResult Testimonial(decimal? id)
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
            //var hotels = _context.CHotels.ToList();
            //var testimonials = _context.CTestimonials.ToList();
            //var userLogins = _context.CUserlogins.ToList();
            //var rooms = _context.CRooms.ToList();
            //var services = _context.CServices.ToList();
            //var user = _context.CUsers.ToList();
            //var multiTable = from H in hotels
            //                 join R in rooms on H.Hotelid equals R.Hotelid
            //                 join T in testimonials on H.Hotelid equals T.Hotelid
            //                 join us in userLogins on T.UserId equals us.UserId
            //                 join s in services on H.Hotelid equals s.Hotelid
            //                 join u in user on T.UserId equals u.UserId
            //                 select new JoinTable
            //                 {
            //                     hotel = H,
            //                     room = R,
            //                     testimonial = T,
            //                     userlogin = us,
            //                     service = s,
            //                     user = u
            //                 };

            //var data = Tuple.Create<IEnumerable<CHotel>, IEnumerable<CTestimonial>, IEnumerable<JoinTable>, IEnumerable<CUserlogin>, IEnumerable<CRoom>, IEnumerable<CService>, IEnumerable<CUser>>(
            //    hotels, testimonials, multiTable, userLogins, rooms, services, user);


            //return View(data);
        }


       [ HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Testimonial(CTestimonial cTestimonial)
        {
            int? custid = HttpContext.Session.GetInt32("custid");
            if (!custid.HasValue)
            {
                ModelState.AddModelError("", "User not found in session.");
                ViewBag.Rooms = await _context.CRooms.ToListAsync();
                return View(cTestimonial);
            }
            cTestimonial.UserId = custid.Value;
            cTestimonial.CreatedAt = DateTime.Now;
            cTestimonial.STATUS = "Pending";
            if (ModelState.IsValid)
            {
                // إضافة التستومنيال إلى قاعدة البيانات
                _context.Add(cTestimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // إعادة التوجيه إلى الصفحة الرئيسية أو صفحة أخرى
            }

            ViewBag.Hotels = _context.CHotels.ToList();
            return View(cTestimonial);
        }

        // POST: Testimonial/Create



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        private bool CUserExists(decimal id)
        {
            return (_context.CUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

    }

}

