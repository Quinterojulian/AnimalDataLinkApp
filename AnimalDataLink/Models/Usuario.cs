using Microsoft.EntityFrameworkCore.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalDataLinkApp.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }
        public string usuario { get; set; }
        public string nombreCompleto { get; set; }
        public string pass { get; set; }
        public DateTime fechaRegistro { get; set; }

        public ICollectionLoader<Cuidador> cuidadorLoader { get;}
    }
}
