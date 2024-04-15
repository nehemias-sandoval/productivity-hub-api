using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace productivity_hub_api.Models
{
    public class Etiqueta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Color { get; set; }

        public List<TareaEtiqueta> TareaEtiquetas { get; set; }
    }
}
