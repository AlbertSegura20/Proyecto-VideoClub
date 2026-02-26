using VideoClub.Models;

namespace VideoClub.ViewModels;

public class RentasViewModel
{
    public IEnumerable<Renta> Rentas { get; set; } = new List<Renta>();
    public IEnumerable<Empleados> Empleados { get; set; } = new List<Empleados>();
    public IEnumerable<Articulos> Articulos { get; set; } = new List<Articulos>();
    public IEnumerable<Clientes> Clientes { get; set; } = new List<Clientes>();
}
