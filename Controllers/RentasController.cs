using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Security.Claims;
using VideoClub.Models;
using VideoClub.ViewModels;

namespace VideoClub.Controllers;

public class RentasController : Controller
{
    private readonly AppDbContext _context;

    public RentasController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var vm = new RentasViewModel
            {
                Rentas = await _context.Rentas
                    .Include(r => r.Empleado)
                    .Include(r => r.Articulo)
                    .Include(r => r.Cliente)
                    .OrderByDescending(r => r.NoRenta)
                    .ToListAsync(),
                Empleados = new List<Empleados>(), // Set properly below
                Articulos = await _context.Articulos.Where(a => a.Estado == Articulos.EstadoArticulo.Disponible).ToListAsync(),
                Clientes = await _context.Clientes.Where(c => c.Estado == Clientes.EstadoCliente.Activo).ToListAsync()
            };

            var allEmpleados = await _context.Empleados.Where(e => e.Estado == Empleados.EstadoEmpleados.Activo).ToListAsync();
            
            if (User.IsInRole("Administrador"))
            {
                vm.Empleados = allEmpleados;
            }
            else
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdString, out int userId))
                {
                    vm.Empleados = allEmpleados.Where(e => e.Id == userId).ToList();
                }
            }

            ViewBag.Empleados = vm.Empleados;
            ViewBag.Articulos = vm.Articulos;
            ViewBag.Clientes = vm.Clientes;

            return View(vm);
        }
        catch (Exception ex)
        {
            Console.WriteLine("EF CORE ERROR: " + ex.ToString());
            throw;
        }
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] RentaCreationDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Los datos enviados están vacíos o tienen un formato incorrecto.");
        }

        if (!ModelState.IsValid)
        {
            var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            Console.WriteLine("MODEL STATE ERRORS: " + errors);
            return BadRequest($"Error de Validación: {errors}");
        }

        try
        {
            var renta = new Renta
            {
                EmpleadoId = dto.EmpleadoId,
                ArticuloId = dto.ArticuloId,
                ClienteId = dto.ClienteId,
                FechaRenta = DateTime.SpecifyKind(dto.FechaRenta, DateTimeKind.Utc),
                FechaDevolucion = DateTime.SpecifyKind(dto.FechaDevolucion, DateTimeKind.Utc),
                MontoPorDia = dto.MontoPorDia,
                CantidadDias = dto.CantidadDias,
                Comentario = dto.Comentario,
                Estado = dto.Estado
            };

            // Update Article Status to Rentado
            var articulo = _context.Articulos.Find(dto.ArticuloId);
            if (articulo != null)
            {
                articulo.Estado = Articulos.EstadoArticulo.Rentado;
                _context.Articulos.Update(articulo);
            }

            _context.Rentas.Add(renta);
            _context.SaveChanges();

            return Ok();
        }
        catch (Exception ex)
        {
            var exceptionMsg = ex.InnerException?.Message ?? ex.Message;
            Console.WriteLine("DB EXCEPTION: " + exceptionMsg);
            return BadRequest($"Error Base de Datos: {exceptionMsg}. Datos Obtenidos: Emp={dto.EmpleadoId}, Cli={dto.ClienteId}, Art={dto.ArticuloId}, F1={dto.FechaRenta}, F2={dto.FechaDevolucion}, Monto={dto.MontoPorDia}");
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] RentaCreationDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Los datos enviados están vacíos o tienen un formato incorrecto.");
        }

        if (!ModelState.IsValid) return BadRequest();

        var existing = _context.Rentas.Find(dto.NoRenta);
        if (existing == null) return NotFound();

        // If returned or changed article, handle article status logic here ideally.
        // For simplicity, we just update the Renta record.
        if (existing.ArticuloId != dto.ArticuloId)
        {
            // Set old article to available
            var oldArticulo = _context.Articulos.Find(existing.ArticuloId);
            if(oldArticulo != null){
                 oldArticulo.Estado = Articulos.EstadoArticulo.Disponible;
                _context.Articulos.Update(oldArticulo);
            }
           
            // Set new article to rentado
            var newArticulo = _context.Articulos.Find(dto.ArticuloId);
            if(newArticulo != null){
                newArticulo.Estado = Articulos.EstadoArticulo.Rentado;
                _context.Articulos.Update(newArticulo);
            }
        }

        if (dto.Estado == Renta.EstadoRenta.Devuelta || dto.Estado == Renta.EstadoRenta.Cancelada)
        {
             var articulo = _context.Articulos.Find(dto.ArticuloId);
             if (articulo != null)
             {
                 articulo.Estado = Articulos.EstadoArticulo.Disponible;
                 _context.Articulos.Update(articulo);
             }
        }

        existing.EmpleadoId = dto.EmpleadoId;
        existing.ArticuloId = dto.ArticuloId;
        existing.ClienteId = dto.ClienteId;
        existing.FechaRenta = DateTime.SpecifyKind(dto.FechaRenta, DateTimeKind.Utc);
        existing.FechaDevolucion = DateTime.SpecifyKind(dto.FechaDevolucion, DateTimeKind.Utc);
        existing.MontoPorDia = dto.MontoPorDia;
        existing.CantidadDias = dto.CantidadDias;
        existing.Comentario = dto.Comentario;
        existing.Estado = dto.Estado;

        _context.Rentas.Update(existing);
        _context.SaveChanges();

        return Ok(existing);
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] int id)
    {
        var data = _context.Rentas.Find(id);
        if (data == null) return NotFound();

        // Optional: restore article status to available if needed
        var articulo = _context.Articulos.Find(data.ArticuloId);
        if (articulo != null && data.Estado != Renta.EstadoRenta.Devuelta)
        {
            articulo.Estado = Articulos.EstadoArticulo.Disponible;
            _context.Articulos.Update(articulo);
        }

        _context.Rentas.Remove(data);
        _context.SaveChanges();
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> ExportarRentasPdf(List<int> ids)
    {
        var rentasQuery = _context.Rentas
            .Include(r => r.Empleado)
            .Include(r => r.Articulo)
            .Include(r => r.Cliente)
            .AsQueryable();

        if (ids != null && ids.Any())
        {
            rentasQuery = rentasQuery.Where(r => ids.Contains(r.NoRenta));
        }

        var rentasParaExportar = await rentasQuery.OrderByDescending(r => r.NoRenta).ToListAsync();

        var pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Landscape());
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header().Element(ComposeHeader);
                page.Content().Element(x => ComposeContent(x, rentasParaExportar));
                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Página ");
                    x.CurrentPageNumber();
                    x.Span(" de ");
                    x.TotalPages();
                });
            });
        });

        byte[] pdfBytes = pdf.GeneratePdf();
        return File(pdfBytes, "application/pdf", "Reporte_Rentas.pdf");

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Darken2);

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text("Reporte de Rentas").Style(titleStyle);
                    column.Item().Text($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            });
        }

        void ComposeContent(IContainer container, List<Renta> rentas)
        {
            container.PaddingVertical(1, Unit.Centimetre).Column(column =>
            {
                column.Spacing(5);

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(40); 
                        columns.RelativeColumn(); 
                        columns.RelativeColumn(); 
                        columns.RelativeColumn(); 
                        columns.ConstantColumn(65); 
                        columns.ConstantColumn(65); 
                        columns.ConstantColumn(80); 
                        columns.ConstantColumn(70); 
                    });

                    
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("No").SemiBold();
                        header.Cell().Element(CellStyle).Text("Empleado").SemiBold();
                        header.Cell().Element(CellStyle).Text("Artículo").SemiBold();
                        header.Cell().Element(CellStyle).Text("Cliente").SemiBold();
                        header.Cell().Element(CellStyle).AlignCenter().Text("F. Renta").SemiBold();
                        header.Cell().Element(CellStyle).AlignCenter().Text("F. Dev").SemiBold();
                        header.Cell().Element(CellStyle).AlignRight().Text("Total").SemiBold();
                        header.Cell().Element(CellStyle).AlignCenter().Text("Estado").SemiBold();

                        static IContainer CellStyle(IContainer container) => container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    });

                    
                    foreach (var renta in rentas)
                    {
                        table.Cell().Element(CellStyle).Text(renta.NoRenta.ToString());
                        table.Cell().Element(CellStyle).Text(renta.Empleado?.Nombre ?? "N/A");
                        table.Cell().Element(CellStyle).Text(renta.Articulo?.Titulo ?? "N/A");
                        table.Cell().Element(CellStyle).Text(renta.Cliente?.Nombre ?? "N/A");
                        table.Cell().Element(CellStyle).AlignCenter().Text(renta.FechaRenta.ToString("dd/MM/yyyy"));
                        table.Cell().Element(CellStyle).AlignCenter().Text(renta.FechaDevolucion.ToString("dd/MM/yyyy"));
                        table.Cell().Element(CellStyle).AlignRight().Text((renta.MontoPorDia * renta.CantidadDias).ToString("C"));
                        table.Cell().Element(CellStyle).AlignCenter().Text(renta.Estado.ToString());

                        static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(3);
                    }
                });
            });
        }
    }
}
