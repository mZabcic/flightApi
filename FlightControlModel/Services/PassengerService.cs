using FlightControlApi.Models;
using FlightControlModel.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightControlModel.Services
{
    public class PassengerService
    {
        private readonly PassengerRepo _passengerRepository;

        public PassengerService(PassengerRepo passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public List<Passenger> GetAll()
        {
            return _passengerRepository.GetAll();
        }

        public Passenger Get(int id)
        {
            return _passengerRepository.Get(id);
        }

        public int Insert(Passenger p)
        {
            return _passengerRepository.Insert(p);
        }

        public bool Update(int id, Passenger p)
        {
            return _passengerRepository.Update(id, p);
        }

        public bool Delete(int unid)
        {
            return _passengerRepository.Delete(unid);
        }

    }
}
