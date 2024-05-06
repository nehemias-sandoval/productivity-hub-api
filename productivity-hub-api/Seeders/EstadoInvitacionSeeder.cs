using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using productivity_hub_api.Models;

namespace productivity_hub_api.Seeders
{
    public static class EstadoInvitacionSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StoreContext(
                serviceProvider.GetRequiredService<DbContextOptions<StoreContext>>()))
            {
                if (!context.EstadoInvitaciones.Any())
                {
                    var estadoInvitaciones = new List<EstadoInvitacion>
                    {
                        new EstadoInvitacion {Nombre = "Pendiente"},
                        new EstadoInvitacion {Nombre = "Aceptada"},
                        new EstadoInvitacion {Nombre = "Rechazada"}
                    };

                    context.EstadoInvitaciones.AddRange(estadoInvitaciones);
                    context.SaveChanges();
                }
            }
        }
    }
}
