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

    public enum TipoArticulo
    {
        [Display(Name = "Pelicula")]
        Pelicula = 1,
        [Display(Name = "CD Musica")]
        CD_Musica = 2,
        [Display(Name = "Libro")]
        Libro = 3
    }

    
    [Key] 
    public int Id { get; set; }
    public EstadoArticulo Estado { get; set; }
    public TipoArticulo Tipo { get; set; }
}

}