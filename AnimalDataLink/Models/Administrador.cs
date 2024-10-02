using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalDataLinkApp.Models
{
    [Table("administrador")]
    public class Administrador
    {
        [Key]
        public int Id { get; set; }
        public int idUsuario { get; set; }
        public int activo { get; set; }
    }
}
