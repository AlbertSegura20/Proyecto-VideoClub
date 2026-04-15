namespace VideoClub.ViewModels;

public class ElencoArticuloCreationDto
{
    public int Id { get; set; }
    public int ArticuloId { get; set; }
    public int ElencoId { get; set; }
    public string Rol { get; set; } = null!;
}
