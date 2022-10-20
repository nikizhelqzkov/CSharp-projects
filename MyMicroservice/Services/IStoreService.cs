using MyMicroservice.Models;

namespace MyMicroservice.Services
{
    public interface IStoreService
    {
        public List<Store> GetStores();

        public Store GetStoreById(int id);

        public void AddStore(Store data);
    }
}
