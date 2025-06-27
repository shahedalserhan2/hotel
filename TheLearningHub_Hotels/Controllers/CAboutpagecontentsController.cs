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
    public class CAboutpagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public CAboutpagecontentsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: CAboutpagecontents
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.CAboutpagecontents.Include(c => c.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: CAboutpagecontents/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CAboutpagecontents == null)
            {
                return NotFound();
            }

            var cAboutpagecontent = await _context.CAboutpagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Aboutpagecontentid == id);
            if (cAboutpagecontent == null)
            {
                return NotFound();
            }

            return View(cAboutpagecontent);
        }

        // GET: CAboutpagecontents/Create
        public IActionResult Create()
        {
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid");
            return View();
        }

        // POST: CAboutpagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Aboutpagecontentid,Projectname,Pagename,ImageFile,WelcomeText,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CAboutpagecontent cAboutpagecontent)
        {
            if (ModelState.IsValid)
            {
                if (cAboutpagecontent.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cAboutpagecontent.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cAboutpagecontent.ImageFile.CopyToAsync(fileStream);
                    }

                    cAboutpagecontent.ImagepathTop = fileName;

                }




                _context.Add(cAboutpagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cAboutpagecontent.Userloginid);
            return View(cAboutpagecontent);
        }

        // GET: CAboutpagecontents/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CAboutpagecontents == null)
            {
                return NotFound();
            }

            var cAboutpagecontent = await _context.CAboutpagecontents.FindAsync(id);
            if (cAboutpagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cAboutpagecontent.Userloginid);
            return View(cAboutpagecontent);
        }

        // POST: CAboutpagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Aboutpagecontentid,Projectname,Pagename,ImageFile,WelcomeText,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CAboutpagecontent cAboutpagecontent)
        {
            if (id != cAboutpagecontent.Aboutpagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (cAboutpagecontent.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cAboutpagecontent.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cAboutpagecontent.ImageFile.CopyToAsync(fileStream);
                    }

                    cAboutpagecontent.ImagepathTop = fileName;

                }
                try
                {
                    _context.Update(cAboutpagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CAboutpagecontentExists(cAboutpagecontent.Aboutpagecontentid))
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
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cAboutpagecontent.Userloginid);
            return View(cAboutpagecontent);
        }

        // GET: CAboutpagecontents/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CAboutpagecontents == null)
            {
                return NotFound();
            }

            var cAboutpagecontent = await _context.CAboutpagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Aboutpagecontentid == id);
            if (cAboutpagecontent == null)
            {
                return NotFound();
            }

            return View(cAboutpagecontent);
        }

        // POST: CAboutpagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CAboutpagecontents == null)
            {
                return Problem("Entity set 'ModelContext.CAboutpagecontents'  is null.");
            }
            var cAboutpagecontent = await _context.CAboutpagecontents.FindAsync(id);
            if (cAboutpagecontent != null)
            {
                _context.CAboutpagecontents.Remove(cAboutpagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CAboutpagecontentExists(decimal id)
        {
          return (_context.CAboutpagecontents?.Any(e => e.Aboutpagecontentid == id)).GetValueOrDefault();
        }
    }
}
