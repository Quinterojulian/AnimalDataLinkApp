using System.ComponentModel.DataAnnotations;

namespace AnimalDataLinkApp.Models.ViewModel
{
    public class UsuarioCreateF
    {
        public string usuario { get; set; }
        public string pass { get; set; }

        [Required(ErrorMessage = "El campo no puede estar vacio")]
        public string nombreCompleto { get; set; }
        [EmailAddress(ErrorMessage = "Debe ser una direcci[on valida de correo electronico")]
        public string email { get; set; }
        [Phone(ErrorMessage = "No es un telefono valido")]
        public string telefono { get; set; }
        public int admin { get; set; }
    }
}
