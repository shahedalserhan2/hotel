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
    public class CUserloginsController : Controller
    {
        private readonly ModelContext _context;

        public CUserloginsController(ModelContext context)
        {
            _context = context;
        }

        // GET: CUserlogins
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.CUserlogins.Include(c => c.Role).Include(c => c.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: CUserlogins/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CUserlogins == null)
            {
                return NotFound();
            }

            var cUserlogin = await _context.CUserlogins
                .Include(c => c.Role)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Userloginid == id);
            if (cUserlogin == null)
            {
                return NotFound();
            }

            return View(cUserlogin);
        }

        // GET: CUserlogins/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.CRoles, "Roleid", "Roleid");
            ViewData["UserId"] = new SelectList(_context.CUsers, "UserId", "UserId");
            return View();
        }

        // POST: CUserlogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userloginid,Username,Passwordd,Roleid,UserId")] CUserlogin cUserlogin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cUserlogin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.CRoles, "Roleid", "Roleid", cUserlogin.Roleid);
            ViewData["UserId"] = new SelectList(_context.CUsers, "UserId", "UserId", cUserlogin.UserId);
            return View(cUserlogin);
        }

        // GET: CUserlogins/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CUserlogins == null)
            {
                return NotFound();
            }

            var cUserlogin = await _context.CUserlogins.FindAsync(id);
            if (cUserlogin == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.CRoles, "Roleid", "Roleid", cUserlogin.Roleid);
            ViewData["UserId"] = new SelectList(_context.CUsers, "UserId", "UserId", cUserlogin.UserId);
            return View(cUserlogin);
        }

        // POST: CUserlogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userloginid,Username,Passwordd,Roleid,UserId")] CUserlogin cUserlogin)
        {
            if (id != cUserlogin.Userloginid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cUserlogin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CUserloginExists(cUserlogin.Userloginid))
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
            ViewData["Roleid"] = new SelectList(_context.CRoles, "Roleid", "Roleid", cUserlogin.Roleid);
            ViewData["UserId"] = new SelectList(_context.CUsers, "UserId", "UserId", cUserlogin.UserId);
            return View(cUserlogin);
        }

        // GET: CUserlogins/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CUserlogins == null)
            {
                return NotFound();
            }

            var cUserlogin = await _context.CUserlogins
                .Include(c => c.Role)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Userloginid == id);
            if (cUserlogin == null)
            {
                return NotFound();
            }

            return View(cUserlogin);
        }

        // POST: CUserlogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CUserlogins == null)
            {
                return Problem("Entity set 'ModelContext.CUserlogins'  is null.");
            }
            var cUserlogin = await _context.CUserlogins.FindAsync(id);
            if (cUserlogin != null)
            {
                _context.CUserlogins.Remove(cUserlogin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CUserloginExists(decimal id)
        {
          return (_context.CUserlogins?.Any(e => e.Userloginid == id)).GetValueOrDefault();
        }
    }
}
