namespace BookShoppingCartMvc.Ropository
{
    public interface ICartRepository
    {
        Task<int> AddItem(int BookId, int Quantity);
        Task<int> RemoveItem(int BookId);
        Task<ShoppingCart> GetCart(string userId);
        Task<int> GetCartItemCount(string userId="");
        Task<ShoppingCart> GetUserCart();
        Task<bool> DoCheckout(CheckoutViewModel checkoutModel);
    }
}