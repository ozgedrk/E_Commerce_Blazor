using E_Commerce_Client.ViewModels;

namespace E_Commerce_Client.Service.IService
{
    public interface ICartService
    {
        Task DecrementItem(ShoppingCart shoppingCart);
        Task IncrementItem(ShoppingCart shoppingCart);
    }
}
