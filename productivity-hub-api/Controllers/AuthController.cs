using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.DTOs.Catalogo;
using productivity_hub_api.Helpers;
using productivity_hub_api.Service.AuthService;

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

        private IHttpContextAccessor _httpContextAccessor;

        public AuthController(
            IUsuarioService<
                UsuarioDto, CreateUsuarioDto, UpdateUsuarioDto, AuthenticateReqDto, AuthenticateResDto> usuarioService,
            IValidator<AuthenticateReqDto> loginValidator,
            IValidator<CreateUsuarioDto> createUsuarioValidator,
            IValidator<UpdateUsuarioDto> updateUsuarioValidator,
            IHttpContextAccessor httpContextAccessor) 
        {

            _usuarioService = usuarioService;
            _loginValidator = loginValidator;
            _createUsuarioValidator = createUsuarioValidator;
            _updateUsuarioValidator = updateUsuarioValidator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("usuario")]
        [Authorize]
        public ActionResult<UsuarioDto> GetEtiquetas()
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            if (usuarioDto == null) return NotFound();

            return Ok(usuarioDto);
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

        [HttpPost("usuario")]
        public async Task<ActionResult> Add(CreateUsuarioDto createUsuarioDto, CancellationToken cancellationToken)
        {
            var validationResult = await _createUsuarioValidator.ValidateAsync(createUsuarioDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _usuarioService.AddAsync(createUsuarioDto);
            return NoContent();
        }
    }
}
