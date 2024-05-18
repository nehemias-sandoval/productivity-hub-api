using System.ComponentModel.DataAnnotations;

namespace productivity_hub_api.DTOs.Tarea
{
    public class UpdateTareaDto
    {
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        public bool Estado { get; set; }

        public List<UpdateTareaDto> Subtareas { get; set; }
    }
}
