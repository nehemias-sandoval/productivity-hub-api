using productivity_hub_api.Models;

namespace productivity_hub_api.Repository
{
    public class EventoTareaRepository
    {
        private StoreContext _context;

        public EventoTareaRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EventoTarea eventoTarea) => await _context.EventosTareas.AddAsync(eventoTarea);
    }
}
