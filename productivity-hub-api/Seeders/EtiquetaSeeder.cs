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
                        new Etiqueta {Nombre = "Pendiente", Color = ""},
                        new Etiqueta {Nombre = "En Progreso", Color = ""},
                        new Etiqueta {Nombre = "Completada", Color = ""}
                    };

                    context.Etiquetas.AddRange(etiquetas);
                    context.SaveChanges();
                }
            }
        }
    }
}
