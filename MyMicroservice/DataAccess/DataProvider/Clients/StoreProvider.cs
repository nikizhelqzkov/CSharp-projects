using Microsoft.EntityFrameworkCore;
using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.Db;

namespace MyMicroservice.DataAccess.DataProvider.Clients
{
    public sealed class StoreProvider : IStoreDataProvider
    {
        public StoreProvider(BikeStoresDBContext dbContext)
        {
            DbContext = dbContext;
        }
        public BikeStoresDBContext DbContext { get; }
        public List<Store> GetStores()
        {
            return DbContext.Stores.ToList();
        }
    }

}
