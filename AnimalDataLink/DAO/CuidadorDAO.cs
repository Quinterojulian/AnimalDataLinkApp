using AnimalDataLinkApp.Models.ViewModel;
using AnimalDataLinkApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalDataLinkApp.DAO
{
    public class CuidadorDAO
    {
        private ContextDB _dbContexto = new ContextDB();
        public bool CreateAnimal(AnimalCreate animal)
        {
            bool respuesta = false;

            Animales animalRegistro = new Animales();
            animalRegistro.nombreAnimal = animal.nombreAnimal;
            animalRegistro.especie = animal.especie;
            animalRegistro.habitat = animal.habitat;
            animalRegistro.tipoComida = animal.tipoComida;
            animalRegistro.aseo = animal.aseo;

            _dbContexto.animales.Add(animalRegistro);

            try
            {
                _dbContexto.SaveChanges();
                respuesta = true;
            }
            catch (Exception ex)
            {
                respuesta = false;
            }

            return respuesta;
        }

        public bool RegistrieHealth(RegistrarSaludViewModel registrarSalud)
        {
			RegistroSalud registro = new RegistroSalud();

            registro.idAnimal = registrarSalud.idAnimal;
            registro.examenesLab  = registrarSalud.examenesLab;
            registro.enfermedades = registrarSalud.enfermedades;
            registro.controlPeso = registrarSalud.controlPeso;
			registro.vacunas = registrarSalud.vacunas;
			registro.alergias = registrarSalud.alergias;
            registro.medicamentos = registrarSalud.medicamentos;
            registro.fechaRegistro = DateTime.Now;

			_dbContexto.registros.Add(registro);

			try
			{
				_dbContexto.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
			
        }
    }
}
