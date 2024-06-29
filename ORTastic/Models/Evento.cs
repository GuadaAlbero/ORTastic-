using System.ComponentModel.DataAnnotations;
using System;
using ORTastic.Attributes;

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
        [FechaMinima(ErrorMessage = "La fecha del evento debe ser al menos 30 dias en el futuro")]
        [HorarioValido (ErrorMessage = "El evento debe ser programado entre las 16 y las 21hs")]
        [DataType (DataType.DateTime)]
        public DateTime Fecha { get; set;}

        //Equipos participantes
        [Required]
        public string Equipos { get; set; }

        [Required]
        public float Precio { get; set; }

        [Required]
        public int Stock { get; set; }

        // Relación uno a muchos con Compras
      //  public virtual ICollection<Compra> Compras { get; set; }
    }
}
