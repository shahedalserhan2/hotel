using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheLearningHub_Hotels.Models;

namespace TheLearningHub_Hotels.Controllers
{
    public class LoginandRegistrationController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public LoginandRegistrationController(ModelContext context, IWebHostEnvironment webHostEnviroment)


        {

            _context = context;

            _webHostEnviroment = webHostEnviroment;

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Username,Passwordd")] CUserlogin userLogin)
        {
           
            var auth = _context.CUserlogins.Where(x => x.Username == userLogin.Username && x.Passwordd == userLogin.Passwordd).FirstOrDefault();
             
            if (auth.Roleid == 1)
            {
                HttpContext.Session.SetString("name", auth.Username);
                HttpContext.Session.SetInt32("custid", (int)auth.UserId);
                //HttpContext.Session.SetInt32("UserId", (int)auth.UserId);

                //var name = HttpContext.Session.GetString("name");
                //var num= HttpContext.Session.GetInt32("custid").ToString();
                return RedirectToAction("Index", "Home");

            }
            else if (auth.Roleid == 2)
            {
                HttpContext.Session.SetInt32("adminid", (int)auth.UserId);
                HttpContext.Session.SetString("adminname", auth.Username);


                return RedirectToAction("Index", "Admin");
            }
            //else if (auth.Roleid == 1)
            //{
            //    HttpContext.Session.SetInt32("Gusetid", (int)auth.UserId);
            //    return RedirectToAction("Index", "Guset");
            //}
            return View(userLogin);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserId,Fname,Lname,PhoneNumber,Email,ImageFile")] CUser cUser, string Username, string Passwordd)
        {
            if (ModelState.IsValid)
            {
                if (cUser.ImageFile != null)
                {
                    // 1- Get the root path
                    string wwwRootPath = _webHostEnviroment.WebRootPath;

                    // 2- Generate unique file name
                    string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(cUser.ImageFile.FileName);

                    // 3- Combine the path for the image
                    string path = Path.Combine(wwwRootPath, "images", fileName);

                    // 4- Upload the image to the folder "images"
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cUser.ImageFile.CopyToAsync(fileStream);
                    }

                    // 5- Save the file path in the database
                    cUser.Imagepath = fileName;
                    _context.Add(cUser);
                    await _context.SaveChangesAsync();

                    // 6- Create user login details
                    CUserlogin user = new CUserlogin();
                    user.Username = Username;
                    user.UserId = cUser.UserId;
                    user.Passwordd = Passwordd;
                    user.Roleid = 1;

                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    // 7- Redirect to the login page after successful registration
                    return RedirectToAction("Login", "LoginandRegistration");
                }
            }
            return View(cUser);
        }
    }
}




