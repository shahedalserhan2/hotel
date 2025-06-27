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
    public class CHotelspagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public CHotelspagecontentsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: CHotelspagecontents
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.CHotelspagecontents.Include(c => c.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: CHotelspagecontents/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CHotelspagecontents == null)
            {
                return NotFound();
            }

            var cHotelspagecontent = await _context.CHotelspagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Hotelspagecontentid == id);
            if (cHotelspagecontent == null)
            {
                return NotFound();
            }

            return View(cHotelspagecontent);
        }

        // GET: CHotelspagecontents/Create
        public IActionResult Create()
        {
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid");
            return View();
        }

        // POST: CHotelspagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Hotelspagecontentid,Projectname,Pagename,ImageFile,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CHotelspagecontent cHotelspagecontent)
        {
            if (ModelState.IsValid)
            {
                if (cHotelspagecontent.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cHotelspagecontent.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cHotelspagecontent.ImageFile.CopyToAsync(fileStream);
                    }

                    cHotelspagecontent.ImagepathTop = fileName;

                }
                _context.Add(cHotelspagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cHotelspagecontent.Userloginid);
            return View(cHotelspagecontent);
        }

        // GET: CHotelspagecontents/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CHotelspagecontents == null)
            {
                return NotFound();
            }

            var cHotelspagecontent = await _context.CHotelspagecontents.FindAsync(id);
            if (cHotelspagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cHotelspagecontent.Userloginid);
            return View(cHotelspagecontent);
        }

        // POST: CHotelspagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Hotelspagecontentid,Projectname,Pagename,ImageFile,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CHotelspagecontent cHotelspagecontent)
        {
            if (id != cHotelspagecontent.Hotelspagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (cHotelspagecontent.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cHotelspagecontent.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cHotelspagecontent.ImageFile.CopyToAsync(fileStream);
                    }

                    cHotelspagecontent.ImagepathTop = fileName;

                }
                try
                {
                    _context.Update(cHotelspagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CHotelspagecontentExists(cHotelspagecontent.Hotelspagecontentid))
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
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cHotelspagecontent.Userloginid);
            return View(cHotelspagecontent);
        }

        // GET: CHotelspagecontents/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CHotelspagecontents == null)
            {
                return NotFound();
            }

            var cHotelspagecontent = await _context.CHotelspagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Hotelspagecontentid == id);
            if (cHotelspagecontent == null)
            {
                return NotFound();
            }

            return View(cHotelspagecontent);
        }

        // POST: CHotelspagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CHotelspagecontents == null)
            {
                return Problem("Entity set 'ModelContext.CHotelspagecontents'  is null.");
            }
            var cHotelspagecontent = await _context.CHotelspagecontents.FindAsync(id);
            if (cHotelspagecontent != null)
            {
                _context.CHotelspagecontents.Remove(cHotelspagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CHotelspagecontentExists(decimal id)
        {
          return (_context.CHotelspagecontents?.Any(e => e.Hotelspagecontentid == id)).GetValueOrDefault();
        }
    }
}
