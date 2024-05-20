using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.DTOs.Proyecto;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace productivity_hub_api.Service
{
    public class UsuarioService : IUsuarioService<UsuarioDto, CreateUsuarioDto, UpdateUsuarioDto, AuthenticateReqDto, AuthenticateResDto>
    {
        private AppSettings _appSettings;
        private IUsuarioRepository _usuarioRepository;
        private IMapper _mapper;

        public UsuarioService(IOptions<AppSettings> appSettings, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<AuthenticateResDto?> Authenticate(AuthenticateReqDto authenticateReqDto)
        {
            var usuario = await _usuarioRepository.Validate(authenticateReqDto.Email, authenticateReqDto.Password);
            
            if (usuario != null)
            {
                var token = await generateJwtToken(usuario);

                var authenticateResDto = _mapper.Map<AuthenticateResDto>(usuario);
                authenticateResDto.Token = token;

                return authenticateResDto;
            }

            return null;
        }

        public async Task<UsuarioDto?> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario != null)
            {
                var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

                return usuarioDto;
            }

            return null;
        }

        public async Task<UsuarioDto> AddAsync(CreateUsuarioDto createUsuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(createUsuarioDto);
            usuario.EncrypyPassword();

            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveAsync();

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return usuarioDto;
        }

        public Task<UsuarioDto?> UpdateAsync(CreateUsuarioDto TUpdateDto)
        {
            throw new NotImplementedException();
        }

        private async Task<string> generateJwtToken(Usuario usuario)
        {
            // Generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {

                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", usuario.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });

            return tokenHandler.WriteToken(token);
        }
    }
}
