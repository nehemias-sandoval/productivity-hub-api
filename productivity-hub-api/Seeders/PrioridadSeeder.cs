using productivity_hub_api.Models;

namespace productivity_hub_api.Seeders
{
    public static class PrioridadSeeder
    {
        public static void SeedPrioriodad(StoreContext context)
        {
            if (!context.Prioridades.Any())
            {
                var Prioriades = new List<Prioridad>
                {
                    new Prioridad {Nombre = "Alta", Color = "#eb459f"},
                    new Prioridad {Nombre = "Media", Color = "#faa61a"},
                    new Prioridad {Nombre = "Baja", Color = "#23a55a"}
                };
            }
        }
    }
}