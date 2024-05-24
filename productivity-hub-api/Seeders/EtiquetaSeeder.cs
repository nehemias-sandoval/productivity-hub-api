using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Models;

namespace productivity_hub_api.Seeders
{
    public class EtiquetaSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StoreContext(
                serviceProvider.GetRequiredService<DbContextOptions<StoreContext>>()))
            {
                if (!context.Etiquetas.Any())
                {
                    var etiquetas = new List<Etiqueta>
                    {
                        new Etiqueta {Nombre = "Pendiente", Color = "#f23f42"},
                        new Etiqueta {Nombre = "En Progreso", Color = "#faa61a"},
                        new Etiqueta {Nombre = "Completada", Color = "#219e56"}
                    };

                    context.Etiquetas.AddRange(etiquetas);
                    context.SaveChanges();
                }
            }
        }
    }
}
