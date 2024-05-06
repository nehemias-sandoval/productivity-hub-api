using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using productivity_hub_api.Models;

namespace productivity_hub_api.Seeders
{
    public static class PrioridadSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StoreContext(
                serviceProvider.GetRequiredService<DbContextOptions<StoreContext>>()))
            {
                if (!context.Prioridades.Any())
                {
                    var Prioriades = new List<Prioridad>
                    {
                        new Prioridad {Nombre = "Alta", Color = "#eb459f"},
                        new Prioridad {Nombre = "Media", Color = "#faa61a"},
                        new Prioridad {Nombre = "Baja", Color = "#23a55a"}
                    };

                    context.Prioridades.AddRange(Prioriades);
                    context.SaveChanges();
                }
            }
        }
    }
}