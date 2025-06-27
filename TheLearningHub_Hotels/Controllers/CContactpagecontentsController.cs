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
    public class CContactpagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public CContactpagecontentsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: CContactpagecontents
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.CContactpagecontents.Include(c => c.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: CContactpagecontents/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CContactpagecontents == null)
            {
                return NotFound();
            }

            var cContactpagecontent = await _context.CContactpagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Contactpagecontentid == id);
            if (cContactpagecontent == null)
            {
                return NotFound();
            }

            return View(cContactpagecontent);
        }

        // GET: CContactpagecontents/Create
        public IActionResult Create()
        {
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid");
            return View();
        }

        // POST: CContactpagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Contactpagecontentid,Projectname,Pagename,ImageFile,Bookingemail,Generalemail,Technicalemail,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CContactpagecontent cContactpagecontent)
        {
            if (ModelState.IsValid)
            {
                if (cContactpagecontent.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cContactpagecontent.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cContactpagecontent.ImageFile.CopyToAsync(fileStream);
                    }

                    cContactpagecontent.ImagepathTop = fileName;

                }
                _context.Add(cContactpagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cContactpagecontent.Userloginid);
            return View(cContactpagecontent);
        }

        // GET: CContactpagecontents/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CContactpagecontents == null)
            {
                return NotFound();
            }

            var cContactpagecontent = await _context.CContactpagecontents.FindAsync(id);
            if (cContactpagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cContactpagecontent.Userloginid);
            return View(cContactpagecontent);
        }

        // POST: CContactpagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Contactpagecontentid,Projectname,Pagename,ImagepathTop,Bookingemail,Generalemail,Technicalemail,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CContactpagecontent cContactpagecontent)
        {
            if (id != cContactpagecontent.Contactpagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (cContactpagecontent.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cContactpagecontent.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cContactpagecontent.ImageFile.CopyToAsync(fileStream);
                    }

                    cContactpagecontent.ImagepathTop = fileName;

                }
                try
                {
                    _context.Update(cContactpagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CContactpagecontentExists(cContactpagecontent.Contactpagecontentid))
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
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cContactpagecontent.Userloginid);
            return View(cContactpagecontent);
        }

        // GET: CContactpagecontents/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CContactpagecontents == null)
            {
                return NotFound();
            }

            var cContactpagecontent = await _context.CContactpagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Contactpagecontentid == id);
            if (cContactpagecontent == null)
            {
                return NotFound();
            }

            return View(cContactpagecontent);
        }

        // POST: CContactpagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CContactpagecontents == null)
            {
                return Problem("Entity set 'ModelContext.CContactpagecontents'  is null.");
            }
            var cContactpagecontent = await _context.CContactpagecontents.FindAsync(id);
            if (cContactpagecontent != null)
            {
                _context.CContactpagecontents.Remove(cContactpagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CContactpagecontentExists(decimal id)
        {
          return (_context.CContactpagecontents?.Any(e => e.Contactpagecontentid == id)).GetValueOrDefault();
        }
    }
}
