using ECommerce.Data;
using ECommerce.Models.DisplayModels;
using ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    [Route("management")]
    public class ManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHomeRepository _homeRepository;
        private readonly IProductRepository _productRepository;

        public ManagementController(ApplicationDbContext context, IHomeRepository homeRepository, IProductRepository productRepository)
        {
            _context = context;
            _homeRepository = homeRepository;
            _productRepository = productRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("product")]
        public IActionResult ProductList() { 
            return View();
        }

        [Route("product/add")]
        public IActionResult AddProduct()
        {
            return View();
        }

        [Route("product/edit/{id?}")]
        public async Task<IActionResult> EditProduct(int? id)
        {
            Details productFull = new Details();
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.SubCategory)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            var productImages = await _productRepository.GetProductImages(id);
            productFull.Images = productImages;

            if (product == null)
            {
                return NotFound();
            }

            return View(productFull);
        }
    }
}
