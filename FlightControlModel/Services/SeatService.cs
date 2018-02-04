using FlightControlApi.Models;
using FlightControlModel.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightControlModel.Services
{
    public class SeatService
    {
        private readonly SeatRepo _seatRepository;

        public SeatService(SeatRepo seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public List<Seat> GetAll()
        {
            return _seatRepository.GetAll();
        }

        public Seat Get(int id)
        {
            return _seatRepository.Get(id);
        }

        public int Insert(Seat p)
        {
            return _seatRepository.Insert(p);
        }

        public bool Update(int id, Seat p)
        {
            return _seatRepository.Update(id, p);
        }

        public bool Delete(int unid)
        {
            return _seatRepository.Delete(unid);
        }

    }
}
