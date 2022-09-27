using Microsoft.EntityFrameworkCore;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.Persistencia{
    public class ApplContext : DbContext{
        public DbSet<Familiar> Familiar {get; set;}
        public DbSet<Especialista> Especialista {get; set;}
        public DbSet<RegistroPatron> RegistroPatron {get; set;}
        public DbSet<Cuidados> Cuidados {get; set;}
        public DbSet<Historia> Historia {get; set;}
        public DbSet<Paciente> Paciente {get; set;}
        // Configuración a la Base de Datos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            // El String de Conexión se actualiza, según la instancia que tengas de Sql en tu pc
            string conectionData = "Data Source = localhost,1433; Initial Catalog = GrowthHormone; user = sa; password = pass;";
            if(!optionsBuilder.IsConfigured){
                optionsBuilder
                .UseSqlServer(conectionData);
            }
        }
    }
}