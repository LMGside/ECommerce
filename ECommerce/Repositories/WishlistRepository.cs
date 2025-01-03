using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommerce.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WishlistRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) 
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddItem(int productId)
        {
            string userID = GetUserId();
            //using var transaction = _db.Database.BeginTransaction();
            try
            {
                // Check Cart or Create Cart
                if (string.IsNullOrEmpty(userID))
                {
                    throw new Exception("User Is not Found");
                }
                var wishlist = await GetWishlist(userID);
                if (wishlist == null)
                {
                    wishlist = new Wishlist
                    {
                        UserId = userID,
                        ProductId = productId,
                        CreatedDate = DateTime.Now
                    };
                    _db.Wishlists.Add(wishlist);
                }
                _db.SaveChanges();

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
            }

            var wishlistCount = await GetWishlistItemCount(userID);
            return wishlistCount;
        }

        public async Task<int> RemoveItem(int productId)
        {
            string userID = GetUserId();
            //using var transaction = _db.Database.BeginTransaction();
            try
            {
                // Check Cart or Create Cart

                if (string.IsNullOrEmpty(userID))
                {
                    throw new Exception("User is not logged in");
                }
                var wish = await GetWishlist(userID);
                if (wish == null)
                {
                    throw new Exception("Wishlist is Empty");
                }

                // Cart Details
                var wishlist = _db.Wishlists.FirstOrDefault(a => a.UserId == wish.UserId && a.ProductId == productId);
                if (wishlist == null)
                {
                    throw new Exception("No Item found on the Wishlist");
                }
                else
                {
                    _db.Wishlists.Remove(wishlist);
                }
                _db.SaveChanges();
                //transaction.Commit();
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
            }

            var wishlistCount = await GetWishlistItemCount(userID);
            return wishlistCount;

        }

        public async Task<int> GetWishlistItemCount(string userId = "")
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }

            var data = await (from wishlist in _db.Wishlists
                              where wishlist.UserId == userId
                              select new { wishlist.UserId }).ToListAsync();

            return data.Count;
        }

        public async Task<IEnumerable<Wishlist>> GetAllUsersWishlist()
        {
            List<Wishlist> wishlists = new List<Wishlist>();
            var userId = GetUserId();
            if (userId == null)
            {
                var noUser = new Wishlist()
                {
                    UserId = ""
                };
                wishlists.Add(noUser);

                return wishlists;
            }

            var wish = await _db.Wishlists
                .Include(a => a.ApplicationUser)
                .Include(a => a.Product)
                .ThenInclude(a => a.SubCategory)
                .Where(a => a.UserId == userId).ToListAsync();

            return wish;
        }

        public async Task<Wishlist> GetUserWishlist()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                var noUser = new Wishlist()
                {
                    UserId = ""
                };

                return noUser;
            }

            var wishlist = await _db.Wishlists
                .Include(a => a.ApplicationUser)
                .Include(a => a.Product)
                .ThenInclude(a => a.SubCategory)
                .Where(a => a.UserId == userId).FirstOrDefaultAsync();

            return wishlist;
        }

        public async Task<Wishlist> GetWishlist(string userId)
        {
            var result = await _db.Wishlists.FirstOrDefaultAsync(x => x.UserId == userId);
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
