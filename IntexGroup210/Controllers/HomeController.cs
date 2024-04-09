using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IntexGroup210.Controllers
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

		public IActionResult Test()
		{
			return View();
		}
    }
}
