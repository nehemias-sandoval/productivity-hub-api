using productivity_hub_api.Models;

namespace productivity_hub_api.Seeders
{
    public static class FrecuenciaSeeder
    {
        public static void SeedFrecuencia(StoreContext context)
        {
            if(!context.Frecuencias.Any())
            {
                var frecuencias = new List<Frecuencia>
                {
                    new Frecuencia {Nombre = "Diario"},
                    new Frecuencia {Nombre = "Semanal"},
                    new Frecuencia {Nombre = "Mensual"}
                };

                context.Frecuencias.AddRange(frecuencias);
                context.SaveChanges();
            }
        }
    }
}
