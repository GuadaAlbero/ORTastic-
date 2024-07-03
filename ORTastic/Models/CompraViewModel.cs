using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORTastic.Models
{
    public class CompraViewModel
    {
        public List<Evento> Eventos { get; set; }

        [Required(ErrorMessage = "La compra es obligatoria.")]
        public Compra Compra { get; set; }  // Datos de la compra

        [Required(ErrorMessage = "La cantidad de entradas es obligatoria.")]
        [Range(1, 5, ErrorMessage = "La cantidad de entradas debe estar entre 1 y 5.")]
        public int CantidadEntradas { get; set; }  // Cantidad de entradas
    }
}
