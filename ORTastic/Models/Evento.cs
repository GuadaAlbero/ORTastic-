using System.ComponentModel.DataAnnotations;
using System;
using ORTastic.Attributes;
namespace ORTastic.Models
{
    public class Evento
    {
        [Key]
        public int Id { get; set; }
        public string Nombreevento { get; set; }
        //fecha y hs del evento
        [FechaMinima(ErrorMessage = "La fecha del evento debe ser al menos 30 dias en el futuro")]
        [HorarioValido(ErrorMessage = "El evento debe ser programado entre las 16 y las 21hs")]
        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }
        //Equipos participantes
        public string Equipos { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
