using AnimalDataLinkApp.Models;
using AnimalDataLinkApp.Models.ViewModel;

namespace AnimalDataLinkApp.DAO
{
	public class UsuarioDAO
	{
		private ContextDB _dbContexto = new ContextDB();

		public bool CreateUser(UsuarioCreateF usuario)
		{
			bool respuesta = false;

			Usuario usuario1 = new Usuario();
			usuario1.nombreCompleto = usuario.nombreCompleto;
			usuario1.usuario = usuario.usuario;
			usuario1.pass = usuario.pass;
			usuario1.fechaRegistro = DateTime.Now;

			_dbContexto.usuario.Add(usuario1);

			try
			{
				_dbContexto.SaveChanges();
				respuesta = true;
			}
			catch (Exception ex)
			{
				respuesta = false;
			}


			if (respuesta)
			{
				if (usuario.admin == 1)
				{
					Administrador administrador = new Administrador();
					administrador.idUsuario = usuario1.idUsuario;
					_dbContexto.administradors.Add(administrador);

				}
				else
				{
					Cuidador cuidador = new Cuidador();
					cuidador.idUsuario = usuario1.idUsuario;
					cuidador.email = usuario.email;
					cuidador.mobile = usuario.telefono.Trim();
					cuidador.disponible = 0;
					_dbContexto.cuidadors.Add(cuidador);
				}

				try
				{
					_dbContexto.SaveChanges();
					respuesta = true;
				}
				catch (Exception ex)
				{
					string msg = ex.Message;
					respuesta = false;
				}
			}

			return respuesta;
		}

		public UsuarioIn LogInApp(UsuarioLogIn logIn)
		{

			UsuarioIn respuesta = (from usuario in _dbContexto.usuario
								   where usuario.usuario == logIn.usuario && usuario.pass == logIn.pass
								   select new UsuarioIn
								   {
									   id = usuario.idUsuario,
									   nombreCompleto = usuario.nombreCompleto,
									   usuario = usuario.usuario

								   }).FirstOrDefault();

			return respuesta;
		}

		public bool usuarioIsAdmin(int userId)
		{
			bool respuesta = false;

			try
			{
				respuesta = _dbContexto.administradors.Where(x => x.idUsuario == userId).Any();
			}
			catch (Exception ex)
			{
				return false;
			}


			return respuesta;
		}

		public bool GetAll()
		{
			return _dbContexto.usuario.Any();
		}

		public bool usuarioExiste(string usuario)
		{
			bool respuesta = false;

			try
			{
				respuesta = _dbContexto.usuario.Where(x => x.usuario.ToUpper() == usuario.ToUpper()).Any();
			}
			catch (Exception ex)
			{
				return false;
			}
			return respuesta;
		}

		public int GetCuidador(int idUsuario)
		{
			int idCuidaor = _dbContexto.cuidadors.Where(x => x.idUsuario == idUsuario).FirstOrDefault().Id;
			return idCuidaor;
		}
	}
}
