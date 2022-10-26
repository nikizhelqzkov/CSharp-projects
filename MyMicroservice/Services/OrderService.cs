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
            result = OrderAddition.AdditionMap(order, result);
            result.OrderItems = OrderItemAddition.AddListMap(order.OrderItems, result.OrderItems);



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

        public async Task<IEnumerable<OrderDTO>> GetOrders(int page, int maxItemsPerPage)
        {
            var order = await _orderDataProvider.GetOrders(page, maxItemsPerPage);
            var result = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(order);
            int size = result.Count();
            var resultItems = new List<OrderDTO>();
            for (int i = 0; i < size; i++)
            {
                var res = OrderAddition.AdditionMap(order.ElementAt(i), result.ElementAt(i));
                res.OrderItems = OrderItemAddition.AddListMap(order.ElementAt(i).OrderItems, res.OrderItems);
                resultItems.Add(res);
            }



            return resultItems;
        }
    }
}
