using MyMicroservice.Models;

namespace MyMicroservice.DataAccess.DataProvider.Interfaces
{
    public interface IOrderProvider : IDataProvider
    {
        IEnumerable<Order> GetOrders(int page = 1, int maxItemsPerPage = 20);
        Task<Order> GetDetailedOrder(int id);
        Task CreateOrder(Order order);
        Order? GetOrder(int id);
        Order? GetOrderWithItems(int id);
        void DeleteOrder(Order result);
        IEnumerable<Order> GetOrders(int customerId, int page = 1, int items = 20);
        IEnumerable<OrderItem> GetOrderItems(int id);
    }
}
