using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace productivity_hub_api.Models
{
    public class Frecuencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public List<EventoRecordatorio> EventoRecordatorios { get; set; }
    }
}
