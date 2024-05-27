using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using productivity_hub_api.Models;

namespace productivity_hub_api.Seeders
{
    public static class TipoNotificacionSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StoreContext(
                serviceProvider.GetRequiredService<DbContextOptions<StoreContext>>()))
            {
                if (!context.TipoNotificaciones.Any())
                {
                    var TpNotificacion = new List<TipoNotificacion>
                    {
                        new TipoNotificacion {Nombre = "Recordatorio diario", Descripcion = "Tiene una tarea pendiente por terminar. Te invitamos a seguir progresando continuamente."},
                        new TipoNotificacion {Nombre = "Vencimiento", Descripcion = "Tu tarea se ha vencido. Recuerda revisar tu progreso diariamente."},                        
                    };

                    context.TipoNotificaciones.AddRange(TpNotificacion);
                    context.SaveChanges();
                }
            }
        }
    }
}
