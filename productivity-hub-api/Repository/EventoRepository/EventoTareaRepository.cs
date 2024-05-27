using productivity_hub_api.Models;

namespace productivity_hub_api.Repository.EventoRepository
{
    public class EventoTareaRepository
    {
        private StoreContext _context;

        public EventoTareaRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EventoTarea eventoTarea) => await _context.EventosTareas.AddAsync(eventoTarea);

        public void Delete(IEnumerable<EventoTarea> eventoTareas) => _context.EventosTareas.RemoveRange(eventoTareas);

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
