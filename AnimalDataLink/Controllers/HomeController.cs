using AnimalDataLinkApp.DAO;
using AnimalDataLinkApp.Models;
using AnimalDataLinkApp.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;

namespace AnimalDataLinkApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            if (User.Identity.IsAuthenticated)
            {              
                return View();
            }
            else
            {
                // El usuario no ha iniciado sesión
                return RedirectToAction("LogIn", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(UsuarioLogIn logIn)
        {
            UsuarioDAO dao = new UsuarioDAO();
            UsuarioIn existe = dao.LogInApp(logIn);
            if (existe != null) 
            {
                // Crear una lista mínima de claims solo para indicar que el usuario ha iniciado sesión
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, existe.usuario),  // Usar el nombre de usuario como claim
                    new Claim(ClaimTypes.GivenName, existe.nombreCompleto),  // Nombre completo
                    new Claim(ClaimTypes.PrimarySid, existe.id.ToString())  // Nombre completo
                };

                // Crear el ClaimsIdentity y ClaimsPrincipal
                var claimsIdentity = new ClaimsIdentity(claims, "login");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Autenticar al usuario
                await HttpContext.SignInAsync(claimsPrincipal);

                if(dao.usuarioIsAdmin(existe.id))
                    return RedirectToAction("Index", "Administrador");
                else
                    return RedirectToAction("Index", "Cuidador");
            }
            return View();
        }

        public IActionResult Create() { return View(); }

        [HttpPost]
        public IActionResult Create(UsuarioCreate usuarioCrear)
        {
            UsuarioDAO dao = new UsuarioDAO();

            if (ModelState.IsValid) 
            {
                if (dao.usuarioExiste(usuarioCrear.usuario)) 
                {
                    ModelState.AddModelError("Usuario", "El nombre de usuario ya existe en el sistema");
                    return View(usuarioCrear);
                }
                TempData["UsuarioCreate"] = JsonConvert.SerializeObject(usuarioCrear); // Serializar el modelo
                return RedirectToAction("CreateFinal");
            }        

            return View(usuarioCrear);
        }

       
        public IActionResult CreateFinal()
        {
            if (TempData["UsuarioCreate"] != null)
            {
                var model = JsonConvert.DeserializeObject<UsuarioCreate>(TempData["UsuarioCreate"].ToString());

                UsuarioCreateF usuario = new UsuarioCreateF();
                usuario.usuario = model.usuario;
                usuario.pass = model.pass;

                return View(usuario);

            }
            return RedirectToAction("Create");
        }

        [HttpPost]
        public IActionResult CreateFinal(UsuarioCreateF usuarioF)
        {
            UsuarioDAO dao = new UsuarioDAO();

            if (ModelState.IsValid)
            {
                if (!dao.GetAll())
                {
                    usuarioF.admin = 1;
                }

                if (dao.CreateUser(usuarioF)) 
                {
                    return RedirectToAction("UsuarioCreado");
                }
            }

            return View(usuarioF);
        }

        public IActionResult UsuarioCreado()
        {       
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }

    }
}
