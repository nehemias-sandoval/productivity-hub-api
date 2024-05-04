using Microsoft.EntityFrameworkCore;

namespace productivity_hub_api.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
             
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventoRecordatorio>()
                .HasKey(x => new { x.IdEvento, x.IdRecordatorio});

            modelBuilder.Entity<EventoTarea>()
                .HasKey(x => new { x.IdEvento, x.IdTarea });

            modelBuilder.Entity<ProyectoTarea>()
                .HasKey(x => new { x.IdProyecto, x.IdTarea });

            modelBuilder.Entity<TareaEtiqueta>()
                .HasKey(x => new { x.IdTarea, x.IdEtiqueta });

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

        public DbSet<TareaEtiqueta> TareaEtiquetas { get; set; }

    }
}
