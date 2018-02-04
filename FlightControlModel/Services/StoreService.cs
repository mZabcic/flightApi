using FlightControlApi.Models;
using FlightControlModel.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightControlModel.Services
{
    public class StoreService
    {
        private readonly StoreRepo _storeRepository;

        public StoreService(StoreRepo storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public List<Store> GetAll()
        {
            return _storeRepository.GetAll();
        }

        public Store Get(int id)
        {
            return _storeRepository.Get(id);
        }

        public int Insert(Store p)
        {
            return _storeRepository.Insert(p);
        }

        public bool Update(int id, Store p)
        {
            return _storeRepository.Update(id, p);
        }

        public bool Delete(int unid)
        {
            return _storeRepository.Delete(unid);
        }

    }
}
