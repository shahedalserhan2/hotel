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
    public class CTeampagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public CTeampagecontentsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: CTeampagecontents
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.CTeampagecontents.Include(c => c.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: CTeampagecontents/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CTeampagecontents == null)
            {
                return NotFound();
            }

            var cTeampagecontent = await _context.CTeampagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Teampagecontentid == id);
            if (cTeampagecontent == null)
            {
                return NotFound();
            }

            return View(cTeampagecontent);
        }

        // GET: CTeampagecontents/Create
        public IActionResult Create()
        {
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid");
            return View();
        }

        // POST: CTeampagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Teampagecontentid,Projectname,Pagename,ImageFile,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CTeampagecontent cTeampagecontent)
        {
            if (ModelState.IsValid)
            {
                if (cTeampagecontent.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cTeampagecontent.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cTeampagecontent.ImageFile.CopyToAsync(fileStream);
                    }

                    cTeampagecontent.ImagepathTop = fileName;

                }


                _context.Add(cTeampagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cTeampagecontent.Userloginid);
            return View(cTeampagecontent);
        }

        // GET: CTeampagecontents/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CTeampagecontents == null)
            {
                return NotFound();
            }

            var cTeampagecontent = await _context.CTeampagecontents.FindAsync(id);
            if (cTeampagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cTeampagecontent.Userloginid);
            return View(cTeampagecontent);
        }

        // POST: CTeampagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Teampagecontentid,Projectname,Pagename,ImageFile,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CTeampagecontent cTeampagecontent)
        {
            if (id != cTeampagecontent.Teampagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (cTeampagecontent.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cTeampagecontent.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cTeampagecontent.ImageFile.CopyToAsync(fileStream);
                    }

                    cTeampagecontent.ImagepathTop = fileName;

                }
                try
                {
                    _context.Update(cTeampagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CTeampagecontentExists(cTeampagecontent.Teampagecontentid))
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
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cTeampagecontent.Userloginid);
            return View(cTeampagecontent);
        }

        // GET: CTeampagecontents/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CTeampagecontents == null)
            {
                return NotFound();
            }

            var cTeampagecontent = await _context.CTeampagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Teampagecontentid == id);
            if (cTeampagecontent == null)
            {
                return NotFound();
            }

            return View(cTeampagecontent);
        }

        // POST: CTeampagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CTeampagecontents == null)
            {
                return Problem("Entity set 'ModelContext.CTeampagecontents'  is null.");
            }
            var cTeampagecontent = await _context.CTeampagecontents.FindAsync(id);
            if (cTeampagecontent != null)
            {
                _context.CTeampagecontents.Remove(cTeampagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CTeampagecontentExists(decimal id)
        {
          return (_context.CTeampagecontents?.Any(e => e.Teampagecontentid == id)).GetValueOrDefault();
        }
    }
}
