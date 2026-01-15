using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VideoClub.Models;

namespace VideoClub.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var path = HttpContext.Request.Path;
        
        return View(new ViewPathModel { Path = path });
    }

    public IActionResult Privacy()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
