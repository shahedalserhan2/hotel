using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheLearningHub_Hotels.Models;

namespace TheLearningHub_Hotels.Controllers
{
    public class CBookingpagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public CBookingpagecontentsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: CBookingpagecontents
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.CBookingpagecontents.Include(c => c.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: CBookingpagecontents/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CBookingpagecontents == null)
            {
                return NotFound();
            }

            var cBookingpagecontent = await _context.CBookingpagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Bookingpagecontentid == id);
            if (cBookingpagecontent == null)
            {
                return NotFound();
            }

            return View(cBookingpagecontent);
        }

        // GET: CBookingpagecontents/Create
        public IActionResult Create()
        {
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid");
            return View();
        }

        // POST: CBookingpagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Bookingpagecontentid,Projectname,Pagename,ImageFile,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CBookingpagecontent cBookingpagecontent)
        {
            if (ModelState.IsValid)
            {
                if (cBookingpagecontent.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cBookingpagecontent.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cBookingpagecontent.ImageFile.CopyToAsync(fileStream);
                    }

                    cBookingpagecontent.ImagepathTop = fileName;

                }
                _context.Add(cBookingpagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cBookingpagecontent.Userloginid);
            return View(cBookingpagecontent);
        }

        // GET: CBookingpagecontents/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CBookingpagecontents == null)
            {
                return NotFound();
            }

            var cBookingpagecontent = await _context.CBookingpagecontents.FindAsync(id);
            if (cBookingpagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cBookingpagecontent.Userloginid);
            return View(cBookingpagecontent);
        }

        // POST: CBookingpagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Bookingpagecontentid,Projectname,Pagename,ImageFile,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CBookingpagecontent cBookingpagecontent)
        {
            if (id != cBookingpagecontent.Bookingpagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (cBookingpagecontent.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cBookingpagecontent.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cBookingpagecontent.ImageFile.CopyToAsync(fileStream);
                    }

                    cBookingpagecontent.ImagepathTop = fileName;

                }
                try
                {
                    _context.Update(cBookingpagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CBookingpagecontentExists(cBookingpagecontent.Bookingpagecontentid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cBookingpagecontent.Userloginid);
            return View(cBookingpagecontent);
        }

        // GET: CBookingpagecontents/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CBookingpagecontents == null)
            {
                return NotFound();
            }

            var cBookingpagecontent = await _context.CBookingpagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Bookingpagecontentid == id);
            if (cBookingpagecontent == null)
            {
                return NotFound();
            }

            return View(cBookingpagecontent);
        }

        // POST: CBookingpagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CBookingpagecontents == null)
            {
                return Problem("Entity set 'ModelContext.CBookingpagecontents'  is null.");
            }
            var cBookingpagecontent = await _context.CBookingpagecontents.FindAsync(id);
            if (cBookingpagecontent != null)
            {
                _context.CBookingpagecontents.Remove(cBookingpagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CBookingpagecontentExists(decimal id)
        {
          return (_context.CBookingpagecontents?.Any(e => e.Bookingpagecontentid == id)).GetValueOrDefault();
        }
    }
}
