using MyMicroservice.DTOModels;
using MyMicroservice.Models;

namespace MyMicroservice.Services
{
    public interface IOrderService
    {
        public IEnumerable<OrderDTO> GetOrders(int page, int maxItemsPerPage);
        Task<OrderDTO?> GetDetailedOrder(int id);
        Task CreateOrder(OrderDTO order);

        void DeleteOrder(OrderDTO order);

        OrderDTO? GetOrder(int id);
        IEnumerable<OrderDTO> GetOrdersByUser(int customerId, int page, int items);
        IEnumerable<OrderItemsDTO> GetOrderItems(int id);
    }
}
