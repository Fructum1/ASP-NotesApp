using ASP_NotesApp.DAL;
using ASP_NotesApp.Models;
using ASP_NotesApp.Models.Domain;
using ASP_NotesApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP_NotesApp.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
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