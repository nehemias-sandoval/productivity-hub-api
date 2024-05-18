using productivity_hub_api.DTOs.Persona;

namespace productivity_hub_api.DTOs.Auth
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public PersonaDto Persona { get; set; }
    }
}
