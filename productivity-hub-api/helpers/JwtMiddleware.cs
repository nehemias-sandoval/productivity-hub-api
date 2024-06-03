using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.Service.AuthService;
using productivity_hub_api.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace productivity_hub_api.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUsuarioService<
            UsuarioDto, CreateUsuarioDto, UpdateUsuarioDto, AuthenticateReqDto, AuthenticateResDto> usuarioService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await attachUserToContext(context, usuarioService, token);

            await _next(context);
        }

        private async Task attachUserToContext(HttpContext context, IUsuarioService<
            UsuarioDto, CreateUsuarioDto, UpdateUsuarioDto, AuthenticateReqDto, AuthenticateResDto> usuarioService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clock skew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // Attach user to context on successful JWT validation
                context.Items["User"] = await usuarioService.GetByIdAsync(userId);
            }
            catch
            {
                // Do nothing if JWT validation fails
                // user is not attached to context so the request won't have access to secure routes
            }
        }
    }
}
