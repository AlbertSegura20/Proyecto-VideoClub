using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VideoClub.Models;

namespace VideoClub.Controllers;

[Authorize]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        var path = HttpContext.Request.Path;
        
        return View(new ViewPathModel { Path = path });
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
