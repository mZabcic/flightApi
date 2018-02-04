using FlightControlApi.Models;
using FlightControlModel.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightControlModel.Services
{
    public class PlaneService
    {
        private readonly PlaneRepo _planeRepository;

        public PlaneService(PlaneRepo planeRepository)
        {
            _planeRepository = planeRepository;
        }

        public List<Plane> GetAll()
        {
            return _planeRepository.GetAll();
        }

        public Plane Get(int id)
        {
            return _planeRepository.Get(id);
        }

        public int Insert(Plane p)
        {
            return _planeRepository.Insert(p);
        }

        public bool Update(int id, Plane p)
        {
            return _planeRepository.Update(id, p);
        }

        public bool Delete(int unid)
        {
            return _planeRepository.Delete(unid);
        }

    }
}
