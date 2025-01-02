using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommerce.Repositories
{
    public class CartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                throw new Exception("Invalid User");
            }

            var shoppingCart = await _db.ShoppingCarts
                .Include(a => a.ApplicationUser)
                .Include(a => a.Carts)
                .ThenInclude(a => a.Product)
                .ThenInclude(a => a.SubCategory)
                .Where(a => a.UserId == userId).FirstOrDefaultAsync();

            return shoppingCart;
        }

        public async Task<Order> UsersLastOrder()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User is not found");
            }
            var orders = await _db.Orders
                            .Include(u => u.ApplicationUser)
                            .Include(u => u.Product)
                            .ThenInclude(u => u.SubCategory)
                            .Where(u => u.UserId == userId)
                            .OrderByDescending(u => u.OrderDetailsId)
                            .FirstOrDefaultAsync();

            return orders;
        }

        public async Task<Order> GetUsersOrder(string orderID)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User is not found");
            }
            var orders = await _db.Orders
                            .Include(u => u.ApplicationUser)
                            .Include(u => u.Product)
                            .ThenInclude(u => u.SubCategory)
                            .Where(u => u.UserId == userId && u.OrderNo == orderID)
                            .FirstOrDefaultAsync();

            return orders;
        }

        public async Task<int> GetCartItemCount(string userId = "")
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }

            var data = await (from shoppingCart in _db.ShoppingCarts
                              join cart in _db.Carts
                              on shoppingCart.ShoppingCartId equals cart.ShoppingCartId
                              where cart.UserId == userId
                              select new { cart.ShoppingCartId }).ToListAsync();

            return data.Count;
        }

        public async Task<bool> DoCheckout()
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                //Enter => order, order details
                //Remove data => CartDetail

                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User not Logged in");
                }
                var cart = await GetShoppingCart(userId);
                if (cart == null)
                {
                    throw new Exception("Invalid Cart");
                }
                var cartDetail = _db.Carts
                                    .Where(u => u.ShoppingCartId == cart.ShoppingCartId).ToList();
                if (cartDetail.Count == 0)
                {
                    throw new Exception("Cart is Empty");
                }
                var order = new Order
                {
                    UserId = userId,
                    OrderNo = Guid.NewGuid().ToString().Substring(0, 8),
                    IsCancel = false,
                    OrderDate = DateTime.Now,
                    Status = "Pending" //pending

                };
                _db.Orders.Add(order);
                _db.SaveChanges();



                foreach (var item in cartDetail)
                {
                    var orderDetail = new Order
                    {
                        ProductId = item.ProductId,
                        OrderNo = Guid.NewGuid().ToString().Substring (0, 8),
                        UserId = userId,
                        IsCancel = false,
                        OrderDate = DateTime.Now,
                        Status = "In Process",
                        PaymentId = 0,
                        Quantity = item.Quantity,

                    };
                    _db.Orders.Add(orderDetail);
                }
                _db.SaveChanges();

                //Remove the Cart Details
                _db.Carts.RemoveRange(cartDetail);
                _db.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    ErrorMsg = ex.Message,
                    Exception = ex.ToString(),
                    StackTrace = ex.StackTrace,
                    CreatedDate = DateTime.Now
                };

                _db.ErrorLogs.Add(errorLog);
                _db.SaveChanges();

                return false;
            }
        }

        public async Task<ShoppingCart> GetShoppingCart(string userId)
        {
            var result = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return result;
        }

        private string GetUserId()
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return string.Empty;
            }
            ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;
            var userId = _userManager.GetUserId(user);

            return userId;
        }
    }
}
