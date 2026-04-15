using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoClub.Models
{
    [Table("Tipos_de_Articulos")]
    public class TiposArticulos {

    public enum EstadoArticulo
    {
        [Display(Name = "Activo")]
        Activo = 1,
        [Display(Name = "Inactivo")]
        Inactivo = 0
    }

    [Key] 
    public int Id { get; set; }
    public EstadoArticulo Estado { get; set; }
    public string Tipo { get; set; }
}

}