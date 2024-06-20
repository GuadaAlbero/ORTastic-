using Microsoft.AspNetCore.Mvc;
using ORTastic.Context;
using ORTastic.Models;

namespace ORTastic.Controllers
{
    public class AccountController : Controller
    {
        private readonly ORTasticDatabaseContext _context;
        public AccountController(ORTasticDatabaseContext context) { _context = context; }

        [HttpGet]

        //llamo vista de Login
        public IActionResult Login() {

            return View();
        }
        [HttpPost]

        //accion de logeo donde paso parametros
        public async Task <IActionResult> Login (User user) {


            if (ModelState.IsValid) { 
            var loggedInUser = _context.Usuarios.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

                if (loggedInUser != null)
                {
                    //establezca sesion y guarde en username el username
                    HttpContext.Session.SetInt32("tipoPerfil", (int)loggedInUser.tipoPerfil);

                    // si no estoy logeado se va al index de mi home
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Nombre de usuario y contraseña incorrecta");
            }

            return View(user);

        }

        [HttpGet]

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(User user) { 
        
            if (ModelState.IsValid)
            {
                _context.Add(user);

                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(user);
        }

        public IActionResult Logout() { 

        HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }

}
