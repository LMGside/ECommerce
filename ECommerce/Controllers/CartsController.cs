using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Controllers
{
    [Route("cart")]
    public class CartsController : Controller
    {
        private readonly ICartRepository _repository;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CartsController(ICartRepository repository, IEmailSender email, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _emailSender = email;
            _context = context;
            _userManager = userManager;
        }

        // GET: Carts
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Carts.Include(c => c.ApplicationUser).Include(c => c.Product).Include(c => c.ShoppingCart);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> AddItem(int productId, int qty = 1, int redirect = 0)
        {
            var cartCount = await _repository.AddItem(productId, qty);
            if(redirect == 0)
            {
                return Ok(cartCount);
            }

            return RedirectToAction("all");
        }

        [Route("all")]
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _repository.GetUserCart();
            return View(cart);
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }
    }
}
