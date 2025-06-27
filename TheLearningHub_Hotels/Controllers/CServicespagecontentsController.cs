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
    public class CServicespagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public CServicespagecontentsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: CServicespagecontents
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.CServicespagecontents.Include(c => c.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: CServicespagecontents/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CServicespagecontents == null)
            {
                return NotFound();
            }

            var cServicespagecontent = await _context.CServicespagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Servicespagecontentid == id);
            if (cServicespagecontent == null)
            {
                return NotFound();
            }

            return View(cServicespagecontent);
        }

        // GET: CServicespagecontents/Create
        public IActionResult Create()
        {
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid");
            return View();
        }

        // POST: CServicespagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Servicespagecontentid,Projectname,Pagename,ImageFile,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CServicespagecontent cServicespagecontent)
        {
            if (ModelState.IsValid)
            {

                if (cServicespagecontent.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cServicespagecontent.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cServicespagecontent.ImageFile.CopyToAsync(fileStream);
                    }

                    cServicespagecontent.ImagepathTop = fileName;

                }

                _context.Add(cServicespagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cServicespagecontent.Userloginid);
            return View(cServicespagecontent);
        }

        // GET: CServicespagecontents/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CServicespagecontents == null)
            {
                return NotFound();
            }

            var cServicespagecontent = await _context.CServicespagecontents.FindAsync(id);
            if (cServicespagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cServicespagecontent.Userloginid);
            return View(cServicespagecontent);
        }

        // POST: CServicespagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Servicespagecontentid,Projectname,Pagename,ImageFile,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CServicespagecontent cServicespagecontent)
        {
            if (id != cServicespagecontent.Servicespagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cServicespagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CServicespagecontentExists(cServicespagecontent.Servicespagecontentid))
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
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cServicespagecontent.Userloginid);
            return View(cServicespagecontent);
        }

        // GET: CServicespagecontents/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CServicespagecontents == null)
            {
                return NotFound();
            }

            var cServicespagecontent = await _context.CServicespagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Servicespagecontentid == id);
            if (cServicespagecontent == null)
            {
                return NotFound();
            }

            return View(cServicespagecontent);
        }

        // POST: CServicespagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CServicespagecontents == null)
            {
                return Problem("Entity set 'ModelContext.CServicespagecontents'  is null.");
            }
            var cServicespagecontent = await _context.CServicespagecontents.FindAsync(id);
            if (cServicespagecontent != null)
            {
                _context.CServicespagecontents.Remove(cServicespagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CServicespagecontentExists(decimal id)
        {
          return (_context.CServicespagecontents?.Any(e => e.Servicespagecontentid == id)).GetValueOrDefault();
        }
    }
}
