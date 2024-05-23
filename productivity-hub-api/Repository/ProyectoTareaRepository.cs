using productivity_hub_api.Models;

namespace productivity_hub_api.Repository
{
    public class ProyectoTareaRepository
    {
        private StoreContext _context;

        public ProyectoTareaRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProyectoTarea proyectoTarea) => await _context.ProyectoTareas.AddAsync(proyectoTarea);
    }
}
