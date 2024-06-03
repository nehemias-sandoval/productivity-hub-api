using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace productivity_hub_api.Models
{
    public class Notificacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Mensaje { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }

        public bool Leida { get; set; }

        public int IdTipoNotificacion { get; set; }

        [ForeignKey("IdTipoNotificacion")]
        public TipoNotificacion TipoNotificacion { get; set; }

        public int IdPersona { get; set; }

        [ForeignKey("IdPersona")]
        public Persona Persona { get; set; }
    }
}
