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
    public class CServicesController : Controller
    {
        private readonly ModelContext _context;

        public CServicesController(ModelContext context)
        {
            _context = context;
        }

        // GET: CServices
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.CServices.Include(c => c.Hotel);
            return View(await modelContext.ToListAsync());
        }

        // GET: CServices/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CServices == null)
            {
                return NotFound();
            }

            var cService = await _context.CServices
                .Include(c => c.Hotel)
                .FirstOrDefaultAsync(m => m.Serviceid == id);
            if (cService == null)
            {
                return NotFound();
            }

            return View(cService);
        }

        // GET: CServices/Create
        public IActionResult Create()
        {
            ViewData["Hotelid"] = new SelectList(_context.CHotels, "Hotelid", "Hotelid");
            return View();
        }

        // POST: CServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Serviceid,Servicename,Servicetext,Hotelid")] CService cService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Hotelid"] = new SelectList(_context.CHotels, "Hotelid", "Hotelid", cService.Hotelid);
            return View(cService);
        }

        // GET: CServices/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CServices == null)
            {
                return NotFound();
            }

            var cService = await _context.CServices.FindAsync(id);
            if (cService == null)
            {
                return NotFound();
            }
            ViewData["Hotelid"] = new SelectList(_context.CHotels, "Hotelid", "Hotelid", cService.Hotelid);
            return View(cService);
        }

        // POST: CServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Serviceid,Servicename,Servicetext,Hotelid")] CService cService)
        {
            if (id != cService.Serviceid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CServiceExists(cService.Serviceid))
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
            ViewData["Hotelid"] = new SelectList(_context.CHotels, "Hotelid", "Hotelid", cService.Hotelid);
            return View(cService);
        }

        // GET: CServices/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CServices == null)
            {
                return NotFound();
            }

            var cService = await _context.CServices
                .Include(c => c.Hotel)
                .FirstOrDefaultAsync(m => m.Serviceid == id);
            if (cService == null)
            {
                return NotFound();
            }

            return View(cService);
        }

        // POST: CServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CServices == null)
            {
                return Problem("Entity set 'ModelContext.CServices'  is null.");
            }
            var cService = await _context.CServices.FindAsync(id);
            if (cService != null)
            {
                _context.CServices.Remove(cService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CServiceExists(decimal id)
        {
          return (_context.CServices?.Any(e => e.Serviceid == id)).GetValueOrDefault();
        }
    }
}
