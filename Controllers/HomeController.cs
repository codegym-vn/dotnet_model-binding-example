using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelBindingTest.Models;

namespace ModelBindingTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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

        static List<Product> products = new List<Product>();

        [HttpPost]
        public IActionResult CreateAndUpdate(Product model)
        {
            // string message = "";
            if (ModelState.IsValid)
            {
                bool isUpdated = false;
                foreach (Product product in products)
                {
                    if (model.Name == product.Name)
                    {
                        product.Brand = model.Brand;
                        product.Description = model.Description;
                        TempData["StatusMessage"] = "product " + model.Name + " is updated successfully";
                        isUpdated = true;
                        break;
                    }
                }

                if (!isUpdated)
                {
                    products.Add(model);
                    TempData["StatusMessage"] = "product " + model.Name + " created successfully. There're " + products.Count + " products now.";
                }
            }
            else
            {
                TempData["StatusMessage"] = "Failed to create the product. Please try again";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
