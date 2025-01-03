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
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Core.Types;

namespace ECommerce.Controllers
{
    [Route("wishlist")]
    public class WishlistsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWishlistRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public WishlistsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IWishlistRepository repository)
        {
            _userManager = userManager;
            _context = context;
            _repository = repository;
        }

        // GET: Wishlists
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Wishlists.Include(w => w.ApplicationUser).Include(w => w.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        [Route("all")]
        public async Task<IActionResult> GetUserWishlist()
        {
            var cart = await _repository.GetAllUsersWishlist();
            return View(cart);
        }

        private bool WishlistExists(int id)
        {
            return _context.Wishlists.Any(e => e.WishlistId == id);
        }
    }
}
