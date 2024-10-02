using Microsoft.EntityFrameworkCore.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalDataLinkApp.Models
{
    [Table("Animales")]
    public class Animales
    {
        [Key]
        public int idAnimal {  get; set; }
        public string nombreAnimal { get; set; }
        public string especie { get; set; }
        public string habitat { get; set; }
        public string tipoComida { get; set; }
        public string aseo { get; set; }
        public int disponible { get; set; }	
		public int idCuidador { get; set; }

		public ICollectionLoader<RegistroSalud> saludoLoader { get; }
		[ForeignKey("idCuidador")]
		public Cuidador cuidador { get; set; }
	}
}
