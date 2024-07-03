using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ORTastic.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int EventoId { get; set; }
        public decimal Precio { get; set; }
        public int CantidadEntradas { get; set; }
    }
}