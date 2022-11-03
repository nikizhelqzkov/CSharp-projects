using MyMicroservice.Models;
using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.DTOModels;
using AutoMapper;
using MyMicroservice.Helper.CustomMappings;

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
            requestOrder.OrderDate = DateTime.Now;
            var order = _mapper.Map<OrderDTO, Order>(requestOrder);

            await _orderDataProvider.CreateOrder(order);
        }

        public void DeleteOrder(OrderDTO order)
        {
            var result = _mapper.Map<OrderDTO, Order>(order);
            _orderDataProvider.DeleteOrder(result);
        }

        public async Task<OrderDTO?> GetDetailedOrder(int id)
        {
            var order = await _orderDataProvider.GetDetailedOrder(id);
            if (order == null)
            {
                return null;
            }
            var result = _mapper.Map<Order, OrderDTO>(order);
            return result;
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

        public IEnumerable<OrderDTO> GetOrders(int page, int maxItemsPerPage)
        {
            var orders = _orderDataProvider.GetOrders(page, maxItemsPerPage);
            var result = new List<OrderDTO>();
            foreach (var order in orders)
            {
                var newDto = _mapper.Map<Order, OrderDTO>(order);
                result.Add(newDto);
            }
            return result;
        }

        public IEnumerable<OrderDTO> GetOrdersByUser(int customerId, int page, int items)
        {
            var orders = _orderDataProvider.GetOrders(customerId, page, items);
            var result = new List<OrderDTO>();
            foreach (var order in orders)
            {
                var newDto = _mapper.Map<Order, OrderDTO>(order);
                result.Add(newDto);
            }
            return result;
        }
    }
}
