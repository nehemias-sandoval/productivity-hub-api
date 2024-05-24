namespace productivity_hub_api.Service.AuthService
{
    public interface IUsuarioService<TDto, TCreateDto, TUpdateDto, TAuthenticateReqDto, TAuthenticateResDto>
    {
        Task<TAuthenticateResDto?> Authenticate(TAuthenticateReqDto authenticateReqDto);

        Task<TDto?> GetByIdAsync(int id);

        Task<TDto> AddAsync(TCreateDto createDto);

        Task<TDto?> UpdateAsync(TCreateDto TUpdateDto);
    }
}
