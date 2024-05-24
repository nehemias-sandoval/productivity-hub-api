namespace productivity_hub_api.Service.ProyectoService
{
    public interface IProyectoService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto>
    {
        Task<IEnumerable<ProyectoDto>> GetAllAsync(bool? estado);

        Task<ProyectoDto?> GetByIdAsync(int id);

        Task<ProyectoDto?> AddAsync(CreateProyectoDto createDto);

        Task<ProyectoDto?> UpdateAsync(int id, UpdateProyectoDto updateDto);

        Task<ProyectoDto?> DeleteAsync(int id);

        Task ChangeEstadoAsync(int id);
    }
}
