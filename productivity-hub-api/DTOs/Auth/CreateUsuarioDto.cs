using productivity_hub_api.DTOs.Persona;

namespace productivity_hub_api.DTOs.Auth
{
    public class CreateUsuarioDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public CreatePersonaDto Persona { get; set; }
    }
}
