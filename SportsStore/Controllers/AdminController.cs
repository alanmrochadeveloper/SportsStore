using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        // GET: /<controller>/
        public ViewResult Index() => View(repository.Products);
        public ViewResult Edit(int productId) =>
            View(repository.Products.FirstOrDefault( p => p.ProductID == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                if (product.ProductID != 0)
                {
                    TempData["message"] = $"{product.Name} has been saved";
                }
                else
                {
                    TempData["message"] = $"{product.Name} has been created";
                }
                return RedirectToAction("Index");
            }
            else
            {
                //there is something worng with the data valus
                return View(product);
            }
        }
        public ViewResult Create()
        {
            ViewBag.Title = "Create new product";
            ViewData["Title"] = "Creating...";
            return View("Edit", new Product());
        }
        
    }
}
