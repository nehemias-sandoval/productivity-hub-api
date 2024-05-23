using productivity_hub_api.DTOs.Subtarea;
using productivity_hub_api.DTOs.Tarea;

namespace productivity_hub_api.Service
{
    public interface ISubtareaService<SubtareaDto, CreateSubtareaDto, UpdateSubtareaDto> 
    {
        Task<IEnumerable<SubtareaDto>> GetAllAsync(bool? pendientes);

        Task<SubtareaDto?> GetByIdAsync(int id);

        Task<SubtareaDto> AddAsync(CreateSubtareaDto createDto);

        Task<SubtareaDto?> UpdateAsync(int id, UpdateSubtareaDto updateDto);

        Task<SubtareaDto?> DeleteAsync(int id);

        Task<SubtareaDto?> ChangeStateAsync(int id);
    }
}
