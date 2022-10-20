using MyMicroservice.Db;

namespace MyMicroservice.Services
{
    public interface IStoreService
    {
        public List<Store> GetStores();
    }
}
