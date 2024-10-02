using AnimalDataLinkApp.DAO;
using AnimalDataLinkApp.Models;
using AnimalDataLinkApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AnimalDataLinkApp.Controllers
{
	public class CuidadorController : Controller
	{
		public IActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				return View();

			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		public IActionResult AnimalCreate()
		{
			if (User.Identity.IsAuthenticated)
			{
				return View();

			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
		[HttpPost]
		public IActionResult AnimalCreate(AnimalCreate crear)
		{
			if (ModelState.IsValid)
			{
				CuidadorDAO addAnimal = new CuidadorDAO();

				if (addAnimal.CreateAnimal(crear))
				{
					TempData["AnimalCreate"] = JsonConvert.SerializeObject(crear); // Serializar el modelo

					return RedirectToAction("ConfirmarCreacion");
				}
			}

			return View(crear);
		}

		public IActionResult ConfirmarCreacion()
		{
			try
			{
				AnimalCreate model = JsonConvert.DeserializeObject<AnimalCreate>(TempData["AnimalCreate"].ToString());
				return View(model);
			}
			catch
			{
				return RedirectToAction("Error", "Home");
			}

		}

		public IActionResult RegistrarSalud()
		{
			if (User.Identity.IsAuthenticated)
			{
				string nombreUsuario = User.FindFirst(ClaimTypes.Name)?.Value;
				int id = 0;

				int.TryParse(User.FindFirst(ClaimTypes.PrimarySid)?.Value, out id);

				AnimalDAO animalDAO = new AnimalDAO();
				UsuarioDAO usuarioDAO = new UsuarioDAO();

				int cuidador = usuarioDAO.GetCuidador(id);

				List<Animales> listAnimales = animalDAO.GetAnimales().Where(x => x.idCuidador == cuidador).ToList();

				ViewBag.listAnimales = listAnimales.Select(t => new SelectListItem
				{
					Value = t.idAnimal.ToString(),
					Text = t.nombreAnimal
				}).ToList();

				ViewBag.listAnimales.Insert(0, new SelectListItem
				{
					Value = "",  // Valor vacío o nulo para evitar que sea seleccionado por defecto en el formulario
					Text = "Seleccione uno",
					Selected = true  // Para que esta opción sea la seleccionada por defecto
				});

				return View();

			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public IActionResult RegistrarSalud(RegistrarSaludViewModel registrarSalud)
		{
			AnimalDAO animalDAO = new AnimalDAO();
			if (ModelState.IsValid)
			{
				CuidadorDAO addAnimal = new CuidadorDAO();

				if (addAnimal.RegistrieHealth(registrarSalud))
				{
					

					var animal = animalDAO.GetAnimales().Where(x => x.idAnimal == registrarSalud.idAnimal).FirstOrDefault();
					RegistroSaludExitoso exitoso = new RegistroSaludExitoso();
					exitoso.nombreAnimal = animal.nombreAnimal;
					exitoso.especie = animal.especie;
					exitoso.habitat = animal.habitat;
					exitoso.tipoComida = animal.tipoComida;
					exitoso.aseo = animal.aseo;
					exitoso.examenesLab = registrarSalud.examenesLab;
					exitoso.enfermedades = registrarSalud.enfermedades;
					exitoso.controlPeso = registrarSalud.controlPeso;
					exitoso.vacunas = registrarSalud.vacunas;
					exitoso.alergias = registrarSalud.alergias;
					exitoso.medicamentos = registrarSalud.medicamentos;

					TempData["RegistroSaludExitoso"] = JsonConvert.SerializeObject(exitoso); // Serializar el modelo

					return RedirectToAction("ConfirmarReport");
				}
			}

			UsuarioDAO usuarioDAO = new UsuarioDAO();
			int id = 0;
			
			int.TryParse(User.FindFirst(ClaimTypes.PrimarySid)?.Value, out id);
			int cuidador = usuarioDAO.GetCuidador(id);

			List<Animales> listAnimales = animalDAO.GetAnimales().Where(x => x.idCuidador == cuidador).ToList();

			ViewBag.listAnimales = listAnimales.Select(t => new SelectListItem
			{
				Value = t.idAnimal.ToString(),
				Text = t.nombreAnimal
			}).ToList();

			ViewBag.listAnimales.Insert(0, new SelectListItem
			{
				Value = "",  // Valor vacío o nulo para evitar que sea seleccionado por defecto en el formulario
				Text = "Seleccione uno",
				Selected = true  // Para que esta opción sea la seleccionada por defecto
			});

			return View(registrarSalud);
		}

		public IActionResult GetAnimalDetails(int id)
		{
			AnimalDAO animalDAO = new AnimalDAO();
			// Aquí buscarías el animal en tu base de datos o lista
			var animal = animalDAO.GetAnimales().Where(x => x.idAnimal == id).FirstOrDefault();

			if (animal == null)
			{
				return NotFound();
			}

			// Devuelve los datos en formato JSON
			return Json(new
			{
				nombre = animal.nombreAnimal,
				especie = animal.especie,
				habitat = animal.habitat,
				tipoComida = animal.tipoComida,
				aseo = animal.aseo
			});
		}

		public IActionResult ConfirmarReport()
		{
			try
			{
				RegistroSaludExitoso model = JsonConvert.DeserializeObject<RegistroSaludExitoso>(TempData["RegistroSaludExitoso"].ToString());
				return View(model);
			}
			catch
			{
				return RedirectToAction("Error", "Home");
			}
		}
	}
}
