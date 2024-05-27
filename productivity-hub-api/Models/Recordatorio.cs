using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace productivity_hub_api.Models
{
    public class Recordatorio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Titulo { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaHoraInicio { get; set; }

        public bool Estado {  get; set; }
    }
}
