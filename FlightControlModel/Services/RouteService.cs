using FlightControlApi.Models;
using FlightControlModel.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightControlModel.Services
{
    public class RouteService
    {
        private readonly RouteRepo _routeRepository;

        public RouteService(RouteRepo routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public List<Route> GetAll()
        {
            return _routeRepository.GetAll();
        }

        public Route Get(int id)
        {
            return _routeRepository.Get(id);
        }

        public int Insert(Route p)
        {
            return _routeRepository.Insert(p);
        }

        public bool Update(int id, Route p)
        {
            return _routeRepository.Update(id, p);
        }

        public bool Delete(int unid)
        {
            return _routeRepository.Delete(unid);
        }

    }
}
