using System.ComponentModel.DataAnnotations;

namespace ORTastic.Models
{
    public class User
    {
        //identificamos id como clave primaria
        [Key]
        public int Id { get; set; }

        //atribito requerido, no puede estar vacio o null
        [Required]
        public string Username { get; set; }

        //Campo requerido y de tipo password
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [EnumDataType(typeof(Perfil))]
        public Perfil tipoPerfil { get; set; }

        // Relación uno a muchos con Compras
       // public virtual ICollection<Compra> Compras { get; set; }
    }
}
