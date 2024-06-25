using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORTastic.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UsuarioId { get; set; }

        //propiedad de navegacion para usuario
        public virtual User Usuario { get; set; }

        [Required]
        [ForeignKey("Evento")]
        public int EventoId { get; set; }

        public virtual Evento Eventos { get; set; }

        public float PrecioTotal { get; set; }



    }
}
