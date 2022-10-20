using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.Models;

namespace MyMicroservice.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreDataProvider _storeDataProvider;

        public StoreService(IStoreDataProvider storeDataProvider)
        {
            _storeDataProvider = storeDataProvider;
        }

        public void AddStore(Store data)
        {
            _storeDataProvider.AddStore(data);
        }

        public Store GetStoreById(int id)
        {
            var result = _storeDataProvider.GetStoreById(id);
            return result;
        }

        public List<Store> GetStores()
        {
            return _storeDataProvider.GetStores();
        }
    }
}
