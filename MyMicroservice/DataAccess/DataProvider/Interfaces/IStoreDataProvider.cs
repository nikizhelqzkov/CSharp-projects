using MyMicroservice.Db;

namespace MyMicroservice.DataAccess.DataProvider.Interfaces
{
    public interface IStoreDataProvider : IDataProvider
    {
        public List<Store> GetStores();
    }
}
