using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Seeders;

namespace productivity_hub_api.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
               .Property(u => u.Google).HasDefaultValue(false);

            modelBuilder.Entity<Tarea>()
                .Property(t => t.IdEtiqueta).HasDefaultValue(1); // Pendiente

            modelBuilder.Entity<Subtarea>()
                .Property(s => s.Estado).HasDefaultValue(false);

            modelBuilder.Entity<Proyecto>()
                .Property(p => p.Estado).HasDefaultValue(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Persona> Personas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Proyecto> Proyectos {  get; set; }

        public DbSet<Tarea> Tareas { get; set; }

        public DbSet<Subtarea> Subtareas { get; set; }

        public DbSet<Evento> Eventos { get; set; }

        public DbSet<Prioridad> Prioridades { get; set;}

        public DbSet<Notificacion> Notificaciones { get; set; }

        public DbSet<Etiqueta> Etiquetas { get; set; }

        public DbSet<Recordatorio> Recordatorios { get; set; }

        public DbSet<Frecuencia> Frecuencias { get; set; }

        public DbSet<Configuracion> Configuraciones { get; set; }

        public DbSet<TipoEvento> TipoEventos { get; set; }

        public DbSet<TipoNotificacion> TipoNotificaciones { get; set; } 

        public DbSet<EventoRecordatorio> EventoRecordatorios { get; set; }

        public DbSet<EventoTarea> EventosTareas { get; set;}

        public DbSet<ProyectoTarea> ProyectoTareas { get; set; }
    }
}
