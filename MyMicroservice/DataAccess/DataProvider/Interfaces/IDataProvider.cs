using MyMicroservice.DataContext;

namespace MyMicroservice.DataAccess.DataProvider.Interfaces
{
    public interface IDataProvider
    {
        public BikeStoresDBContext DbContext { get; }
    }

    public interface IDataProvider<T> where T : IDataProvider
    {
        public T Provider { get; }
    }

}