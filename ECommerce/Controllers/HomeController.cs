using ECommerce.Models;
using ECommerce.Models.DisplayModels;
using ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _logger = logger;
            _homeRepository = homeRepository;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> product = await _homeRepository.GetNewlyAddedProducts();

            product = product.Take(4);

            ProductList list = new ProductList()
            {
                Products = product
            };

            return View(list);
        }

        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("search")]
        public IActionResult Search() {
            return View();
        }

        [Route("wishlist")]
        public IActionResult WishList()
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
