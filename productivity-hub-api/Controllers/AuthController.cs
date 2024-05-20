using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.helpers;
using productivity_hub_api.Service;

namespace productivity_hub_api.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUsuarioService<
            UsuarioDto, CreateUsuarioDto, UpdateUsuarioDto, AuthenticateReqDto, AuthenticateResDto> _usuarioService;

        private IValidator<AuthenticateReqDto> _loginValidator;
        private IValidator<CreateUsuarioDto> _createUsuarioValidator;
        private IValidator<UpdateUsuarioDto> _updateUsuarioValidator;


        public AuthController(
            IUsuarioService<
                UsuarioDto, CreateUsuarioDto, UpdateUsuarioDto, AuthenticateReqDto, AuthenticateResDto> usuarioService,
            IValidator<AuthenticateReqDto> loginValidator,
            IValidator<CreateUsuarioDto> createUsuarioValidator,
            IValidator<UpdateUsuarioDto> updateUsuarioValidator) 
        {

            _usuarioService = usuarioService;
            _loginValidator = loginValidator;
            _createUsuarioValidator = createUsuarioValidator;
            _updateUsuarioValidator = updateUsuarioValidator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(AuthenticateReqDto authenticateReq)
        {
            var validationResult = await _loginValidator.ValidateAsync(authenticateReq);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = await _usuarioService.Authenticate(authenticateReq);
            if (response == null)
                return BadRequest(new { message = "Usuario o contraseña no valida" });

            return Ok(response);
        }

        [HttpGet("usuario/{id}")]
        [Authorize]
        public async Task<ActionResult<UsuarioDto>> GetById(int id)
        {
            var proyetoDto = await _usuarioService.GetByIdAsync(id);
            return proyetoDto == null ? NotFound() : Ok(proyetoDto);
        }

        [HttpPost("usuario")]
        public async Task<ActionResult<UsuarioDto>> Add(CreateUsuarioDto createUsuarioDto)
        {
            var validationResult = await _createUsuarioValidator.ValidateAsync(createUsuarioDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var usuarioDto = await _usuarioService.AddAsync(createUsuarioDto);
            return CreatedAtAction(nameof(GetById), new { id = usuarioDto.Id }, usuarioDto);
        }
    }
}
