﻿using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Models;

namespace productivity_hub_api.Seeders
{
    public static class FrecuenciaSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StoreContext(
                serviceProvider.GetRequiredService<DbContextOptions<StoreContext>>()))
            {
                if (!context.Frecuencias.Any())
                {
                    var frecuencias = new List<Frecuencia>
                    {
                        new Frecuencia {Nombre = "Diaria"},
                        new Frecuencia {Nombre = "Semanal"},
                        new Frecuencia {Nombre = "Mensual"}
                    };

                    context.Frecuencias.AddRange(frecuencias);
                    context.SaveChanges();
                }
            }
        }
    }
}