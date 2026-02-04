using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoClub.Models
{
    [Table("Rentas")]
    public class Renta{

        public enum EstadoRenta
        {
            [Display(Name = "Activa")]
            Activa = 1,
            [Display(Name = "Devuelta")]
            Devuelta = 2,
            [Display(Name = "Atrasada")]
            Atrasada = 3,
            [Display(Name = "Cancelada")]
            Cancelada = 4
        }

    [Key]
    public int NoRenta { get; set; }                 

    public int EmpleadoId { get; set; }
    public Empleados Empleado { get; set; } = null!;

    public int ArticuloId { get; set; }
    public Articulos Articulo { get; set; } = null!;

    public int ClienteId { get; set; }
    public Clientes Cliente { get; set; } = null!;

    public DateTime FechaRenta { get; set; }
    public DateTime FechaDevolucion { get; set; }

    public decimal MontoPorDia { get; set; }      
    public int CantidadDias { get; set; }           
    public string? Comentario { get; set; }        
    public EstadoRenta Estado { get; set; }          
    }


}
