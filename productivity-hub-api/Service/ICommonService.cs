namespace productivity_hub_api.Service
{
    public interface ICommonService<TDto, TCreateDto, TUpdateDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync();

        Task<TDto?> GetByIdAsync(int id);

        Task<TDto> AddAsync(TCreateDto createDto);

        Task<TDto?> UpdateAsync(int id, TUpdateDto updateDto);

        Task<TDto?> DeleteAsync(int id);
    }
}
