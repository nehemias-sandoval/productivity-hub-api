namespace productivity_hub_api.DTOs.Persona
{
    public class CreatePersonaDto
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime FechaNacimiento { get; set; }
    }
}
