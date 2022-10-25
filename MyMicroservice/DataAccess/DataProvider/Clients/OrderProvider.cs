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

        public Order? GetOrder(int id)
        {
            var result = (from o in DbContext.Orders.AsNoTracking()
                              //join ordItems in DbContext.OrderItems on o.OrderId equals ordItems.OrderId
                          where o.OrderId == id
                          select o).FirstOrDefault();
            return result;
        }

        public async Task<IEnumerable<Order>> GetOrders(int page = 1, int maxItemsPerPage = 20)
        {
            return await DbContext.Orders.Include(o => o.OrderItems)
                .Skip((page - 1) * maxItemsPerPage).Take(maxItemsPerPage).ToListAsync();
        }
    }
}
