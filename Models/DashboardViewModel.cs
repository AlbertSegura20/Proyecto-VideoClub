using System.Collections.Generic;
using VideoClub.Models;

namespace VideoClub.Models
{
    public class DashboardViewModel
    {
        public int RentasHoy { get; set; }
        public int RentasPendientes { get; set; }
        public int EntregasAtrasadas { get; set; }
        public int ArticulosDisponibles { get; set; }
        public List<Renta> UltimasRentas { get; set; } = new List<Renta>();
        
        // Include this to maintain the layout path state in Dashboard
        public string Path { get; set; } = string.Empty;
    }
}
