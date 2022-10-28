using AutoMapper;
using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.DataAccess.Requests;
using MyMicroservice.DTOModels;
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

        private StoreDTO FromRequestToDTO(StoreRequest request)
        {
            StoreDTO dto = new StoreDTO();
            dto.StoreName = request.StoreName;
            dto.Phone = request.Phone ?? null;
            dto.Email = request.Email ?? null;
            dto.Street = request.Street ?? null;
            dto.City = request.City ?? null;
            dto.State = request.State ?? null;
            dto.ZipCode = request.ZipCode ?? null;
            return dto;
        }

        public void AddStore(StoreRequest request)
        {
            var data = FromRequestToDTO(request);
            var store = _mapper.Map<Store>(data);
            _storeDataProvider.AddStore(store);
        }

        public StoreDTO GetStoreById(int id)
        {

            var result = _storeDataProvider.GetStoreByIdWithDetails(id);

            return _mapper.Map<StoreDTO>(result);
        }

        public List<StoreDTO> GetStores()
        {
            var store = _storeDataProvider.GetStores();

            return _mapper.Map<List<StoreDTO>>(store);
        }

        public void UpdateStoreById(int id, StoreDTO data)
        {
            var oldStore = _storeDataProvider.GetStoreById(id);

            if (oldStore == null)
            {

                _storeDataProvider.AddStore(_mapper.Map<Store>(data));
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
