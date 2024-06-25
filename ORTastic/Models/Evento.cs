using System.ComponentModel.DataAnnotations;
namespace ORTastic.Models
{
    public class Evento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombreevento { get; set; }

        //fecha y hs del evento
        [Required]
        public DateTime Fecha { get; set;}

        //Equipos participantes
        [Required]
        public string Equipos { get; set; }

        [Required]
        public float Precio { get; set; }

        // Relación uno a muchos con Compras
      //  public virtual ICollection<Compra> Compras { get; set; }
    }
}
