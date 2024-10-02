using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalDataLinkApp.Models
{
    [Table("RegistroSalud")]
    public class RegistroSalud
    {
        [Key]
        public int Id { get; set; }		
		public int idAnimal { get; set; }
        public string examenesLab { get; set; }
        public string enfermedades { get; set; }
        public string controlPeso { get; set; }
        public string vacunas { get; set; }
        public string alergias { get; set; }
        public string medicamentos { get; set; }
        public DateTime? fechaRegistro { get; set; }
		[ForeignKey("idAnimal")]
		public Animales animales { get; set; }
    }
}
