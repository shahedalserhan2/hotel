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
    public class CRoomsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public CRoomsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: CRooms
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.CRooms.Include(c => c.Hotel);
            return View(await modelContext.ToListAsync());
        }

        // GET: CRooms/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CRooms == null)
            {
                return NotFound();
            }

            var cRoom = await _context.CRooms
                .Include(c => c.Hotel)
                .FirstOrDefaultAsync(m => m.Roomid == id);
            if (cRoom == null)
            {
                return NotFound();
            }

            return View(cRoom);
        }

        // GET: CRooms/Create
        public IActionResult Create()
        {
            ViewData["Hotelid"] = new SelectList(_context.CHotels, "Hotelid", "Hotelid");
            return View();
        }

        // POST: CRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Roomid,Roomnumber,Roomcapacity,PricePerNight,Isavailable,Hotelid,ImageFile")] CRoom cRoom)
        {
            if (ModelState.IsValid)

            {
                if (cRoom.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cRoom.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cRoom.ImageFile.CopyToAsync(fileStream);
                    }

                    cRoom.Imagepath = fileName;

                }
                _context.Add(cRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Hotelid"] = new SelectList(_context.CHotels, "Hotelid", "Hotelid", cRoom.Hotelid);
            return View(cRoom);
        }

        // GET: CRooms/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CRooms == null)
            {
                return NotFound();
            }

            var cRoom = await _context.CRooms.FindAsync(id);
            if (cRoom == null)
            {
                return NotFound();
            }
            ViewData["Hotelid"] = new SelectList(_context.CHotels, "Hotelid", "Hotelid", cRoom.Hotelid);
            return View(cRoom);
        }

        // POST: CRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Roomid,Roomnumber,Roomcapacity,PricePerNight,Isavailable,Hotelid,ImageFile")] CRoom cRoom)
        {
            if (id != cRoom.Roomid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (cRoom.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cRoom.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cRoom.ImageFile.CopyToAsync(fileStream);
                    }

                    cRoom.Imagepath = fileName;

                }

                try
                {
                    _context.Update(cRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CRoomExists(cRoom.Roomid))
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
            ViewData["Hotelid"] = new SelectList(_context.CHotels, "Hotelid", "Hotelid", cRoom.Hotelid);
            return View(cRoom);
        }

        // GET: CRooms/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CRooms == null)
            {
                return NotFound();
            }

            var cRoom = await _context.CRooms
                .Include(c => c.Hotel)
                .FirstOrDefaultAsync(m => m.Roomid == id);
            if (cRoom == null)
            {
                return NotFound();
            }

            return View(cRoom);
        }

        // POST: CRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CRooms == null)
            {
                return Problem("Entity set 'ModelContext.CRooms'  is null.");
            }
            var cRoom = await _context.CRooms.FindAsync(id);
            if (cRoom != null)
            {
                _context.CRooms.Remove(cRoom);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CRoomExists(decimal id)
        {
          return (_context.CRooms?.Any(e => e.Roomid == id)).GetValueOrDefault();
        }
    }
}
