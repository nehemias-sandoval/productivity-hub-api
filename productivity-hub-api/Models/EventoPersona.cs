using System.ComponentModel.DataAnnotations.Schema;

namespace productivity_hub_api.Models
{
    public class EventoPersona
    {
        public int IdEvento { get; set; }

        [ForeignKey("IdEvento")]
        public Evento Evento { get; set; }

        public int IdPersona { get; set; }

        [ForeignKey("IdPersona")]
        public Persona Persona { get; set; }

        public int IdEstadoInvitacion { get; set; }

        [ForeignKey("IdEstadoInvitacion")]
        public EstadoInvitacion EstadoInvitacion { get; set; }
    }
}
