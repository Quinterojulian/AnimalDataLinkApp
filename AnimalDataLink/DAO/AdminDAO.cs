using AnimalDataLinkApp.Models;
using AnimalDataLinkApp.Models.ViewModel;

namespace AnimalDataLinkApp.DAO
{
    public class AdminDao
    {
        private ContextDB _dbContexto = new ContextDB();

        public List<CuidadorViewModel> CuidadorList ()
        {
            List<CuidadorViewModel> cuidadors = new List<CuidadorViewModel>();

            cuidadors = (from cuidador in _dbContexto.cuidadors
                        select new CuidadorViewModel
                        {
                            id = cuidador.Id,
                            nombre = cuidador.usuarioForanea.nombreCompleto,
                            email = cuidador.email,
                            mobile = cuidador.mobile,
                            disponible = cuidador.disponible
                        }).ToList();

            return cuidadors;
        }

        public List<string> AsignarCuidador(AsignarViewModel asignar) 
        {
            List<string> errores = new List<string>();

            foreach(var item in asignar.animales) 
            {
                if (item.EstaSeleccionado) 
                {
					Animales animales = _dbContexto.animales.Find(item.Id);
					animales.idCuidador = asignar.idCuidador;
					animales.disponible = 1;

					_dbContexto.animales.Update(animales);

					try
					{
						_dbContexto.SaveChanges();

					}
					catch (Exception ex)
					{
						string error = "el animal no se pudo asignar con id:" + item.Id + " y de nombre " + item.Nombre;
						errores.Add(error);
					}
				}                
            }

            return errores;
        } 
    }
}
