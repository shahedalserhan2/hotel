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
    public class CHotelsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public CHotelsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: CHotels
        public async Task<IActionResult> Index()
        {
              return _context.CHotels != null ? 
                          View(await _context.CHotels.ToListAsync()) :
                          Problem("Entity set 'ModelContext.CHotels'  is null.");
        }

        // GET: CHotels/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CHotels == null)
            {
                return NotFound();
            }

            var cHotel = await _context.CHotels
                .FirstOrDefaultAsync(m => m.Hotelid == id);
            if (cHotel == null)
            {
                return NotFound();
            }

            return View(cHotel);
        }

        // GET: CHotels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CHotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Hotelid,Hotelname,Location,Description,ImageFile")] CHotel cHotel)
        {
            if (ModelState.IsValid)
            {

                if (cHotel.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cHotel.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cHotel.ImageFile.CopyToAsync(fileStream);
                    }

                    cHotel.Imagepath = fileName;

                }
                _context.Add(cHotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cHotel);
        }

        // GET: CHotels/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CHotels == null)
            {
                return NotFound();
            }

            var cHotel = await _context.CHotels.FindAsync(id);
            if (cHotel == null)
            {
                return NotFound();
            }
            return View(cHotel);
        }

        // POST: CHotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Hotelid,Hotelname,Location,Description,ImageFile")] CHotel cHotel)
        {
            if (id != cHotel.Hotelid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (cHotel.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cHotel.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cHotel.ImageFile.CopyToAsync(fileStream);
                    }

                    cHotel.Imagepath = fileName;

                }
                try
                {
                    _context.Update(cHotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CHotelExists(cHotel.Hotelid))
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
            return View(cHotel);
        }

        // GET: CHotels/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CHotels == null)
            {
                return NotFound();
            }

            var cHotel = await _context.CHotels
                .FirstOrDefaultAsync(m => m.Hotelid == id);
            if (cHotel == null)
            {
                return NotFound();
            }

            return View(cHotel);
        }

        // POST: CHotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CHotels == null)
            {
                return Problem("Entity set 'ModelContext.CHotels'  is null.");
            }
            var cHotel = await _context.CHotels.FindAsync(id);
            if (cHotel != null)
            {
                _context.CHotels.Remove(cHotel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CHotelExists(decimal id)
        {
          return (_context.CHotels?.Any(e => e.Hotelid == id)).GetValueOrDefault();
        }
    }
}
