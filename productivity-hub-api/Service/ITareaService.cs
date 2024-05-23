using productivity_hub_api.DTOs.Tarea;

namespace productivity_hub_api.Service
{
    public interface ITareaService<TareaDto, CreateTareaDto, UpdateTareaDto, CreateTareaEtiquetaDto>
    {
        Task<IEnumerable<TareaDto>> GetAllAsync(bool? pendientes);

        Task<TareaDto?> GetByIdAsync(int id);

        Task<TareaDto?> AddAsync(CreateTareaDto createDto);

        Task<TareaDto?> UpdateAsync(int id, UpdateTareaDto updateDto);

        Task<TareaDto?> DeleteAsync(int id);

        Task<TareaDto?> ChangeStateAsync(int id);

        Task<TareaDto?> AddEtiquetaAsync(CreateTareaEtiquetaDto createDto);
    }
}
