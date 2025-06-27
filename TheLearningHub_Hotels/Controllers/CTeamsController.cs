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
    public class CTeamsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public CTeamsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: CTeams
        public async Task<IActionResult> Index()
        {
              return _context.CTeams != null ? 
                          View(await _context.CTeams.ToListAsync()) :
                          Problem("Entity set 'ModelContext.CTeams'  is null.");
        }

        // GET: CTeams/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CTeams == null)
            {
                return NotFound();
            }

            var cTeam = await _context.CTeams
                .FirstOrDefaultAsync(m => m.Teamid == id);
            if (cTeam == null)
            {
                return NotFound();
            }

            return View(cTeam);
        }

        // GET: CTeams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Teamid,TeamMembername,Position,ImageFile")] CTeam cTeam)
        {
            if (ModelState.IsValid)
            {
                if (cTeam.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cTeam.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cTeam.ImageFile.CopyToAsync(fileStream);
                    }

                    cTeam.Imagepath = fileName;

                }
                _context.Add(cTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cTeam);
        }

        // GET: CTeams/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CTeams == null)
            {
                return NotFound();
            }

            var cTeam = await _context.CTeams.FindAsync(id);
            if (cTeam == null)
            {
                return NotFound();
            }
            return View(cTeam);
        }

        // POST: CTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Teamid,TeamMembername,Position,Imagepath")] CTeam cTeam)
        {
            if (id != cTeam.Teamid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CTeamExists(cTeam.Teamid))
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
            return View(cTeam);
        }

        // GET: CTeams/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CTeams == null)
            {
                return NotFound();
            }

            var cTeam = await _context.CTeams
                .FirstOrDefaultAsync(m => m.Teamid == id);
            if (cTeam == null)
            {
                return NotFound();
            }

            return View(cTeam);
        }

        // POST: CTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CTeams == null)
            {
                return Problem("Entity set 'ModelContext.CTeams'  is null.");
            }
            var cTeam = await _context.CTeams.FindAsync(id);
            if (cTeam != null)
            {
                _context.CTeams.Remove(cTeam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CTeamExists(decimal id)
        {
          return (_context.CTeams?.Any(e => e.Teamid == id)).GetValueOrDefault();
        }
    }
}
