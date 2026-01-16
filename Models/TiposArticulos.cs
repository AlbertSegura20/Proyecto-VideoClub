
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoClub.Models
{
    public class TiposArticulos {

    public enum EstadoArticulo
    {
        Activo = 1,
        Inactivo = 0
    }

    
        // EF Core detecta automáticamente "Id" como Primary Key
    [Key] 
    public int Id { get; set; }

        // Definimos restricciones como longitud máxima
    [MaxLength(100)]
    public string? Descripcion { get; set; }

        // EF Core guardará esto como int (1 o 0) por defecto en la BD
    public EstadoArticulo Estado { get; set; }
}

}