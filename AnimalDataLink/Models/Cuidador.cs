using Microsoft.EntityFrameworkCore.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace AnimalDataLinkApp.Models
{
    [Table("Cuidador")]
    public class Cuidador
    {
        [Key]
        public int Id { get; set; }
        public int idUsuario { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public int disponible { get; set; }
        [ForeignKey("idUsuario")]
        public Usuario usuarioForanea { get; set; }
		public ICollectionLoader<Cuidador> cuidadorLoader { get; }
	}
}
