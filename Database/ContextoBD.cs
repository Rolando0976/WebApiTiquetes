using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebApiTiquetes.Models;

namespace WebApiTiquetes.DataBase
{
    public class ContextoBD : DbContext
    {
        public ContextoBD(DbContextOptions<ContextoBD> options) : base(options) { }

        public DbSet<Roles> roles { get; set; }
        public DbSet<Usuarios> usuarios { get; set; }
        public DbSet<Ticketes> Ticketes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().HasKey(x => x.ro_identificador);
            modelBuilder.Entity<Usuarios>().HasKey(x => x.us_identificador);
            modelBuilder.Entity<Ticketes>().HasKey(x => x.ti_identificador);

        }
        
    }
    
}
