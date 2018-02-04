using FlightControlApi.Models;
using FlightControlModel.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightControlModel.Services
{
    public class FlightService
    {
        private readonly FlightRepo _flightRepository;

        public FlightService(FlightRepo flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public List<Flight> GetAll()
        {
            return _flightRepository.GetAll();
        }

        public Flight Get(int id)
        {
            return _flightRepository.Get(id);
        }

        public int Insert(Flight p)
        {
            return _flightRepository.Insert(p);
        }

        public bool Update(int id, Flight p)
        {
            return _flightRepository.Update(id, p);
        }

        public bool Delete(int unid)
        {
            return _flightRepository.Delete(unid);
        }
    }
}
