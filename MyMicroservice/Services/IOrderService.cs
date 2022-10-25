using MyMicroservice.DTOModels;
using MyMicroservice.Models;

namespace MyMicroservice.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDTO>> GetOrders(int page, int maxItemsPerPage);
        Task<Order> GetDetailedOrder(int id);
        Task CreateOrder(OrderDTO order);

        void DeleteOrder(OrderDTO order);

        OrderDTO? GetOrder(int id);
    }
}
