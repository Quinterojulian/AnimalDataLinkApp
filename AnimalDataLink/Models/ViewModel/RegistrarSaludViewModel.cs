using System.ComponentModel.DataAnnotations;

namespace AnimalDataLinkApp.Models.ViewModel
{
	public class RegistrarSaludViewModel
	{
		[Required(ErrorMessage = "debe de elegir un animal para el registro")]
		[Range(1, int.MaxValue, ErrorMessage = "debe de elegir un animal para el registro")]
		public int idAnimal { get; set; }
		[Display(Name = "Examenes de Laboratorio:")]
		[Required(ErrorMessage = "campo obligatorio")]
		public string examenesLab { get; set; }
		[Display(Name = "Enfermedades:")]
		[Required(ErrorMessage = "campo obligatorio")]
		public string enfermedades { get; set; }
		[Display(Name = "Control de peso:")]
		[Required(ErrorMessage = "campo obligatorio")]
		public string controlPeso { get; set; }
		[Required(ErrorMessage = "campo obligatorio")]
		[Display(Name = "Vacunas:")]
		public string vacunas { get; set; }
		[Required(ErrorMessage = "campo obligatorio")]
		[Display(Name = "Alergias:")]
		public string alergias { get; set; }
		[Required(ErrorMessage = "campo obligatorio")]
		[Display(Name = "Medicamentos:")]
		public string medicamentos { get; set; }
	}
}
