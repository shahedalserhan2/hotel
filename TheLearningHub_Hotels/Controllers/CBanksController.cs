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
    public class CBanksController : Controller
    {
        private readonly ModelContext _context;


        public CBanksController(ModelContext context)
        {
            _context = context;
        }

        // GET: CBanks
        public async Task<IActionResult> Index()
        {
              return _context.CBanks != null ? 
                          View(await _context.CBanks.ToListAsync()) :
                          Problem("Entity set 'ModelContext.CBanks'  is null.");
        }

        // GET: CBanks/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CBanks == null)
            {
                return NotFound();
            }

            var cBank = await _context.CBanks
                .FirstOrDefaultAsync(m => m.Bankid == id);
            if (cBank == null)
            {
                return NotFound();
            }

            return View(cBank);
        }

        // GET: CBanks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CBanks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Bankid,Creditcardnumber,Creditcardexp,MONEY")] CBank cBank)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cBank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cBank);
        }

        // GET: CBanks/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CBanks == null)
            {
                return NotFound();
            }

            var cBank = await _context.CBanks.FindAsync(id);
            if (cBank == null)
            {
                return NotFound();
            }
            return View(cBank);
        }

        // POST: CBanks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Bankid,Creditcardnumber,Creditcardexp,MONEY")] CBank cBank)
        {
            if (id != cBank.Bankid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cBank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CBankExists(cBank.Bankid))
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
            return View(cBank);
        }

        // GET: CBanks/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CBanks == null)
            {
                return NotFound();
            }

            var cBank = await _context.CBanks
                .FirstOrDefaultAsync(m => m.Bankid == id);
            if (cBank == null)
            {
                return NotFound();
            }

            return View(cBank);
        }

        // POST: CBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CBanks == null)
            {
                return Problem("Entity set 'ModelContext.CBanks'  is null.");
            }
            var cBank = await _context.CBanks.FindAsync(id);
            if (cBank != null)
            {
                _context.CBanks.Remove(cBank);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CBankExists(decimal id)
        {
          return (_context.CBanks?.Any(e => e.Bankid == id)).GetValueOrDefault();
        }
    }
}
