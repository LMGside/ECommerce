using ECommerce.Models;

namespace ECommerce.Repositories
{
    public interface IWishlistRepository
    {
        Task<int> AddItem(int productId);
        Task<int> RemoveItem(int productId);
        Task<int> GetWishlistItemCount(string userId = "");
        Task<Wishlist> GetUserWishlist();
        Task<Wishlist> GetWishlist(string userId);
        Task<IEnumerable<Wishlist>> GetAllUsersWishlist();

    }
}
