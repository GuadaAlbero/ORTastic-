namespace ORTastic.Models
{
    public class CompraDetalle
    {
        public int IdCompra { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreEvento { get; set; }
        public int CantidadEntradas { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
