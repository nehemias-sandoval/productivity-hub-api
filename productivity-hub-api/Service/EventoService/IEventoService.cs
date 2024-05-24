namespace productivity_hub_api.Service.EventoService
{
    public interface IEventoService<EventoDto, CreateEventoDto, UpdateEventoDto>
    {
        Task<IEnumerable<EventoDto>> GetAllAsync(bool? vencido);

        Task<EventoDto?> GetByIdAsync(int id);

        Task<EventoDto?> AddAsync(CreateEventoDto createDto);

        Task<EventoDto?> UpdateAsync(int id, UpdateEventoDto updateDto);

        Task<EventoDto?> DeleteAsync(int id);
    }
}
