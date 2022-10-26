using MyMicroservice.DTOModels;
using MyMicroservice.Models;

namespace MyMicroservice.Helper.CustomMappings
{
    public static class OrderItemAddition
    {
        public static OrderItemsDTO AdditionMap(OrderItem src, OrderItemsDTO dest)
        {
            if (src.Product != null)
            {
                dest.ProductName = src.Product.ProductName;
            }
            return dest;
        }
        public static OrderItem AdditionMap(OrderItemsDTO src, OrderItem dest)
        {
            dest.Product.ProductName = src.ProductName;

            return dest;
        }

        public static ICollection<OrderItem>? AddListMap(ICollection<OrderItemsDTO> src, ICollection<OrderItem> dest)
        {
            if (src == null)
            {
                return null;
            }
            ICollection<OrderItem> result = new List<OrderItem>();
            for (int i = 0; i < dest.Count; i++)
            {
                var newOrder = dest.ElementAt(i);
                newOrder = AdditionMap(src.ElementAt(i), newOrder);
                result.Add(newOrder);
            }
            return result;
        }
        public static ICollection<OrderItemsDTO>? AddListMap(ICollection<OrderItem> src, ICollection<OrderItemsDTO> dest)
        {
            if (src == null)
            {
                return null;
            }
            ICollection<OrderItemsDTO> result = new List<OrderItemsDTO>();
            for (int i = 0; i < dest.Count; i++)
            {
                var newOrder = dest.ElementAt(i);
                newOrder = AdditionMap(src.ElementAt(i), newOrder);
                result.Add(newOrder);
            }
            return result;
        }
    }
}
