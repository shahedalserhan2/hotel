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
    public class CHomepagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public CHomepagecontentsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: CHomepagecontents
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.CHomepagecontents.Include(c => c.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: CHomepagecontents/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CHomepagecontents == null)
            {
                return NotFound();
            }

            var cHomepagecontent = await _context.CHomepagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Homepagecontent == id);
            if (cHomepagecontent == null)
            {
                return NotFound();
            }

            return View(cHomepagecontent);
        }

        // GET: CHomepagecontents/Create
        public IActionResult Create()
        {
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid");
            return View();
        }

        // POST: CHomepagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Homepagecontent,Projectname,Pagename,ImageFile1,ImageFile2,WelcomeText,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CHomepagecontent cHomepagecontent)
        {
            if (ModelState.IsValid)
            {
                if (cHomepagecontent.ImageFile1 != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cHomepagecontent.ImageFile1.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cHomepagecontent.ImageFile1.CopyToAsync(fileStream);
                    }

                    cHomepagecontent.ImagepathTop1 = fileName;


                }
                else if (cHomepagecontent.ImageFile2 != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cHomepagecontent.ImageFile2.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cHomepagecontent.ImageFile2.CopyToAsync(fileStream);
                    }

                    cHomepagecontent.ImagepathTop2 = fileName;


                }
                _context.Add(cHomepagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cHomepagecontent.Userloginid);
            return View(cHomepagecontent);
        }

        // GET: CHomepagecontents/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CHomepagecontents == null)
            {
                return NotFound();
            }

            var cHomepagecontent = await _context.CHomepagecontents.FindAsync(id);
            if (cHomepagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cHomepagecontent.Userloginid);
            return View(cHomepagecontent);
        }

        // POST: CHomepagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Homepagecontent,Projectname,Pagename,ImageFile1,ImageFile2,WelcomeText,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] CHomepagecontent cHomepagecontent)
        {
            if (id != cHomepagecontent.Homepagecontent)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (cHomepagecontent.ImageFile1 != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cHomepagecontent.ImageFile1.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cHomepagecontent.ImageFile1.CopyToAsync(fileStream);
                    }

                    cHomepagecontent.ImagepathTop1 = fileName;
                    

                }
                 if (cHomepagecontent.ImageFile2 != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cHomepagecontent.ImageFile2.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cHomepagecontent.ImageFile2.CopyToAsync(fileStream);
                    }

                    cHomepagecontent.ImagepathTop2 = fileName;


                }
                try
                {
                    _context.Update(cHomepagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CHomepagecontentExists(cHomepagecontent.Homepagecontent))
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
            ViewData["Userloginid"] = new SelectList(_context.CUserlogins, "Userloginid", "Userloginid", cHomepagecontent.Userloginid);
            return View(cHomepagecontent);
        }

        // GET: CHomepagecontents/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CHomepagecontents == null)
            {
                return NotFound();
            }

            var cHomepagecontent = await _context.CHomepagecontents
                .Include(c => c.Userlogin)
                .FirstOrDefaultAsync(m => m.Homepagecontent == id);
            if (cHomepagecontent == null)
            {
                return NotFound();
            }

            return View(cHomepagecontent);
        }

        // POST: CHomepagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CHomepagecontents == null)
            {
                return Problem("Entity set 'ModelContext.CHomepagecontents'  is null.");
            }
            var cHomepagecontent = await _context.CHomepagecontents.FindAsync(id);
            if (cHomepagecontent != null)
            {
                _context.CHomepagecontents.Remove(cHomepagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CHomepagecontentExists(decimal id)
        {
          return (_context.CHomepagecontents?.Any(e => e.Homepagecontent == id)).GetValueOrDefault();
        }
    }
}
