using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AnimalDataLinkApp.Models
{
    public class ContextDB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			// base.OnConfiguring(optionsBuilder);
			// optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AnimalDataLinkDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=tcp:serv123.database.windows.net,1433;Initial Catalog=AnimalDataLinkDB;User ID=adminAnimal;Password=An1mlPl4n3t;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
			}
		}

        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Administrador> administradors { get; set; }
        public DbSet<Cuidador> cuidadors { get; set; }
        public DbSet<Animales> animales { get; set; }
        public DbSet<RegistroSalud> registros { get; set; }
    }
}
