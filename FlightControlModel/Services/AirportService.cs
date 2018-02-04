using FlightControlApi.Models;
using FlightControlModel.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightControlModel.Services
{
    public class AirportService
    {
        private readonly AirportRepo _airportRepository;

        public AirportService(AirportRepo airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public List<Airport> GetAll()
        {
            return _airportRepository.GetAll();
        }

        public Airport Get(int id)
        {
            return _airportRepository.Get(id);
        }

        public int Insert(Airport p)
        {
            return _airportRepository.Insert(p);
        }

        public bool Update(int id, Airport p)
        {
            return _airportRepository.Update(id, p);
        }

        public bool Delete(int unid)
        {
            return _airportRepository.Delete(unid);
        }
    
}
}
