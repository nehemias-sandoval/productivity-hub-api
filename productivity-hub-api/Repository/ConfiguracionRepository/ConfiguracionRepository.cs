using productivity_hub_api.Models;

namespace productivity_hub_api.Repository.ConfiguracionRepository
{
    public class ConfiguracionRepository
    {
        private StoreContext _context;

        public ConfiguracionRepository(StoreContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(Configuracion configuracion) => await _context.Configuraciones.AddAsync(configuracion);

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
