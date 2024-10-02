using System.ComponentModel.DataAnnotations;

namespace AnimalDataLinkApp.Models.ViewModel
{
	public class RegistroSaludExitoso
	{
		public string nombreAnimal { get; set; }
		public string especie { get; set; }
		public string habitat { get; set; }
		public string tipoComida { get; set; }
		public string aseo { get; set; }
		public string examenesLab { get; set; }
		public string enfermedades { get; set; }
		public string controlPeso { get; set; }
		public string vacunas { get; set; }
		public string alergias { get; set; }
		public string medicamentos { get; set; }
	}
}
