using Microsoft.EntityFrameworkCore;
using ORTastic.Models;

namespace ORTastic.Context
{
    public class ORTasticDatabaseContext : DbContext
    {

        public ORTasticDatabaseContext(DbContextOptions<ORTasticDatabaseContext>
 options) : base(options)
        {
        }

        //creo tabla usuario con los atributos de mi modelo user
        public DbSet<User> Usuarios { get; set; }

        public DbSet<Evento> Eventos { get; set; }

        public DbSet<Compra> Compras { get; set; }

    }
}
