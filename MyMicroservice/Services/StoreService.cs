using AutoMapper;
using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.Models;

namespace MyMicroservice.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreDataProvider _storeDataProvider;
        private readonly IMapper _mapper;

        public StoreService(IStoreDataProvider storeDataProvider, IMapper mapper)
        {
            _storeDataProvider = storeDataProvider;
            _mapper = mapper;
        }

        public void AddStore(Store data)
        {
            _storeDataProvider.AddStore(data);
        }

        public Store GetStoreById(int id)
        {
            var result = _storeDataProvider.GetStoreByIdWithDetails(id);
            return result;
        }

        public List<Store> GetStores()
        {
            return _storeDataProvider.GetStores();
        }

        public void UpdateStoreById(int id, Store data)
        {
            var oldStore = _storeDataProvider.GetStoreById(id);
            if (oldStore == null)
            {
                _storeDataProvider.AddStore(data);
                return;
            }

            oldStore.Street = data.Street;
            oldStore.Email = data.Email;
            oldStore.City = data.City;
            oldStore.Phone = data.Phone;
            oldStore.State = data.State;
            oldStore.StoreName = data.StoreName;
            oldStore.ZipCode = data.ZipCode;

            _storeDataProvider.UpdateStoreById();

        }

    }
}
