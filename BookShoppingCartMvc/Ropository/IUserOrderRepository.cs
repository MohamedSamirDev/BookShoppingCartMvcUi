namespace BookShoppingCartMvc.Ropository
{
    public interface IUserOrderRepository
    {
        Task<IEnumerable<Order>> UserOrders(bool getAll=false);
        Task ChangeOrderStatus(UpdateOrderStatusViewModel data);
        Task TogglePaymentStatus(int orderId);
        Task<Order?> GetOrderById(int id);

        Task<IEnumerable<OrderStatus>> GetOrderStatuses();
        string? GetUserId();
    }
}