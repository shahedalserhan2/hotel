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
    public class CTestimonialsController : Controller
    {
        private readonly ModelContext _context;

        public CTestimonialsController(ModelContext context)
        {
            _context = context;
        }

        // GET: CTestimonials
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.CTestimonials.Include(c => c.Hotel).Include(c => c.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: CTestimonials/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CTestimonials == null)
            {
                return NotFound();
            }

            var cTestimonial = await _context.CTestimonials
                .Include(c => c.Hotel)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Testimonialid == id);
            if (cTestimonial == null)
            {
                return NotFound();
            }

            return View(cTestimonial);
        }

        // GET: CTestimonials/Create
        public IActionResult Create()
        {
            ViewData["Hotelid"] = new SelectList(_context.CHotels, "Hotelid", "Hotelid");
            ViewData["UserId"] = new SelectList(_context.CUsers, "UserId", "UserId");
            return View();
        }

        // POST: CTestimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Testimonialid,TestimonialText,Rating,CreatedAt,UserId,Hotelid,STATUS,ACTIONS")] CTestimonial cTestimonial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cTestimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Hotelid"] = new SelectList(_context.CHotels, "Hotelid", "Hotelid", cTestimonial.Hotelid);
            ViewData["UserId"] = new SelectList(_context.CUsers, "UserId", "UserId", cTestimonial.UserId);
            return View(cTestimonial);
        }

        // GET: CTestimonials/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CTestimonials == null)
            {
                return NotFound();
            }

            var cTestimonial = await _context.CTestimonials.FindAsync(id);
            if (cTestimonial == null)
            {
                return NotFound();
            }
            ViewData["Hotelid"] = new SelectList(_context.CHotels, "Hotelid", "Hotelid", cTestimonial.Hotelid);
            ViewData["UserId"] = new SelectList(_context.CUsers, "UserId", "UserId", cTestimonial.UserId);
            return View(cTestimonial);
        }

        // POST: CTestimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Testimonialid,TestimonialText,Rating,CreatedAt,UserId,Hotelid")] CTestimonial cTestimonial)
        {
            if (id != cTestimonial.Testimonialid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cTestimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CTestimonialExists(cTestimonial.Testimonialid))
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
            ViewData["Hotelid"] = new SelectList(_context.CHotels, "Hotelid", "Hotelid", cTestimonial.Hotelid);
            ViewData["UserId"] = new SelectList(_context.CUsers, "UserId", "UserId", cTestimonial.UserId);
            return View(cTestimonial);
        }

        // GET: CTestimonials/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CTestimonials == null)
            {
                return NotFound();
            }

            var cTestimonial = await _context.CTestimonials
                .Include(c => c.Hotel)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Testimonialid == id);
            if (cTestimonial == null)
            {
                return NotFound();
            }

            return View(cTestimonial);
        }

        // POST: CTestimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CTestimonials == null)
            {
                return Problem("Entity set 'ModelContext.CTestimonials'  is null.");
            }
            var cTestimonial = await _context.CTestimonials.FindAsync(id);
            if (cTestimonial != null)
            {
                _context.CTestimonials.Remove(cTestimonial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CTestimonialExists(decimal id)
        {
          return (_context.CTestimonials?.Any(e => e.Testimonialid == id)).GetValueOrDefault();
        }
    }
}
