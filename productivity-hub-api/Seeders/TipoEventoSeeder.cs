using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Models;

namespace productivity_hub_api.Seeders
{
    public static class TipoEventoSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StoreContext(
                serviceProvider.GetRequiredService<DbContextOptions<StoreContext>>()))
            {
                if (!context.TipoEventos.Any())
                {
                    var tipoEventos = new List<TipoEvento>
                    {
                        new TipoEvento {Nombre = "Cumpleaños", Icono = "https://www.svgrepo.com/show/401192/birthday-cake.svg"},
                        new TipoEvento {Nombre = "Aniversario", Icono = "https://www.svgrepo.com/show/419478/day-dinner-love.svg"},
                        new TipoEvento {Nombre = "Navidad", Icono = "https://www.svgrepo.com/show/492736/christmas-cityscape.svg"},
                        new TipoEvento {Nombre = "Día del Padre", Icono = "https://www.svgrepo.com/show/393434/dad-daddy-heart-love.svg"},
                        new TipoEvento {Nombre = "Día de la Madre", Icono = "https://www.svgrepo.com/show/288812/calendar-mothers-day.svg"}
                    };

                    context.TipoEventos.AddRange(tipoEventos);
                    context.SaveChanges();
                }
            }
        }
    }
}
