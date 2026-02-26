using System.ComponentModel.DataAnnotations;
using static VideoClub.Models.Renta;

namespace VideoClub.ViewModels;

public class RentaCreationDto
{
    public int NoRenta { get; set; }
    
    [Required]
    public int EmpleadoId { get; set; }
    
    [Required]
    public int ArticuloId { get; set; }
    
    [Required]
    public int ClienteId { get; set; }
    
    [Required]
    public DateTime FechaRenta { get; set; }
    
    [Required]
    public DateTime FechaDevolucion { get; set; }
    
    [Required]
    public decimal MontoPorDia { get; set; }
    
    [Required]
    public int CantidadDias { get; set; }
    
    public string? Comentario { get; set; }
    
    [Required]
    public EstadoRenta Estado { get; set; }
}
