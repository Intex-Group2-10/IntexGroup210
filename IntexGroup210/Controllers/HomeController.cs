using IntexGroup210.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IntexGroup210.Controllers
{
    public class HomeController : Controller
    {
        private ILegoRepository _repo;

		public HomeController(ILegoRepository temp)
		{
			_repo = temp;
		}

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
            var viewStuff = _repo.Products.ToList();

			return View(viewStuff);
		}
    }
}
