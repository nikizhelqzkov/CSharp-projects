using MyMicroservice.DataAccess.Requests;
using MyMicroservice.DTOModels;
using MyMicroservice.Models;

namespace MyMicroservice.Services
{
    public interface IStoreService
    {
        public List<StoreDTO> GetStores();

        public StoreDTO GetStoreById(int id);

        public void AddStore(StoreRequest data);

        public void UpdateStoreById(int id, StoreDTO data);

    }
}
