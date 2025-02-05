using ECommerce.Models;

namespace ECommerce.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int productId, int quantity);
        Task<int> RemoveItem(int productId);
        Task<Cart> GetUserCart();
        Task<Order> UsersLastOrder();
        Task<Order> GetUsersOrder(string orderID);
        Task<int> GetCartItemCount(string userId = "");
        Task<bool> DoCheckout();
        Task<Cart> GetShoppingCart(string userId);
    }
}
