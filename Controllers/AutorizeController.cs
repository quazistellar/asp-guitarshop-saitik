using guitarshop.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace guitarshop.Controllers
{
    public class AutorizeController : Controller
    {
        private readonly AspGuiznotesContext _context;
        private readonly ILogger<AutorizeController> _logger;

        public AutorizeController(ILogger<AutorizeController> logger, AspGuiznotesContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Cab()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Regist()
        {
            return View();
        }

        private string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";

            using (var sha256 = SHA256.Create())
            {

                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cab(string login, string password)
        {

            string hashedpass = HashPassword(password);

            var user = _context.Users.FirstOrDefault(u => u.LoginUser == login && u.PasswordUser == hashedpass);



            if (user != null)
            {
                var claim = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, user.NameClient),  
                     new Claim(ClaimTypes.Email, user.Email),
                     new Claim(ClaimTypes.Role,user.IdRole == 2? "Пользователь":"Администратор")
                };

                var claimIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return RedirectToAction("Ctlg", "Catalog");
            }
            ViewBag.ErrorMessage = "ERROR";
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Regist(string name, string password, string login, string surname, string email, string patr)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(surname))
            {
                ViewBag.ErrorMessage = "ERROR";
                return View();
            }

            if (_context.Users.Any(u => u.LoginUser == login))
            {
                ViewBag.ErrorMessage = "Пользователь с таким логином уже существует";
                return View();
            }

            string hashpassword = HashPassword(password);

            var user = new User
            {
                NameClient = name,
                PasswordUser = hashpassword,
                LoginUser = login,
                SurnameClient = surname,
                Email = email,
                PatronimClient = patr,
                IdRole = 2
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Cab");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home"); 
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
