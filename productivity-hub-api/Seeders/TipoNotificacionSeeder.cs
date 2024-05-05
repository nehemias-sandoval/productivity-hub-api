using productivity_hub_api.Models;

namespace productivity_hub_api.Seeders
{
    public static class TipoNotificacionSeeder
    {
        public static void SeedNotificacion(StoreContext context)
        {
            if(!context.TipoNotificaciones.Any())
            {
                var TpNotificacion = new List<TipoNotificacion>
                {
                    new TipoNotificacion {Nombre = "Recordatorio", Descripcion = "Tiene una tarea pendiente por terminar. Te invitamos a seguir progresando continuamente."},
                    new TipoNotificacion {Nombre = "Vencimiento", Descripcion = "Tu tarea se ha vencido. Recuerda revisar tu progreso diariamente."},
                    new TipoNotificacion {Nombre = "Asignación", Descripcion = "Se te asignó una nueva tarea. Da lo mejor de ti y completala a tiempo."},
                    new TipoNotificacion {Nombre = "Invitación", Descripcion = "Se te ha invitado a un proyecto. Puedes revisar los detalles en tu correo electrónico."}
                };

                context.TipoNotificaciones.AddRange(TpNotificacion);
                context.SaveChanges();
            }
        }
    }
}
