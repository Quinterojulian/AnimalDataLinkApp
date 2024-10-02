using AnimalDataLinkApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalDataLinkApp.DAO
{
	public class AnimalDAO
	{
		private ContextDB _dbContexto = new ContextDB();
		public List<Animales> GetAnimales()
		{
			List<Animales> animales = new List<Animales>();

			animales = _dbContexto.animales.ToList();

			return animales;
		}
	}
}
