using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.Db;

namespace MyMicroservice.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreDataProvider _storeDataProvider;

        public StoreService(IStoreDataProvider storeDataProvider)
        {
            _storeDataProvider = storeDataProvider;
        }

        public List<Store> GetStores()
        {
            return _storeDataProvider.GetStores();
        }
    }
}
