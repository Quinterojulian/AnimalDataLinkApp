namespace AnimalDataLinkApp.Models.ViewModel
{  
    public class AsignarViewModel
    {
        public int idCuidador { get; set; }
        public List<AnimalDisponible> animales { get; set; }   
    }

    public class AnimalDisponible
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool EstaSeleccionado { get; set; } 
    }
}
