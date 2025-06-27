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
    public class CReservationsController : Controller
    {
        private readonly ModelContext _context;

        public CReservationsController(ModelContext context)
        {
            _context = context;
        }

        // GET: CReservations
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.CReservations.Include(c => c.Room).Include(c => c.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: CReservations/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CReservations == null)
            {
                return NotFound();
            }

            var cReservation = await _context.CReservations
                .Include(c => c.Room)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Reservationsid == id);
            if (cReservation == null)
            {
                return NotFound();
            }

            return View(cReservation);
        }

        // GET: CReservations/Create
        public IActionResult Create()
        {
            ViewData["Roomid"] = new SelectList(_context.CRooms, "Roomid", "Roomid");
            ViewData["UserId"] = new SelectList(_context.CUsers, "UserId", "UserId");
            return View();
        }

        // POST: CReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Reservationsid,CheckInDate,CheckOutDate,Toltalprice,Invoicepdf,UserId,Roomid")] CReservation cReservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roomid"] = new SelectList(_context.CRooms, "Roomid", "Roomid", cReservation.Roomid);
            ViewData["UserId"] = new SelectList(_context.CUsers, "UserId", "UserId", cReservation.UserId);
            return View(cReservation);
        }

        // GET: CReservations/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CReservations == null)
            {
                return NotFound();
            }

            var cReservation = await _context.CReservations.FindAsync(id);
            if (cReservation == null)
            {
                return NotFound();
            }
            ViewData["Roomid"] = new SelectList(_context.CRooms, "Roomid", "Roomid", cReservation.Roomid);
            ViewData["UserId"] = new SelectList(_context.CUsers, "UserId", "UserId", cReservation.UserId);
            return View(cReservation);
        }

        // POST: CReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Reservationsid,CheckInDate,CheckOutDate,Toltalprice,Invoicepdf,UserId,Roomid")] CReservation cReservation)
        {
            if (id != cReservation.Reservationsid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CReservationExists(cReservation.Reservationsid))
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
            ViewData["Roomid"] = new SelectList(_context.CRooms, "Roomid", "Roomid", cReservation.Roomid);
            ViewData["UserId"] = new SelectList(_context.CUsers, "UserId", "UserId", cReservation.UserId);
            return View(cReservation);
        }

        // GET: CReservations/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CReservations == null)
            {
                return NotFound();
            }

            var cReservation = await _context.CReservations
                .Include(c => c.Room)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Reservationsid == id);
            if (cReservation == null)
            {
                return NotFound();
            }

            return View(cReservation);
        }

        // POST: CReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CReservations == null)
            {
                return Problem("Entity set 'ModelContext.CReservations'  is null.");
            }
            var cReservation = await _context.CReservations.FindAsync(id);
            if (cReservation != null)
            {
                _context.CReservations.Remove(cReservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CReservationExists(decimal id)
        {
          return (_context.CReservations?.Any(e => e.Reservationsid == id)).GetValueOrDefault();
        }
    }
}
