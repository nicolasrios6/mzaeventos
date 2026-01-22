using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MzaEventos.Models;
using System.Reflection.Emit;

namespace MzaEventos.Data
{
    public class EventosDbContext : IdentityDbContext<Usuario>
    {
        public EventosDbContext(DbContextOptions<EventosDbContext> options) : base(options)
        {
        }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuración de relaciones
            builder.Entity<Evento>()
                .HasOne(e => e.Categoria)
                .WithMany(c => c.Eventos)
                .HasForeignKey(e => e.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict); // Evita borrado en cascada
        }
    }
}
