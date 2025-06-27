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
    public class CUsersController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public CUsersController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: CUsers
        public async Task<IActionResult> Index()
        {
              return _context.CUsers != null ? 
                          View(await _context.CUsers.ToListAsync()) :
                          Problem("Entity set 'ModelContext.CUsers'  is null.");
        }

        // GET: CUsers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.CUsers == null)
            {
                return NotFound();
            }

            var cUser = await _context.CUsers
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (cUser == null)
            {
                return NotFound();
            }

            return View(cUser);
        }

        // GET: CUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Fname,Lname,PhoneNumber,Email,ImageFile")] CUser cUser)
        {
            if (ModelState.IsValid)
            {
                if (cUser.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cUser.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cUser.ImageFile.CopyToAsync(fileStream);
                    }

                    cUser.Imagepath = fileName;

                }
                _context.Add(cUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cUser);
        }

        // GET: CUsers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.CUsers == null)
            {
                return NotFound();
            }

            var cUser = await _context.CUsers.FindAsync(id);
            if (cUser == null)
            {
                return NotFound();
            }
            return View(cUser);
        }

        // POST: CUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("UserId,Fname,Lname,PhoneNumber,Email,ImageFile")] CUser cUser)
        {
            if (id != cUser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (cUser.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + cUser.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cUser.ImageFile.CopyToAsync(fileStream);
                    }

                    cUser.Imagepath = fileName;

                }
                try
                {
                    _context.Update(cUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CUserExists(cUser.UserId))
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
            return View(cUser);
        }

        // GET: CUsers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.CUsers == null)
            {
                return NotFound();
            }

            var cUser = await _context.CUsers
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (cUser == null)
            {
                return NotFound();
            }

            return View(cUser);
        }

        // POST: CUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.CUsers == null)
            {
                return Problem("Entity set 'ModelContext.CUsers'  is null.");
            }
            var cUser = await _context.CUsers.FindAsync(id);
            if (cUser != null)
            {
                _context.CUsers.Remove(cUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CUserExists(decimal id)
        {
          return (_context.CUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
