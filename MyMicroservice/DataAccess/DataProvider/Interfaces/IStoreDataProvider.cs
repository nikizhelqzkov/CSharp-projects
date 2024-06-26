﻿using MyMicroservice.Models;

namespace MyMicroservice.DataAccess.DataProvider.Interfaces
{
    public interface IStoreDataProvider : IDataProvider
    {
        public List<Store> GetStores();

        public Store GetStoreByIdWithDetails(int id);

        public Store GetStoreById(int id);

        public void AddStore(Store data);

        public int GetMaxedStore();

        public void UpdateStoreById();

    }
}
