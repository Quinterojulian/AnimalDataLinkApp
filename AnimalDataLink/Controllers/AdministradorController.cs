using AnimalDataLinkApp.DAO;
using AnimalDataLinkApp.Models;
using AnimalDataLinkApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace AnimalDataLinkApp.Controllers
{
    public class AdministradorController : Controller
    {

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string nombreUsuario = User.FindFirst(ClaimTypes.Name)?.Value;
                int id = 0;

                int.TryParse(User.FindFirst(ClaimTypes.PrimarySid)?.Value, out id);

                UsuarioDAO dao = new UsuarioDAO();
                if (dao.usuarioIsAdmin(id))
                {
                   return View();

                }

                return RedirectToAction("Index", "Cuidador");

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult AsignarMasivo()
        {
            if (User.Identity.IsAuthenticated)
            {
                string nombreUsuario = User.FindFirst(ClaimTypes.Name)?.Value;
                int id = 0;

                int.TryParse(User.FindFirst(ClaimTypes.PrimarySid)?.Value, out id);

                UsuarioDAO dao = new UsuarioDAO();
                if (dao.usuarioIsAdmin(id))
                {
                    AdminDao adminDao = new AdminDao();
                    AnimalDAO animalDao = new AnimalDAO();
                    var cuidadores = adminDao.CuidadorList();

                    ViewBag.cuidadores = cuidadores.Where(x => x.disponible == 0).Select(t => new SelectListItem
                    {
                        Value = t.id.ToString(),
                        Text = t.nombre
                    }).ToList();

                    AsignarViewModel asignar = new AsignarViewModel();

                    asignar.animales = animalDao.GetAnimales().Where(x=> x.disponible==0).Select(x=> new AnimalDisponible { Id = x.idAnimal,Nombre= x.nombreAnimal, EstaSeleccionado= false}).ToList();

                    return View(asignar);

                }

                return RedirectToAction("Index", "Cuidador");

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult AsignarMasivo(AsignarViewModel asignar) 
        {
			AdminDao adminDao = new AdminDao();
			AnimalDAO animalDao = new AnimalDAO();
			List<string> resultado = adminDao.AsignarCuidador(asignar);

            if(resultado.Count > 0) 
            { 
                ViewBag.errores = resultado;
            }

			var cuidadores = adminDao.CuidadorList();

			ViewBag.cuidadores = cuidadores.Where(x => x.disponible == 0).Select(t => new SelectListItem
			{
				Value = t.id.ToString(),
				Text = t.nombre
			}).ToList();

			asignar.animales = animalDao.GetAnimales().Where(x => x.disponible == 0).Select(x => new AnimalDisponible { Id = x.idAnimal, Nombre = x.nombreAnimal, EstaSeleccionado = false }).ToList();

			return View(asignar);
		}

    }
}
