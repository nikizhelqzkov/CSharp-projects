using MyMicroservice.Models;
using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.DTOModels;
using AutoMapper;

namespace MyMicroservice.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderProvider _orderDataProvider;
        private readonly IMapper _mapper;

        public OrderService(IOrderProvider orderDataProvider, IMapper mapper)
        {
            _orderDataProvider = orderDataProvider;
            _mapper = mapper;
        }

        public async Task CreateOrder(OrderDTO requestOrder)
        {
            var order = _mapper.Map<OrderDTO, Order>(requestOrder);

            await _orderDataProvider.CreateOrder(order);
        }

        public void DeleteOrder(OrderDTO order)
        {
            var result = _mapper.Map<OrderDTO, Order>(order);
            _orderDataProvider.DeleteOrder(result);
        }

        public async Task<Order> GetDetailedOrder(int id)
        {
            return await _orderDataProvider.GetDetailedOrder(id);
        }

        public OrderDTO? GetOrder(int id)
        {
            var order = _orderDataProvider.GetOrder(id);
            if (order == null)
            {
                return null;
            }
            var orderDTO = _mapper.Map<Order, OrderDTO>(order);
            return orderDTO;

        }

        public async Task<IEnumerable<OrderDTO>> GetOrders(int page, int maxItemsPerPage)
        {
            var orders = await _orderDataProvider.GetOrders(page, maxItemsPerPage);
            var result = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);
            return result;
        }
    }
}
