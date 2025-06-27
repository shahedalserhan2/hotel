using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text.RegularExpressions;
using TheLearningHub_Hotels.Models;

namespace TheLearningHub_Hotels.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(ModelContext context, ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            ViewData["numberOfHotel"] = _context.CHotels.Count();
            ViewData["numberOfRooms"] = _context.CRooms.Count();
            ViewData["numberOfClients"] = _context.CUserlogins.Where(x => x.Roleid == 1).Count();
            ViewData["numberOfRoom"] = _context.CRooms.Where(x => x.Isavailable == "true").Count();
            ViewData["booked rooms"] = _context.CReservations.Count();
            var hotels = _context.CHotels.ToList();
            var testimonials = _context.CTestimonials.ToList();
            var userLogins = _context.CUserlogins.ToList();
            var rooms = _context.CRooms.ToList();
            var services = _context.CServices.ToList();
            var user = _context.CUsers.ToList();
            var reservation = _context.CReservations.ToList();



            var multiTable = from H in hotels
                             join R in rooms on H.Hotelid equals R.Hotelid
                             join T in testimonials on H.Hotelid equals T.Hotelid
                             join us in userLogins on T.UserId equals us.UserId
                             join s in services on H.Hotelid equals s.Hotelid
                             join u in user on T.UserId equals u.UserId
                             join v in reservation on R.Roomid equals v.Roomid

                             select new JoinTable
                             {
                                 hotel = H,
                                 room = R,
                                 testimonial = T,
                                 userlogin = us,
                                 service = s,
                                 user = u,

                             };
            var data = Tuple.Create<IEnumerable<CHotel>, IEnumerable<CTestimonial>, IEnumerable<JoinTable>, IEnumerable<CUserlogin>, IEnumerable<CRoom>, IEnumerable<CService>, IEnumerable<CUser>>(
                    hotels, testimonials, multiTable, userLogins, rooms, services, user);

            return View(data);

        }

        public async Task<IActionResult> UpdateTestimonialStatus(decimal id, string status)
        {
            var testimonial = await _context.CTestimonials.FindAsync(id);
            if (testimonial != null)
            {
                testimonial.STATUS = status;
                _context.Update(testimonial);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "CTestimonials"); // إعادة التوجيه إلى صفحة العرض أو أي صفحة أخرى
        }




        public IActionResult Profile()
        {
            ViewBag.username = HttpContext.Session.GetString("adminname");
            int? adminId = HttpContext.Session.GetInt32("adminid");

            if (!adminId.HasValue)
            {
                return NotFound();
            }

            var user = _context.CUsers.SingleOrDefault(p => p.UserId == adminId.Value);
            if (user == null)
            {
                return NotFound();
            }

            //ViewData["user"] = user;
            return View(user);
        }

        public IActionResult Edit()
        {
            ViewBag.username = HttpContext.Session.GetString("adminname");
            int? adminId = HttpContext.Session.GetInt32("adminid");

            if (!adminId.HasValue)
            {
                return NotFound();
            }

            var user = _context.CUsers.SingleOrDefault(p => p.UserId == adminId.Value);
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

        private bool CUserExists(decimal id)
        {
            return (_context.CUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
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
        [HttpGet]
        public IActionResult Report()
        {
            ViewBag.username = HttpContext.Session.GetString("adminname");
            int? adminId = HttpContext.Session.GetInt32("adminid");
            // Object of the user (all user's data)
            var user = _context.CUsers.Where(x => x.UserId == adminId).SingleOrDefault();
            ViewBag.Fname = user.Fname;
            ViewBag.Lname = user.Lname;
            ViewBag.UserImage = user.Imagepath;

            var data = _context.CReservations.Include(u => u.User)
                                            .Include(r => r.Room)
                                            .ThenInclude(h => h.Hotel)
                                            .ToList();

            //   ViewBag.Benefits = data.Sum(r => r.Toltalprice);
            //   var reservationDates = data
            //.Where(r => r.CheckInDate.HasValue)
            //.GroupBy(r => r.CheckInDate.Value.Date)
            //.Select(g => new { Date = g.Key, Count = g.Count() })
            //.OrderBy(g => g.Date)
            //.ToList();

            var revenuePerMonth = data
                .Where(r => r.CheckInDate.HasValue)
                .GroupBy(r => r.CheckInDate.Value.Month)
                .Select(g => new { Month = g.Key, Revenue = g.Sum(r => r.Toltalprice.HasValue ? r.Toltalprice.Value : 0) })
                .OrderBy(g => g.Month)
                .ToList();

            //ViewBag.ReservationDates = reservationDates.Select(r => r.Date.ToString("yyyy-MM-dd")).ToList();
            //ViewBag.ReservationCounts = reservationDates.Select(r => r.Count).ToList();
            ViewBag.RevenuePerMonth = revenuePerMonth.Select(r => r.Revenue).ToList();
            ViewBag.Months = revenuePerMonth.Select(r => System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(r.Month)).ToList();

            return View(data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Report(DateTime? CheckinDate, DateTime? CheckoutDate, int? Month, int? Year)
        {
            var data = _context.CReservations
                               .Include(u => u.User)
                               .Include(r => r.Room)
                               .ThenInclude(h => h.Hotel);

            // تقرير عام إذا لم يتم توفير أي تواريخ
            if (CheckinDate == null && CheckoutDate == null && Month == null && Year == null)
            {
                ViewBag.Benefits = data.Sum(r => r.Toltalprice);
                return View(await data.ToListAsync());
            }

            // تقرير حسب تاريخ البداية فقط
            if (CheckinDate != null && CheckoutDate == null)
            {
                var result = await data.Where(x => x.CheckInDate.HasValue &&
                                                   x.CheckInDate.Value.Date >= CheckinDate.Value.Date)
                                       .ToListAsync();
                ViewBag.Benefits = result.Sum(r => r.Toltalprice);
                return View(result);
            }

            // تقرير حسب تاريخ النهاية فقط
            if (CheckinDate == null && CheckoutDate != null)
            {
                var result = await data.Where(x => x.CheckInDate.HasValue &&
                                                   x.CheckInDate.Value.Date <= CheckoutDate.Value.Date)
                                       .ToListAsync();
                ViewBag.Benefits = result.Sum(r => r.Toltalprice);
                return View(result);
            }

            // تقرير حسب تاريخ البداية والنهاية
            if (CheckinDate != null && CheckoutDate != null)
            {
                var result = await data.Where(x => x.CheckInDate.HasValue &&
                                                   x.CheckInDate.Value.Date >= CheckinDate.Value.Date &&
                                                   x.CheckInDate.Value.Date <= CheckoutDate.Value.Date)
                                       .ToListAsync();
                ViewBag.Benefits = result.Sum(r => r.Toltalprice);
                return View(result);
            }

            // تقرير حسب الشهر والسنة
            if (Month.HasValue && Year.HasValue)
            {
                var result = await data.Where(x => x.CheckInDate.HasValue &&
                                                   x.CheckInDate.Value.Month == Month.Value &&
                                                   x.CheckInDate.Value.Year == Year.Value)
                                       .ToListAsync();
                ViewBag.Benefits = result.Sum(r => r.Toltalprice);
                return View(result);
            }

            // إذا لم تنطبق أي من الحالات السابقة، عرض رسالة خطأ أو حالة فارغة
            ViewBag.Benefits = 0;
            return View(await data.ToListAsync());
        }
      

    }
}










