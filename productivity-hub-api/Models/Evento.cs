using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace productivity_hub_api.Models
{
    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        public int IdTipoEvento { get; set; }

        [ForeignKey("IdTipoEvento")]
        public TipoEvento TipoEvento { get; set; }

        public int IdPersona { get; set; }

        [ForeignKey("IdPersona")]
        public Persona Persona { get; set; }

        public List<EventoTarea> EventoTareas { get; set; }
    }
}
