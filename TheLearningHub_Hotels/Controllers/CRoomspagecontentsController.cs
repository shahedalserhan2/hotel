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
    public class CRoomspagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public CRoomspagecontentsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: CRoomspagecontents
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.CRoomspagecontents.Include(c => c.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: CRoomspagecontents/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CRoomspagecontents == null)
            {
                return NotFound();
            }

            var cRoomspagecontent = await _context.CRoomspagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Roomspagecontentid == id);
            if (cRoomspagecontent == null)
            {
                return NotFound();
            }

            return View(cRoomspagecontent);
        }

        // GET: CRoomspagecontents/Create
        public IActionResult Create()
        {
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid");
            return View();
        }

        // POST: CRoomspagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Roomspagecontentid,Projectname,Pagename,ImageFile,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CRoomspagecontent cRoomspagecontent)
        {
            if (ModelState.IsValid)
            {

                if (cRoomspagecontent.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cRoomspagecontent.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cRoomspagecontent.ImageFile.CopyToAsync(fileStream);
                    }

                    cRoomspagecontent.ImagepathTop = fileName;

                }
                _context.Add(cRoomspagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cRoomspagecontent.Userloginid);
            return View(cRoomspagecontent);
        }

        // GET: CRoomspagecontents/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CRoomspagecontents == null)
            {
                return NotFound();
            }

            var cRoomspagecontent = await _context.CRoomspagecontents.FindAsync(id);
            if (cRoomspagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cRoomspagecontent.Userloginid);
            return View(cRoomspagecontent);
        }

        // POST: CRoomspagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Roomspagecontentid,Projectname,Pagename,ImageFile,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CRoomspagecontent cRoomspagecontent)
        {
            if (id != cRoomspagecontent.Roomspagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cRoomspagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CRoomspagecontentExists(cRoomspagecontent.Roomspagecontentid))
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
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cRoomspagecontent.Userloginid);
            return View(cRoomspagecontent);
        }

        // GET: CRoomspagecontents/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CRoomspagecontents == null)
            {
                return NotFound();
            }

            var cRoomspagecontent = await _context.CRoomspagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Roomspagecontentid == id);
            if (cRoomspagecontent == null)
            {
                return NotFound();
            }

            return View(cRoomspagecontent);
        }

        // POST: CRoomspagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CRoomspagecontents == null)
            {
                return Problem("Entity set 'ModelContext.CRoomspagecontents'  is null.");
            }
            var cRoomspagecontent = await _context.CRoomspagecontents.FindAsync(id);
            if (cRoomspagecontent != null)
            {
                _context.CRoomspagecontents.Remove(cRoomspagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CRoomspagecontentExists(decimal id)
        {
          return (_context.CRoomspagecontents?.Any(e => e.Roomspagecontentid == id)).GetValueOrDefault();
        }
    }
}
