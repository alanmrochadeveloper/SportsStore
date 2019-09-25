
using Microsoft.AspNetCore.Mvc;
using SportsStoreEF01.Models;

namespace SportsStoreEF01.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;
        public HomeController(IRepository repo) => repository = repo;
        public IActionResult Index()
        {
            System.Console.Clear();
            return View(repository.Products);
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            repository.AddProduct(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
