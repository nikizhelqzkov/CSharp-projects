using Microsoft.EntityFrameworkCore;
using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.DataContext;
using MyMicroservice.Models;

namespace MyMicroservice.DataAccess.DataProvider.Clients
{
    public sealed class OrderProvider : IOrderProvider
    {
        public OrderProvider(BikeStoresDBContext dbContext)
        {
            DbContext = dbContext;
        }
        public BikeStoresDBContext DbContext { get; }

        public async Task CreateOrder(Order order)
        {
            DbContext.Orders.Add(order);
            await DbContext.SaveChangesAsync();
        }

        public void DeleteOrder(Order result)
        {
            DbContext.Orders.Remove(result);
            DbContext.SaveChanges();
        }

        public async Task<Order> GetDetailedOrder(int id)
        {
            var order = await DbContext.Orders
                              .Include(s => s.Staff)
                              .Include(s => s.Customer)
                              .Include(s => s.Store)
                              .Include(o => o.OrderItems)
                              .ThenInclude(s => s.Product)
                              .FirstOrDefaultAsync(o => o.OrderId == id);
            return order;
        }

        public Order? GetOrderWithItems(int id)
        {
            var result = (from o in DbContext.Orders.AsNoTracking()
                          join ordItem in DbContext.OrderItems on o.OrderId equals ordItem.OrderId
                          where o.OrderId == id
                          select new Order
                          {
                              OrderId = o.OrderId,
                              OrderDate = o.OrderDate,
                              RequiredDate = o.RequiredDate,
                              ShippedDate = o.ShippedDate,
                              CustomerId = o.CustomerId,
                              StaffId = o.StaffId,
                              StoreId = o.StoreId,
                              OrderItems = new List<OrderItem>
                              {
                                  new OrderItem
                                  {
                                      OrderId = ordItem.OrderId,
                                      ProductId = ordItem.ProductId,
                                      Quantity = ordItem.Quantity,
                                      ListPrice = ordItem.ListPrice,
                                      Discount = ordItem.Discount
                                  }
                              }
                          }).FirstOrDefault();
            return result;
        }
        public Order? GetOrder(int id)
        {
            var result = DbContext.Orders.AsNoTracking().FirstOrDefault(o => o.OrderId == id);
            return result;
        }

        public IEnumerable<Order> GetOrders(int page = 1, int maxItemsPerPage = 20)
        {
            return (from o in DbContext.Orders
                    join ordItem in DbContext.OrderItems on o.OrderId equals ordItem.OrderId
                    select new Order
                    {
                        OrderId = o.OrderId,
                        OrderDate = o.OrderDate,
                        RequiredDate = o.RequiredDate,
                        ShippedDate = o.ShippedDate,
                        CustomerId = o.CustomerId,
                        StaffId = o.StaffId,
                        StoreId = o.StoreId,
                        OrderItems = new List<OrderItem>
                              {
                                  new OrderItem
                                  {
                                      OrderId = ordItem.OrderId,
                                      ProductId = ordItem.ProductId,
                                      Quantity = ordItem.Quantity,
                                      ListPrice = ordItem.ListPrice,
                                      Discount = ordItem.Discount
                                  }
                              }
                    })
                         .Skip((page - 1) * maxItemsPerPage)
                         .Take(maxItemsPerPage)
                         .ToList();

        }

        public IEnumerable<Order> GetOrders(int customerId, int page, int maxItemsPerPage)
        {
            return (from o in DbContext.Orders
                    join ordItem in DbContext.OrderItems on o.OrderId equals ordItem.OrderId
                    join product in DbContext.Products on ordItem.ProductId equals product.ProductId
                    join store in DbContext.Stores on o.StoreId equals store.StoreId
                    where o.CustomerId == customerId
                    select new Order
                    {
                        OrderId = o.OrderId,
                        OrderDate = o.OrderDate,
                        RequiredDate = o.RequiredDate,
                        ShippedDate = o.ShippedDate,
                        CustomerId = o.CustomerId,
                        StaffId = o.StaffId,
                        StoreId = o.StoreId,
                        Store = new Store
                        {
                            StoreId = store.StoreId,
                            StoreName = store.StoreName,
                        },
                        OrderItems = new List<OrderItem>
                              {
                                  new OrderItem
                                  {
                                      ItemId = ordItem.ItemId,
                                      OrderId = ordItem.OrderId,
                                      ProductId = ordItem.ProductId,
                                      Quantity = ordItem.Quantity,
                                      ListPrice = ordItem.ListPrice,
                                      Discount = ordItem.Discount,
                                      Product = new Product
                                      {
                                          ProductId = product.ProductId,
                                          ProductName = product.ProductName,
                                          ModelYear = product.ModelYear
                                      }
                                  }
                              }
                    })
                    .Skip((page - 1) * maxItemsPerPage)
                    .Take(maxItemsPerPage)
                    .ToList();
        }

        public IEnumerable<OrderItem> GetOrderItems(int id)
        {
            var items = (from ordItem in DbContext.OrderItems
                         join product in DbContext.Products on ordItem.ProductId equals product.ProductId
                         where ordItem.OrderId == id
                         select new OrderItem
                         {
                             ItemId = ordItem.ItemId,
                             OrderId = ordItem.OrderId,
                             ProductId = ordItem.ProductId,
                             Quantity = ordItem.Quantity,
                             ListPrice = ordItem.ListPrice,
                             Discount = ordItem.Discount,
                             Product = new Product
                             {
                                 ProductId = product.ProductId,
                                 ProductName = product.ProductName,
                                 ModelYear = product.ModelYear
                             }
                         }).ToList();
            return items;
        }
    }
}
