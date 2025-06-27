using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheLearningHub_Hotels.Models;

namespace TheLearningHub_Hotels.Controllers
{
    public class CTestimonialpagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public CTestimonialpagecontentsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: CTestimonialpagecontents
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.CTestimonialpagecontents.Include(c => c.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: CTestimonialpagecontents/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CTestimonialpagecontents == null)
            {
                return NotFound();
            }

            var cTestimonialpagecontent = await _context.CTestimonialpagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Testimonialpagecontentid == id);
            if (cTestimonialpagecontent == null)
            {
                return NotFound();
            }

            return View(cTestimonialpagecontent);
        }

        // GET: CTestimonialpagecontents/Create
        public IActionResult Create()
        {
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid");
            return View();
        }

        // POST: CTestimonialpagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Testimonialpagecontentid,Projectname,Pagename,ImageFile1,ImageFile2,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CTestimonialpagecontent cTestimonialpagecontent)
        {
            if (ModelState.IsValid)
            {

                if (cTestimonialpagecontent.ImageFile1 != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cTestimonialpagecontent.ImageFile1.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cTestimonialpagecontent.ImageFile1.CopyToAsync(fileStream);
                    }

                    cTestimonialpagecontent.ImagepathTop = fileName;

                }
                if(cTestimonialpagecontent.ImageFile2 != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cTestimonialpagecontent.ImageFile2.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cTestimonialpagecontent.ImageFile2.CopyToAsync(fileStream);
                    }

                    cTestimonialpagecontent.ImagepathMiddle = fileName;

                }
                _context.Add(cTestimonialpagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cTestimonialpagecontent.Userloginid);
            return View(cTestimonialpagecontent);
        }

        // GET: CTestimonialpagecontents/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CTestimonialpagecontents == null)
            {
                return NotFound();
            }

            var cTestimonialpagecontent = await _context.CTestimonialpagecontents.FindAsync(id);
            if (cTestimonialpagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cTestimonialpagecontent.Userloginid);
            return View(cTestimonialpagecontent);
        }

        // POST: CTestimonialpagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Testimonialpagecontentid,Projectname,Pagename,ImagepathTop,ImagepathMiddle,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CTestimonialpagecontent cTestimonialpagecontent)
        {
            if (id != cTestimonialpagecontent.Testimonialpagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cTestimonialpagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CTestimonialpagecontentExists(cTestimonialpagecontent.Testimonialpagecontentid))
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
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cTestimonialpagecontent.Userloginid);
            return View(cTestimonialpagecontent);
        }

        // GET: CTestimonialpagecontents/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CTestimonialpagecontents == null)
            {
                return NotFound();
            }

            var cTestimonialpagecontent = await _context.CTestimonialpagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Testimonialpagecontentid == id);
            if (cTestimonialpagecontent == null)
            {
                return NotFound();
            }

            return View(cTestimonialpagecontent);
        }

        // POST: CTestimonialpagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CTestimonialpagecontents == null)
            {
                return Problem("Entity set 'ModelContext.CTestimonialpagecontents'  is null.");
            }
            var cTestimonialpagecontent = await _context.CTestimonialpagecontents.FindAsync(id);
            if (cTestimonialpagecontent != null)
            {
                _context.CTestimonialpagecontents.Remove(cTestimonialpagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CTestimonialpagecontentExists(decimal id)
        {
          return (_context.CTestimonialpagecontents?.Any(e => e.Testimonialpagecontentid == id)).GetValueOrDefault();
        }
    }
}
