using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoClub.Models
{
    [Table("ElencoArticulo")]
    public class ElencoArticulo
    {

    [Key]    
    public int Id { get; set; }
    public int ArticuloId { get; set; }
    public Articulos Articulo { get; set; } = null!;
    public int ElencoId { get; set; }
    public Elenco Elenco { get; set; } = null!;
    public string Rol { get; set; } = null!;  
    }
}