using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnimalDataLinkApp.Models.ViewModel
{
    public class AnimalCreate
    {
        [Display(Name = "Nombre del Animal")]
        public string nombreAnimal { get; set; }
        [Display(Name = "Especie")]
        public string especie { get; set; }
        [Display(Name = "Hábitat")]
        public string habitat { get; set; }
        [Display(Name = "Tipo Comida")]
        public string tipoComida { get; set; }
        [Display(Name = "Aseo")]
        public string aseo { get; set; }
    }
}
