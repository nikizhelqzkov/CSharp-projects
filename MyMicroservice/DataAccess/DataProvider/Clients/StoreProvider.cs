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
            var result = DbContext.Stores
               .Include(store => store.Orders)
               .FirstOrDefault(store => store.StoreId == id);
            return result;
        }

        public void UpdateStoreById()
        {
            DbContext.SaveChanges();
        }

    }

}
