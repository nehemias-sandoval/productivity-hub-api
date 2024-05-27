using productivity_hub_api.DTOs.Subtarea;
using productivity_hub_api.DTOs.Tarea;

namespace productivity_hub_api.Service.TareaService
{
    public interface ISubtareaService<SubtareaDto, CreateSubtareaDto, UpdateSubtareaDto>
    {
        Task<IEnumerable<SubtareaDto>> GetAllAsync(bool? pendientes, int idTarea);

        Task<SubtareaDto?> GetByIdAsync(int id);

        Task<SubtareaDto?> AddAsync(CreateSubtareaDto createDto);

        Task<SubtareaDto?> UpdateAsync(int id, UpdateSubtareaDto updateDto);

        Task<bool> DeleteAsync(int id);

        Task<SubtareaDto?> ChangeStateAsync(int id);
    }
}
