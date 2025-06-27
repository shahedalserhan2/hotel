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
    public class CRolesController : Controller
    {
        private readonly ModelContext _context;

        public CRolesController(ModelContext context)
        {
            _context = context;
        }

        // GET: CRoles
        public async Task<IActionResult> Index()
        {
              return _context.CRoles != null ? 
                          View(await _context.CRoles.ToListAsync()) :
                          Problem("Entity set 'ModelContext.CRoles'  is null.");
        }

        // GET: CRoles/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CRoles == null)
            {
                return NotFound();
            }

            var cRole = await _context.CRoles
                .FirstOrDefaultAsync(m => m.Roleid == id);
            if (cRole == null)
            {
                return NotFound();
            }

            return View(cRole);
        }

        // GET: CRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Roleid,Rolename")] CRole cRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cRole);
        }

        // GET: CRoles/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CRoles == null)
            {
                return NotFound();
            }

            var cRole = await _context.CRoles.FindAsync(id);
            if (cRole == null)
            {
                return NotFound();
            }
            return View(cRole);
        }

        // POST: CRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Roleid,Rolename")] CRole cRole)
        {
            if (id != cRole.Roleid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CRoleExists(cRole.Roleid))
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
            return View(cRole);
        }

        // GET: CRoles/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CRoles == null)
            {
                return NotFound();
            }

            var cRole = await _context.CRoles
                .FirstOrDefaultAsync(m => m.Roleid == id);
            if (cRole == null)
            {
                return NotFound();
            }

            return View(cRole);
        }

        // POST: CRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CRoles == null)
            {
                return Problem("Entity set 'ModelContext.CRoles'  is null.");
            }
            var cRole = await _context.CRoles.FindAsync(id);
            if (cRole != null)
            {
                _context.CRoles.Remove(cRole);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CRoleExists(decimal id)
        {
          return (_context.CRoles?.Any(e => e.Roleid == id)).GetValueOrDefault();
        }
    }
}
