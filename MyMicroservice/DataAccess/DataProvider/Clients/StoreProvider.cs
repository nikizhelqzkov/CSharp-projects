using Microsoft.EntityFrameworkCore;
using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.DataContext;
using MyMicroservice.Models;

namespace MyMicroservice.DataAccess.DataProvider.Clients
{
    public sealed class StoreProvider : IStoreDataProvider
    {
        public StoreProvider(BikeStoresDBContext dbContext)
        {
            DbContext = dbContext;
        }
        public BikeStoresDBContext DbContext { get; }

        public Store GetStoreById(int id)
        {

            var result = DbContext.Stores.FirstOrDefault(store => store.StoreId == id);
            return result;

        }

        public List<Store> GetStores()
        {
            return DbContext.Stores.ToList();
        }

        public void AddStore(Store data)
        {
            DbContext.Stores.Add(data);
            DbContext.SaveChanges();
        }

        public int GetMaxedStore()
        {
            return DbContext.Stores.Select(i => i.StoreId).Max();
        }

        public Store GetStoreByIdWithDetails(int id)
        {
            var result = (from store in DbContext.Stores
                          join order in DbContext.Orders on store.StoreId equals order.StoreId
                          join staff in DbContext.Staffs on store.StoreId equals staff.StoreId
                          join stock in DbContext.Stocks on store.StoreId equals stock.StoreId
                          where store.StoreId == id
                          select new Store
                          {
                              StoreId = store.StoreId,
                              StoreName = store.StoreName,
                              Phone = store.Phone,
                              Email = store.Email,
                              Street = store.Street,
                              City = store.City,
                              State = store.State,
                              ZipCode = store.ZipCode,
                              Orders = new List<Order>
                            {
                              new Order
                              {

                              OrderId = order.OrderId,
                              OrderDate = order.OrderDate,
                              RequiredDate = order.RequiredDate,
                              ShippedDate = order.ShippedDate,
                              CustomerId = order.CustomerId,
                              StaffId = order.StaffId,
                              StoreId = order.StoreId,
                              }
                            },
                              Staff = new List<Staff>
                             {
                                 new Staff
                                 {
                                    StaffId = staff.StaffId,
                                    FirstName = staff.FirstName,
                                    LastName = staff.LastName,
                                    Email = staff.Email,
                                    Phone = staff.Phone,
                                    Active = staff.Active,
                                    StoreId = staff.StoreId,
                                    ManagerId = staff.ManagerId,
                                 }
                             },
                              Stocks = new List<Stock>
                             {
                                 new Stock
                                 {
                                     StoreId = stock.StoreId,
                                     ProductId = stock.ProductId,
                                     Quantity = stock.Quantity
                                 }
                             }

                          }).FirstOrDefault();
            return result;
        }

        public void UpdateStoreById()
        {
            DbContext.SaveChanges();
        }

    }

}
