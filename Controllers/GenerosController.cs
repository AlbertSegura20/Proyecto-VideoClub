using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VideoClub.Models;

namespace VideoClub.Controllers;

public class GenerosController : Controller
{
  
    public IActionResult Index()
    {
        var path = HttpContext.Request.Path;
        return View(new ViewPathModel { Path = path });
    }

}
