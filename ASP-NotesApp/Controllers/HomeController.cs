using ASP_NotesApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP_NotesApp.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            ViewData["Message"] = "Hello!";
            return View("Index");
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
}